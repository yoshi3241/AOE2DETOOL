using AOE2DETOOL.Models.Data;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;
using static AOE2DETOOL.Definition.Enums;
using static AOE2DETOOL.Models.Data.UnitInfo;
using AOE2DETOOL.Utilities;
using Timer = System.Timers.Timer;
using AOE2DETOOL.Definition;

namespace AOE2DETOOL.Models.Logic
{
    public class AOE2DEDataProcess
    {
        const string TAG_POSION_NO_DATA = "none";

        public static readonly object BuildInfoDataLock = new object();
        public static readonly object MoveInfoDataLock = new object();
        public static readonly object TechInfoDataLock = new object();

        // DOSプロセス渡しの為static
        private static string _gptRequestStr = "";
        private OpenAIService openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = Environment.GetEnvironmentVariable(Constants.KEY_ENV_OPENAI) ?? ""
        });

        public struct ActionCommand
        {
            private double? _gameTime;                          // ゲーム時間
            public double? GameTime
            {
                get { return _gameTime; }
                set { _gameTime = value; }
            }

            private string _command;
            public string Command
            {
                get { return _command; }
                set { _command = value; }
            }

            private CommandDetail _commandDetail;
            public CommandDetail CommandDetail
            {
                get { return _commandDetail; }
                set { _commandDetail = value; }
            }

            private float _x;
            public float X
            {
                get { return _x; }
                set { _x = value; }
            }

            private float _y;
            public float Y
            {
                get { return _y; }
                set { _y = value; }
            }
        }

        public float _viewX = 0;    // ゲーム視界X
        public float ViewX
        {
            get { return _viewX; }
        }

        public float _viewY = 0;    // ゲーム視界Y
        public float ViewY
        {
            get { return _viewY; }
        }

        private double _gameTime = 0;   // ゲーム時間
        public double GameTime
        {
            get { return _gameTime; }
        }

        private double _toolLocalTime = 0;  // ゲーム時間
        public double ToolLocalTime
        {
            get { return _toolLocalTime; }
        }

        private double _finalTime;                          // ゲーム時間
        public double FinalTime
        {
            get { return _finalTime; }
            set { _finalTime = value; }
        }

        private List<ActionCommand> _actionCommandList = new List<ActionCommand>();
        public List<ActionCommand> ActionCommandList
        {
            get { return _actionCommandList; }
        }

        private string _log = "";   // デバッグ用パイソンデータログ
        public string Log
        {
            get { return _log; }
        }

        private bool _pythonRunning = false;    // パイソン実行状態
        public bool PythonRunning
        {
            get { return _pythonRunning; }
        }

        private List<PlayerInfo> _playerInfoList = new List<PlayerInfo>();
        public List<PlayerInfo> PlayerInfoList
        {
            get { return _playerInfoList; }
        }

        List<BuildInfo> _buildInfoList = new List<BuildInfo>();
        public List<BuildInfo> BildInfoList
        {
            get { return _buildInfoList; }
        }

        List<MoveInfo> _moveInfoList = new List<MoveInfo>();
        public List<MoveInfo> MoveInfoList
        {
            get { return _moveInfoList; }
        }

        List<TechInfo> _techInfoList = new List<TechInfo>();
        public List<TechInfo> TechInfoList
        {
            get { return _techInfoList; }
        }


        string _myselfName = "";
        public string MyselfName
        {
            get { return _myselfName; }
            set { _myselfName = value; }
        }

        private int _commandIndex = 0;  // コマンドインデックス
        private Process? _pythonProcProcess = null; // パイソンプロセス
        private Timer? _timer = null;
        private readonly List<Action> _startCallbacks = new();
        private bool _isError = false;

        public void AddStartCallback(Action callback)
        {
            if (!_startCallbacks.Contains(callback))
                _startCallbacks.Add(callback);
        }

        public void RemoveStartCallback(Action callback)
        {
            _startCallbacks.Remove(callback);
        }

        private void InvokeStartCallbacks()
        {
            foreach (var cb in _startCallbacks.ToList())
            {
                cb?.Invoke();
            }
        }

        public void ClearLog()
        {
            _log = "";
        }

        public bool GameCommandProc(ActionCommand actionCommand)
        {
            var command = actionCommand.Command;

            switch (command)
            {
                case "CHAT":
                    break;

                case "POSTGAME":     // 投了

                    Speech.Talk($"ゲームが終了しました").Forget();

                    return true;

                case "VIEWLOCK":
                    _viewX = actionCommand.X;
                    _viewY = actionCommand.Y;
                    break;
            }

            return false;
        }

        public bool ActionCommandProc(ActionCommand actionCommand)
        {
            var command = actionCommand.Command;

            var commandDetail = actionCommand.CommandDetail;
            if (actionCommand.GameTime is not null)
            {
                _gameTime = actionCommand.GameTime.Value;
            }

            switch (command)
            {
                case "Action.DE_QUEUE":     // ユニット生産（カウンタ加算）

                    PlayerUnitAdd(commandDetail);

                    break;

                case "Action.MOVE":         // ユニット移動
                                            // ACTION:1,Action.MOVE,{'player_id': 1, 'object_ids': [], 'x': 79.27083587646484, 'y': 69.4375, 'sequence': 15733167}
                    var mi = new MoveInfo
                    {
                        PlayerId = commandDetail.PlayerId,
                        ObjectIds = commandDetail.ObjectIds,
                        BuildingId = commandDetail.BuildingId,
                        X = commandDetail.X,
                        Y = commandDetail.Y,
                        Sequence = commandDetail.Sequence
                    };

                    lock (MoveInfoDataLock)
                    {
                        _moveInfoList.Add(mi);
                    }
                    break;

                case "Action.BUILD":        // 建物建築
                                            // ACTION:1,Action.BUILD,{'player_id': 1, 'building_id': 70, 'object_ids': [30559, 30560], 'x': 100.0, 'y': 102.0, 'sequence': 15996014}
                    var bi = new BuildInfo
                    {
                        PlayerId = commandDetail.PlayerId,
                        ObjectIds = commandDetail.ObjectIds,
                        BuildingId = commandDetail.BuildingId,
                        X = commandDetail.X,
                        Y = commandDetail.Y,
                        Sequence = commandDetail.Sequence
                    };

                    lock (BuildInfoDataLock)
                    {
                        _buildInfoList.Add(bi);
                    }

                    break;

                case "Action.ORDER":        // ターゲット攻撃など
                                            // ACTION:1,Action.ORDER,{'player_id': 1, 'object_ids': [30559, 30560], 'target_id': 30742, 'x': 95.0, 'y': 95.0, 'sequence': 15971028}

                    break;

                case "Action.WALL":        // 柵建築
                                            //00:00:14 ACTION;Action.WALL;{'player_id': 1, 'object_ids': [29011, 29012], 'x': 167, 'y': 174, 'x_end': 180, 'y_end': 174, 'building_id': 72, 'sequence': 14888}
                    var biWall = new BuildInfo
                    {
                        PlayerId = commandDetail.PlayerId,
                        ObjectIds = commandDetail.ObjectIds,
                        BuildingId = commandDetail.BuildingId,
                        X = commandDetail.X,
                        Y = commandDetail.Y,
                        XEnd = commandDetail.XEnd,
                        YEnd = commandDetail.YEnd,
                        Sequence = commandDetail.Sequence
                    };

                    lock (BuildInfoDataLock)
                    {
                        _buildInfoList.Add(biWall);
                    }

                    break;

                case "Action.SPECIAL":      // 作成キャンセル（カウンタ減算）

                    break;

                case "Action.RESEARCH":     // テクノロジー開発
                                            // Action.RESEARCH,1,{'technology_id': 239, 'object_ids': [30592]}

                    PlayerTownCenterTechDevelopment(commandDetail);

                    var ti = new TechInfo
                    {
                        PlayerId = commandDetail.PlayerId,
                        ObjectIds = commandDetail.ObjectIds,
                        TechnologyId = commandDetail.TechnologyId,
                        Sequence = commandDetail.Sequence
                    };

                    lock (TechInfoDataLock)
                    {
                        _techInfoList.Add(ti);
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Pythonから取得したデータから個別イベントに分解して処理する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimeCore(object? sender, ElapsedEventArgs e)
        {
            StopTimer();

            _toolLocalTime+= Constants.ElapsedTimeInterval;
            if (_toolLocalTime > _finalTime) _toolLocalTime = _finalTime;

            while (_commandIndex < _actionCommandList.Count)
            {
                if (_commandIndex >= _actionCommandList.Count) break;

                var commandQueue = _actionCommandList[_commandIndex];
                if (GameCommandProc(commandQueue))
                {
                    // end
                    return;
                }

                if (commandQueue.CommandDetail is null || commandQueue.CommandDetail.Sequence > _toolLocalTime)
                {
                    break;
                }

                if (ActionCommandProc(commandQueue))
                {
                    // end
                    return;
                }

                var myself = _playerInfoList.FirstOrDefault(p => p.IsMyself);
                var (sneakyPlayerInfo, allyPlayerInfo) = IsEnemyForwardBuildingNearTeamCenter(myself!, 50);
                if (sneakyPlayerInfo is not null && !sneakyPlayerInfo.IsSneakyForward)
                {
                    sneakyPlayerInfo.IsSneakyForward = true;
                    var sneakSpeakColor = sneakyPlayerInfo.GetColorSpeak();
                    var allySpeakColor = allyPlayerInfo.GetColorSpeak();
                    Speech.Talk($"{sneakSpeakColor}が{allySpeakColor}の近く小屋を建てています").Forget();
                }

                _commandIndex++;
            }

            StartTimer();
        }
        
        /// <summary>
        /// データ初期化
        /// </summary>
        public bool InitData(string myselfName)
        {
            _isError = false;

            _myselfName = myselfName;

            if (_timer == null)
            {
                _timer = new Timer(10);
                _timer.Elapsed += TimeCore;
            }

            ClearLog();       // パイソン受け渡しログデータ（デバッグ用）

            _viewX = 0;     // 視界X
            _viewY = 0;     // 視界Y

            _actionCommandList.Clear();
            _buildInfoList.Clear();
            _moveInfoList.Clear();
            _techInfoList.Clear();

            _commandIndex = 0;

            _playerInfoList.Clear();
            for (int i = 0; i < Constants.MaxPlayer; i++)
            {
                var item = new PlayerInfo();

                item.Number = i + 1;

                _playerInfoList.Add(item);
            }

            return true;
        }

        /// <summary>
        /// タイマー開始
        /// </summary>
        private void StartTimer()
        {
            if (_timer != null)
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// タイマー終了
        /// </summary>
        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// タイマーリセット
        /// </summary>
        private void ResetTimer()
        {
            StopTimer();

            _gameTime = 0;
            _toolLocalTime = 0;
            _finalTime = 0;

            StartTimer();
        }

        public void DoPlay(string replayFilePath, string myselfName)
        {
            if (_pythonRunning)
            {
                PythonProssKill();

                Thread.Sleep(1000);
            }

            DoStop();
            InitData(myselfName);

            Speech.SpeechStart();
            StartAnalysis(replayFilePath);
        }

        public void DoStop()
        {
            StopTimer();
        }

        /// <summary>
        /// AOE2DEの各種ログ取得メイン処理
        /// </summary>
        private void StartAnalysis(string replayFilePath)
        {
            Speech.Talk("読み込みを開始します").Forget();

            var psInfo = new ProcessStartInfo
            {
                FileName = Constants.PYTHON_CMD_NAME,
                Arguments = @$"""{Environment.GetEnvironmentVariable(Constants.KEY_ENV_PYTHON_GET_DATA_PROC)!}"" ""{replayFilePath}"" ""{Constants.PYTHON_OUTPUT_DATAPATH}""",
                CreateNoWindow = true,                 // コンソール・ウィンドウを開かない
                UseShellExecute = false,               // シェル機能を使用しない
                RedirectStandardOutput = true,         // 標準出力をリダイレクト
                RedirectStandardError = true
            };
            _pythonProcProcess = Process.Start(psInfo); // アプリの実行開始
            if(_pythonProcProcess is null)
            {
                Console.WriteLine("エラー：Pythonプロセスの取得エラー");

                return;
            }

            _pythonProcProcess.OutputDataReceived += OutputDataReceived;
            _pythonProcProcess.ErrorDataReceived += ErrorDataReceived;
            _pythonProcProcess.BeginErrorReadLine();
            _pythonProcProcess.BeginOutputReadLine();

            _pythonRunning = true;
        }

        /// <summary>
        /// パイソンプロセス強制終了
        /// </summary>
        public void PythonProssKill()
        {
            StopTimer();

            _pythonProcProcess?.Kill();
            _pythonProcProcess?.Dispose();
            _pythonProcProcess = null;
            _pythonRunning = false;
        }

        public void PlayerUnitAdd(CommandDetail commandDetail)
        {
            if (commandDetail.ObjectIds is null)
            {
                Console.WriteLine("エラー：解析エラー");

                return;
            }

            var localUnitType = UnitType.None;
            AOE2DEConvert.ToLocalUnitType(commandDetail.UnitId, out localUnitType);
            UnitGroupType unitGroupType = UnitGroupType.None;
            AOE2DEConvert.ToLocalUnitGroupType(localUnitType, out unitGroupType);

            // ユニットカウンタ加算
            var unitTypeCount = _playerInfoList[commandDetail.PlayerId - 1].UnitInfoList.UnitTypeCount;
            if(!unitTypeCount.ContainsKey(localUnitType))
            {
                unitTypeCount.Add(localUnitType, new UnitInfoItem());
            }

            // ユニット生産された小屋オブジェクトID別に加算
            var upi = new UnitProductionInfo();
            upi.Amount = 1;
            upi.TechnologyId = 0;
            var popCompletionTime = (1000 * 25);    // 農民は25秒で生産

            var objectIds = unitTypeCount[localUnitType].ObjectIds;
            if (!objectIds.ContainsKey(commandDetail.ObjectIds[0]))
            {
                // キー無しの場合は新規登録
                var info = new List<UnitProductionInfo>();
                upi.CompletionTime = popCompletionTime;
                info.Add(upi);
                objectIds.Add(commandDetail.ObjectIds[0], info);
            }
            else
            {
                // キー有りの場合
                if (objectIds[commandDetail.ObjectIds[0]].Count > 0)
                {
                    var ss = objectIds[commandDetail.ObjectIds[0]][objectIds[commandDetail.ObjectIds[0]].Count - 1];
                    if(ss.CompletionTime > commandDetail.Sequence)
                    {
                        // 生産途中の物があれば最終キューのユニットが完成時間後から加算して登録
                        upi.CompletionTime = ss.CompletionTime + popCompletionTime;
                        objectIds[commandDetail.ObjectIds[0]].Add(upi);
                    }
                    else
                    {
                        // 生産途中の物が無ければ今の時間から加算して登録
                        upi.CompletionTime = commandDetail.Sequence + popCompletionTime;
                        objectIds[commandDetail.ObjectIds[0]].Add(upi);
                    }
                }
            }

            // ユニットグループカウンタ加算
            var unitGroupTypeCount = _playerInfoList[commandDetail.PlayerId - 1].UnitInfoList.UnitGroupTypeCount;
            if(!unitGroupTypeCount.ContainsKey(unitGroupType))
            {
                // キー無し
                unitGroupTypeCount.Add(unitGroupType, 0);
            }
            unitGroupTypeCount[unitGroupType] += commandDetail.Amount;
        }

        public void PlayerTownCenterTechDevelopment(CommandDetail commandDetail)
        {
            // 機織り 00:06:09 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 22, 'object_ids': [8321], 'sequence': 369793}
            // 領主 00:01:10 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 101, 'object_ids': [8321], 'sequence': 70598}
            // 城主 00:02:31 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 102, 'object_ids': [8321], 'sequence': 151640}
            // 手押し車 00:04:26 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 213, 'object_ids': [8321], 'sequence': 266209}
            // 荷車 00:05:01 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 249, 'object_ids': [8321], 'sequence': 301140}
            // 見張り 00:05:26 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 8, 'object_ids': [8321], 'sequence': 326529}
            // 巡回 00:06:46 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 280, 'object_ids': [8321], 'sequence': 406154}
            // 帝王 00:07:21 ACTION;Action.RESEARCH;{'player_id': 1, 'technology_id': 103, 'object_ids': [8321], 'sequence': 441709}


            // 処理対象かどうか
            var isReturn = true;
            // 開発にかかる秒数
            var techTime = 0;
            // 町の中心での開発の為、種別を農民に合わせ並行しないようにする
            var localUnitType = UnitType.Pop;

            var myselfPlayerInfo = _playerInfoList.Where((a) => a.IsMyself).FirstOrDefault();
            var targetPlayerInfo = _playerInfoList.Where((a) => a.PlayerId == commandDetail.player_id).FirstOrDefault();
            if (myselfPlayerInfo is null || targetPlayerInfo is null) return;

            switch (commandDetail.TechnologyId)
            {
                case 8:            // 見張り
                    techTime = 25;
                    isReturn = false;
                    break;
                case 22:            // 機織り
                    techTime = 25;
                    isReturn = false;
                    break;
                case 101:            // 領主
                    techTime = 130;
                    isReturn = false;
                    break;
                case 102:            // 城主
                    techTime = 160;
                    isReturn = false;
                    break;
                case 103:            // 帝王
                    techTime = 190;
                    isReturn = false;
                    break;
                case 213:            // 手押し車
                    techTime = 75;
                    isReturn = false;
                    break;
                case 249:            // 荷車
                    techTime = 55;
                    isReturn = false;
                    break;
                case 280:            // 巡回
                    techTime = 40;
                    isReturn = false;
                    break;
            }

            if (isReturn)
            {
                // 対象外
                return;
            }

            if(commandDetail is null || commandDetail.ObjectIds is null)
            {
                Console.WriteLine("エラー：コマンドデータなし");

                return;
            }

            // ユニットカウンタ加算
            var unitTypeCount = _playerInfoList[commandDetail.PlayerId - 1].UnitInfoList.UnitTypeCount;
            if (!unitTypeCount.ContainsKey(localUnitType))
            {
                unitTypeCount.Add(localUnitType, new UnitInfo.UnitInfoItem());
            }

            // ユニット生産された小屋オブジェクトID別に加算
            var upi = new UnitProductionInfo();
            upi.Amount = 0;
            upi.TechnologyId = commandDetail.TechnologyId;
            var popCompletionTime = (1000 * techTime);    // 機織りは25秒で開発

            var objectIds = unitTypeCount[localUnitType].ObjectIds;
            if (!objectIds.ContainsKey(commandDetail.ObjectIds[0]))
            {
                // キー無しの場合は新規登録

                var a = new List<UnitProductionInfo>();
                upi.CompletionTime = popCompletionTime;
                a.Add(upi);
                objectIds.Add(commandDetail.ObjectIds[0], a);
            }
            else
            {
                // キー有りの場合
                if (objectIds[commandDetail.ObjectIds[0]].Count > 0)
                {
                    var ss = objectIds[commandDetail.ObjectIds[0]][objectIds[commandDetail.ObjectIds[0]].Count - 1];
                    if (ss.CompletionTime > commandDetail.Sequence)
                    {
                        // 生産途中の物があれば最終キューのユニットが完成時間後から加算して登録
                        upi.CompletionTime = ss.CompletionTime + popCompletionTime;
                        objectIds[commandDetail.ObjectIds[0]].Add(upi);
                    }
                    else
                    {
                        // 生産途中の物が無ければ今の時間から加算して登録
                        upi.CompletionTime = commandDetail.Sequence + popCompletionTime;
                        objectIds[commandDetail.ObjectIds[0]].Add(upi);
                    }
                }
            }
        }

        private float GetDistanceSquared(float x1, float y1, float x2, float y2)
        {
            float dx = x1 - x2;
            float dy = y1 - y2;
            return dx * dx + dy * dy;
        }

        public bool DetermineIfFrontline(PlayerInfo playerInfo)
        {
            if (playerInfo == null) return false;

            float myX = playerInfo.Posion.X;
            float myY = playerInfo.Posion.Y;

            // 敵プレイヤーを抽出
            var enemyList = _playerInfoList
                .Where(p => p.TeamId != playerInfo.TeamId)
                .ToList();

            if (enemyList.Count == 0) return false;

            // 味方（自分含む）
            var allyList = _playerInfoList
                .Where(p => p.TeamId == playerInfo.TeamId)
                .ToList();

            // 各プレイヤーと最近敵との距離を計算し、昇順でソート
            var sortedByThreatDistance = allyList
                .OrderBy(p =>
                {
                    var closestEnemy = enemyList
                        .OrderBy(e => GetDistanceSquared(p.Posion.X, p.Posion.Y, e.Posion.X, e.Posion.Y))
                        .First();
                    return GetDistanceSquared(p.Posion.X, p.Posion.Y, closestEnemy.Posion.X, closestEnemy.Posion.Y);
                })
                .ToList();

            // ソート後、味方の中で自分が何番目に敵に近いかを取得
            int myIndex = sortedByThreatDistance.FindIndex(p => p.PlayerId == playerInfo.PlayerId);

            // 2人以上なら、近い方2人を前衛と見なす
            bool isFront = (myIndex <= 1);

            return isFront;
        }

        /// <summary>
        /// 指定されたプレイヤーに対して、距離が近い順に敵を並べて、
        /// 上位N人を前衛、それ以降を後衛として分類する。
        /// </summary>
        /// <param name="targetPlayer">対象プレイヤー</param>
        /// <param name="numFront">前衛とみなす人数（通常は2）</param>
        /// <returns>(前衛敵リスト, 後衛敵リスト)</returns>
        public (List<PlayerInfo> FrontEnemies, List<PlayerInfo> BackEnemies) GetFrontBackEnemiesForPlayerOld(PlayerInfo targetPlayer, int numFront = 2)
        {
            if (targetPlayer == null) return (new List<PlayerInfo>(), new List<PlayerInfo>());

            float myX = targetPlayer.Posion.X;
            float myY = targetPlayer.Posion.Y;

            // 敵チームのプレイヤー取得
            var enemyList = _playerInfoList
                .Where(p => p.TeamId != targetPlayer.TeamId)
                .ToList();

            if (enemyList.Count == 0) return (new List<PlayerInfo>(), new List<PlayerInfo>());

            // 敵との距離でソート
            var sortedEnemies = enemyList
                .OrderBy(e => GetDistanceSquared(myX, myY, e.Posion.X, e.Posion.Y))
                .ToList();

            var frontEnemies = sortedEnemies.Take(numFront).ToList();
            var backEnemies = sortedEnemies.Skip(numFront).ToList();

            return (frontEnemies, backEnemies);
        }

        public (List<PlayerInfo> FrontEnemies, List<PlayerInfo> BackEnemies) GetFrontBackEnemiesFromMyFrontline(PlayerInfo myself, int numFront = 2)
        {
            // 自分の前衛を取得（自分含む）
            var frontlineReference = DetermineIfFrontline(myself)
                ? myself
                : GetSideAllyFrontOrBack(myself); // 自分が後衛なら前の味方を取得

            if (frontlineReference is null)
                return (new(), new());

            float fx = frontlineReference.Posion.X;
            float fy = frontlineReference.Posion.Y;

            // 敵リスト
            var enemyList = _playerInfoList
                .Where(p => p.TeamId != myself.TeamId)
                .ToList();

            var sortedEnemies = enemyList
                .OrderBy(e => GetDistanceSquared(fx, fy, e.Posion.X, e.Posion.Y))
                .ToList();

            var frontEnemies = sortedEnemies.Take(numFront).ToList();
            var backEnemies = sortedEnemies.Skip(numFront).ToList();

            return (frontEnemies, backEnemies);
        }


        public List<PlayerInfo> GetAlliesOnMySide(PlayerInfo myself, int count = 2)
        {
            return _playerInfoList
                .Where(p => p.TeamId == myself.TeamId && p.PlayerId != myself.PlayerId)
                .OrderBy(p => GetDistanceSquared(p.Posion.X, p.Posion.Y, myself.Posion.X, myself.Posion.Y))
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// 指定された味方リストに対して、敵との距離が近い順に前衛・後衛に分ける
        /// </summary>
        /// <param name="allies">対象プレイヤー群（自分含む）</param>
        /// <param name="numFront">前衛とみなす人数</param>
        /// <returns>(前衛リスト, 後衛リスト)</returns>
        public (List<PlayerInfo> FrontList, List<PlayerInfo> BackList) GetFrontBackAllies(List<PlayerInfo> allies, int numFront = 2)
        {
            // 全員の中で最も近い敵との距離を計算して昇順ソート
            var enemyList = _playerInfoList.Where(p => p.TeamId != allies.First().TeamId).ToList();

            var sortedAllies = allies
                .OrderBy(a =>
                {
                    var nearestEnemy = enemyList
                        .OrderBy(e => GetDistanceSquared(a.Posion.X, a.Posion.Y, e.Posion.X, e.Posion.Y))
                        .FirstOrDefault();

                    return nearestEnemy != null
                        ? GetDistanceSquared(a.Posion.X, a.Posion.Y, nearestEnemy.Posion.X, nearestEnemy.Posion.Y)
                        : float.MaxValue;
                })
                .ToList();

            var frontList = sortedAllies.Take(numFront).ToList();
            var backList = sortedAllies.Skip(numFront).ToList();

            return (frontList, backList);
        }

        public PlayerInfo? GetSideAllyFrontOrBack(PlayerInfo myself, int numFront = 1)
        {
            var sideAllies = GetAlliesOnMySide(myself, count: 2);
            if (sideAllies.Count == 0) return null;

            var (frontList, backList) = GetFrontBackAllies(sideAllies.Append(myself).ToList(), numFront);

            if (DetermineIfFrontline(myself))
            {
                // 自分が前衛 → 自分の後ろの味方を返す
                return backList.FirstOrDefault();
            }
            else
            {
                // 自分が後衛 → 自分の前の味方を返す
                return frontList.FirstOrDefault();
            }
        }


        async Task StartSpeech()
        {
            var myselfPlayerInfo = _playerInfoList.FirstOrDefault(a => a.IsMyself);
            if (myselfPlayerInfo is not null && myselfPlayerInfo.IsFrontline)
            {
                await Speech.Talk("前衛です");
            }
            else
            {
                await Speech.Talk("後衛です");
            }

            if (myselfPlayerInfo is not null)
            {
                var frontEnemyColorSpeak = myselfPlayerInfo.FrontEnemy.GetColorSpeak();
                var backEnemyColorSpeak = myselfPlayerInfo.BackEnemy.GetColorSpeak();

                await Speech.Talk($"敵前衛は{frontEnemyColorSpeak}です");
                await Speech.Talk($"敵後衛は{backEnemyColorSpeak}です");
            }
        }

        public (PlayerInfo? sneakyPlayerInfo, PlayerInfo? allyPlayerInfo) IsEnemyForwardBuildingNearTeamCenter(PlayerInfo myself, float rangeThreshold = 50f)
        {
            if (myself == null) return (null, null);

            // 味方全員（自分含む）
            var teamMembers = _playerInfoList.Where(p => p.TeamId == myself.TeamId).ToList();

            // 敵プレイヤーのIDリスト（敵建物かどうかのフィルタに使用）
            var enemyPlayerIds = _playerInfoList
                .Where(p => p.TeamId != myself.TeamId)
                .Select(p => p.PlayerId)
                .ToHashSet();

            // 軍事系建物ID（必要に応じて調整）
            var forwardBuildingIds = new HashSet<long>
            {
                12, // バラック
                14, // 射手小屋
                19, // 厩舎
                82, // 前哨
                // 必要に応じて追加
            };

            foreach (var bi in _buildInfoList)
            {
                if (!enemyPlayerIds.Contains(bi.PlayerId)) continue;
                if (!forwardBuildingIds.Contains(bi.BuildingId)) continue;

                // 味方全員の町の中心座標との距離をチェック
                foreach (var allyPlayerInfo in teamMembers)
                {
                    float dist2 = GetDistanceSquared(allyPlayerInfo.Posion.X, allyPlayerInfo.Posion.Y, bi.X, bi.Y);
                    if (dist2 < rangeThreshold * rangeThreshold)
                    {
                        var sneakyPlayerInfo = _playerInfoList.Where(p => p.Number == bi.PlayerId).First();
                        return (sneakyPlayerInfo, allyPlayerInfo);
                    }
                }
            }

            return (null, null);
        }


        public void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                // データなし
                return;
            }

            var timeStr = "XX:XX:XX";
            var dataStr = e.Data;
            var number = 0;
            var dataList = dataStr?.Split(";");
            if(dataList is null)
            {
                Console.WriteLine(@"エラー：解析エラー\n{dataStr}");

                return;
            }

            var isTeamsError = false;

            var command = "";
            switch (dataList[0])
            {
                case "HEADERSTART":
                    ResetTimer();
                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "HEADEREND":
                    StartTimer();
                    _log += $"{timeStr} {dataStr}\r\n";

                    foreach (var playerInfo in _playerInfoList)
                    {
                        var (frontEnemies, backEnemies) = GetFrontBackEnemiesFromMyFrontline(playerInfo, numFront: 1);
                        playerInfo.FrontEnemy = frontEnemies.FirstOrDefault() ?? new();
                        playerInfo.FrontEnemyName = frontEnemies.FirstOrDefault()?.PlayerName ?? "";
                        playerInfo.BackEnemy = backEnemies.FirstOrDefault() ?? new();
                        playerInfo.BackEnemyName = backEnemies.FirstOrDefault()?.PlayerName ?? "";
                    }

                    foreach (var playerInfo in _playerInfoList)
                    {
                        playerInfo.IsFrontline = DetermineIfFrontline(playerInfo);
                    }

                    foreach (var playerInfo in _playerInfoList)
                    {
                        var sideAlly = GetSideAllyFrontOrBack(playerInfo);
                        playerInfo.SideAlly = sideAlly ?? new();
                        playerInfo.SideAllyName = sideAlly?.PlayerName ?? "";
                    }

                    StartSpeech().Forget();

                    InvokeStartCallbacks();

                    break;

                case "MAPSEND":
                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "VIEWLOCK":
                    command = dataList[0];

                    var viewX = float.Parse(dataList[1]);
                    var viewY = float.Parse(dataList[2]);
                    _actionCommandList.Add(new ActionCommand { GameTime = null, Command = command, CommandDetail = new CommandDetail(), X = viewX, Y = viewY });
                    //_log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "TEAMS":

                    var posionX = 0f;
                    var posionY = 0f;
                    if (dataList[7] == TAG_POSION_NO_DATA || dataList[8] == TAG_POSION_NO_DATA)
                    {
                        if (!isTeamsError)
                        {
                            isTeamsError = true;
                            Speech.Talk($"チーム拠点位置の解析エラーです。無視します").Forget();
                        }
                    }
                    else
                    {
                        posionX = float.Parse(dataList[7]);
                        posionY = float.Parse(dataList[8]);
                    }

                    number = int.Parse(dataList[1]) - 1;
                    _playerInfoList[number].TeamId = int.Parse(dataList[2]);
                    _playerInfoList[number].PlayerId = int.Parse(dataList[3]) + 1;
                    _playerInfoList[number].ColorName = dataList[4];
                    _playerInfoList[number].CivilizationId = int.Parse(dataList[5]);
                    _playerInfoList[number].CivilizationName = dataList[6];
                    _playerInfoList[number].Posion.X = posionX;
                    _playerInfoList[number].Posion.Y = posionY;
                    _playerInfoList[number].PlayerName = System.Text.RegularExpressions.Regex.Unescape(dataList[9]);

                    if(_playerInfoList[number].PlayerName == _myselfName)
                    {
                        _playerInfoList[number].IsMyself = true;
                    }

                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "ACTION":
                    // Action.MOVE;{'player_id': 2, 'object_ids': [], 'x': 39.85416793823242, 'y': 108.9375, 'sequence': 806206}
                    // Action.DE_TRIBUTE;{'player_id': 2, 'target_player_id': b'\x01', 'food': 0.0, 'wood': 0.0, 'stone': 0.0, 'gold': 0.0, 'sequence': 1216361}
                    // Action.RESEARCH;{'player_id': 1, 'technology_id': 22, 'object_ids': [8765], 'sequence': 13494}
                    command = dataList[1];

                    // JSON変換
                    var str = dataList[2];
                    str = str.Replace(": b", ": ");
                    str = str.Replace("\\x", "");
                    str = str.Replace("'", @"""");
                    CommandDetail? commandDetail = JsonSerializer.Deserialize<CommandDetail>(str);
                    if(commandDetail is not null)
                    {
                        _actionCommandList.Add(new ActionCommand { GameTime = commandDetail.Sequence, Command = command, CommandDetail = commandDetail });
                        timeStr = commandDetail.GetGameTimeStr();
                        _finalTime = commandDetail.sequence;
                    }
                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "CHAT":
                    command = dataList[0];

                    _actionCommandList.Add(new ActionCommand { GameTime = null, Command = command, CommandDetail = new CommandDetail() });
                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "MAP":
                    command = dataList[0];

                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                case "POSTGAME":
                    command = dataList[0];

                    _actionCommandList.Add(new ActionCommand { GameTime = null, Command = command, CommandDetail = new CommandDetail() });
                    _log += $"{timeStr} {dataStr}\r\n";
                    break;

                default:
                    _log += $"{timeStr} {dataStr}\r\n";
                    break;
            }
        }

        public string GetGameTimeStr()
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)_gameTime);

            return ts.ToString(@"hh\:mm\:ss");
        }

        public void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            _log += "ERROR>" + e.Data + "\r\n";
            if(!_isError)
            {
                _isError = true;
                Speech.Talk($"解析エラーです。停止します").Forget();
            }
        }
    }
}
