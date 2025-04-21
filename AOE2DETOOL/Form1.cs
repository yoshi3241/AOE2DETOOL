using AOE2DETOOL.Definition;
using AOE2DETOOL.Extensions;
using AOE2DETOOL.Models.Data;
using AOE2DETOOL.Models.Logic;
using AOE2DETOOL.Utilities;
using System.Data;
using System.Text;
using static AOE2DETOOL.Definition.Enums;

namespace AOE2DETOOL
{
    public partial class Form1 : Form
    {
        private List<UserControl1> uc1 = new List<UserControl1>();          // プレイヤー詳細情報配列
        private List<FileInfo> _fileList = new List<FileInfo>();            // 表示ファイル一覧
        private AOE2DEDataProcess _aOE2DEDataProcess = new AOE2DEDataProcess();

        public Form1()
        {
            InitializeComponent();

            // ユーザー詳細情報配列セット
            uc1.Add(this.userControl11);
            uc1.Add(this.userControl12);
            uc1.Add(this.userControl13);
            uc1.Add(this.userControl14);
            uc1.Add(this.userControl15);
            uc1.Add(this.userControl16);
            uc1.Add(this.userControl17);
            uc1.Add(this.userControl18);

            // Exitイベントハンドラを追加
            Application.ApplicationExit += new EventHandler(ApplicationExitEvent);
        }

        Aoe2deOverlayForm _overlayForm;
        Aoe2deControllForm _controllForm;

        private void Form1_Load(object sender, EventArgs e)
        {
            // ビュー初期化関連
            InitView(Environment.GetEnvironmentVariable(Constants.KEY_ENV_REPLAY_DIR)!);

            _overlayForm = new Aoe2deOverlayForm();
            _overlayForm.InitData(this, _aOE2DEDataProcess);
            _overlayForm.Show();

            _controllForm = new Aoe2deControllForm();
            _controllForm.InitData(this, _overlayForm, _aOE2DEDataProcess);
            _controllForm.Show();
        }

        /// <summary>
        /// ファイル一覧表示
        /// </summary>
        /// <param name="dir"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        private void FileListInit(string dir)
        {
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Directory not found. path={dir}");

            DirectoryInfo di = new DirectoryInfo(dir);

            this.listBox1.Items.Clear();
            _fileList = di.GetFiles("*.*").OrderBy(f => f.LastWriteTime).Reverse().ToList();

            foreach(var item in _fileList)
            {
                this.listBox1.Items.Add($"{item.Name} {item.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss")}");
            }

            this.listBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// ビュー初期化
        /// </summary>
        /// <param name="dir"></param>
        private void InitView(string dir)
        {
            textGameTime.Text = "";

            // ファイル一覧初期化
            InitFileListView(dir);

            // ユーザー情報初期化
            InitPlayerInfoView(dir);
        }

        public void FileListUpdate()
        {
            InitView(Environment.GetEnvironmentVariable(Constants.KEY_ENV_REPLAY_DIR)!);
        }

        /// <summary>
        /// ファイル一覧初期化
        /// </summary>
        /// <param name="dir"></param>
        private void InitFileListView(string dir)
        {
            // ファイル一覧初期化
            FileListInit(dir);
        }

        /// <summary>
        /// ユーザー情報初期化
        /// </summary>
        /// <param name="dir"></param>
        private void InitPlayerInfoView(string dir)
        {
            // ユーザー情報初期化
            for (int index = 0; index < 8; index++)
            {
                uc1[index].groupBox1.Text = "";

                uc1[index].textColor.BackColor = Color.White;

                // 農民バー
                uc1[index].progressBar1.Value = 0;
                // 荷馬車バー
                uc1[index].progressBar2.Value = 0;
                // 漁船バー
                uc1[index].progressBar3.Value = 0;

                uc1[index].textPop.Text = "0";              // 農民
                uc1[index].textWagon.Text = "0";            // 荷馬車
                uc1[index].textfFshing.Text = "0";          // 漁船


                uc1[index].textBowType.Text = "0";               // 
                uc1[index].textSkirmisherType.Text = "0";               // 
                uc1[index].textSpearType.Text = "0";               // 
                uc1[index].textWarriorType.Text = "0";               // 
                uc1[index].textHorseType.Text = "0";               // 
                uc1[index].textElephantType.Text = "0";               // 
                uc1[index].textCamelType.Text = "0";               // 
                uc1[index].textSiegeType.Text = "0";               // 
                uc1[index].textHaskarlType.Text = "0";               // 
                uc1[index].textClergymanType.Text = "0";               // 
                uc1[index].textScorpionType.Text = "0";               // 
            }
        }

        /// <summary>
        /// ApplicationExitイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationExitEvent(object sender, EventArgs e)
        {
            _aOE2DEDataProcess.PythonProssKill();
            
            //ApplicationExitイベントハンドラを削除
            Application.ApplicationExit -= new EventHandler(ApplicationExitEvent);
        }

        /// <summary>
        /// 指定したディレクトリの中から更新時刻が一番新しいファイルを取得
        /// </summary>
        /// <param name="dir">対象ディレクトリ</param>
        /// <param name="compare"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        static public string GetLatestFileCore(string dir, Func<string, string, bool> compare)
        {
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Directory not found. path={dir}");

            string file = "";
            foreach (string path in Directory.GetFiles(dir))
            {
                if (string.IsNullOrEmpty(file))
                {
                    file = path;
                }
                else
                {
                    if (compare(path, file))
                    {
                        file = path;
                    }
                }
            }
            return file;
        }

        /// <summary>
        /// 指定したディレクトリの中から更新時刻が一番新しいファイルを取得
        /// </summary>
        /// <returns>
        /// 最新のファイルパス。
        /// ただしディレクトリにファイルが1つも無ければ空文字
        /// </returns>
        public string GetLatestFile(string dir)
        {
            var filePath = _fileList[listBox1.SelectedIndex].FullName;

            return filePath;
        }

        public string GetSelectedFile(string dir)
        {
            var filePath = _fileList[listBox1.SelectedIndex].FullName;

            return filePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoPlay();
        }

        public void DoPlay()
        {
            var replayFilePath = GetSelectedFile(Environment.GetEnvironmentVariable(Constants.KEY_ENV_REPLAY_DIR)!);
            textBox1.Text = "";
            textGameTime.Text = "";

            InitPlayerInfoView(Environment.GetEnvironmentVariable(Constants.KEY_ENV_REPLAY_DIR)!);

            _aOE2DEDataProcess.DoPlay(replayFilePath, textMyselfName.Text);

            _overlayForm.StartOverlay();
            _controllForm.StartOverlay();
        }

        private void TechInfoProc()
        {
            lock (AOE2DEDataProcess.TechInfoDataLock)
            {
                if (_aOE2DEDataProcess is null || _aOE2DEDataProcess.BildInfoList.Count == 0)
                {
                    return;
                }

                if(_aOE2DEDataProcess.PlayerInfoList is null)
                {
                    return;
                }

                var playerInfoList = _aOE2DEDataProcess.PlayerInfoList;
                var techInfos = _aOE2DEDataProcess.TechInfoList.Where(a => !a.IsUnprocessed && a.Sequence < _aOE2DEDataProcess.GameTime);

                if (!techInfos.Any())
                {
                    return;
                }

                foreach (var techInfo in techInfos)
                {
                    // 開発にかかる秒数
                    var techTime = 0;

                    techInfo.IsUnprocessed = true;

                    var myselfPlayerInfo = playerInfoList.Where((a) => a.IsMyself).FirstOrDefault();
                    var targetPlayerInfo = playerInfoList.Where((a) => a.Number == techInfo.PlayerId).FirstOrDefault();
                    if (myselfPlayerInfo is null || targetPlayerInfo is null) return;

                    switch (techInfo.TechnologyId)
                    {
                        case 8:            // 見張り
                            break;
                        case 22:            // 機織り
                            break;
                        case 101:            // 領主
                            break;
                        case 102:            // 城主
                            break;
                        case 103:            // 帝王
                            if (myselfPlayerInfo.TeamId != targetPlayerInfo.TeamId)
                            {
                                if (!targetPlayerInfo.AlertEmperorAttack)
                                {
                                    targetPlayerInfo.AlertEmperorAttack = true;
                                    var speakColor = targetPlayerInfo.GetColorSpeak();

                                    if (targetPlayerInfo.CivilizationName == Civilization.Turks && techInfo.Sequence < (60000 * 25))
                                    {
                                        Speech.Talk($"{speakColor}が即時帝王を行いました").Forget();
                                    }
                                    else if (techInfo.Sequence < (60000 * 31))
                                    {
                                        Speech.Talk($"{speakColor}が帝王に移行しました").Forget();
                                    }
                                }
                            }

                            break;
                        case 213:            // 手押し車
                            break;
                        case 249:            // 荷車
                            break;
                        case 280:            // 巡回
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 画面更新用タイマー（スレッドセーフのためタイマーで処理）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (_aOE2DEDataProcess.Log != "")
            {
                textBox1.Text += _aOE2DEDataProcess.Log;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                _aOE2DEDataProcess.ClearLog();
            }

            textGameTime.Text = _aOE2DEDataProcess.GetGameTimeStr();

            // コールバックでの書き代わり不一致を防ぐためコピー
            var playerInfoList = _aOE2DEDataProcess.PlayerInfoList.ToArray();

            var myselfInfoList = playerInfoList.Where((a) => a.IsMyself).FirstOrDefault();

            var index = 0;
            StringBuilder sb = new StringBuilder();
            for(int i = 1; i < Constants.MaxPlayer; i ++)
            {
                foreach (var item in playerInfoList.Where((a) => a.TeamId == i))
                {
                    var civilizationName = Civilization.GetNameByCode(item.CivilizationName);
                    var nameText = $"{item.PlayerId} {item.PlayerName} ({civilizationName})";
                    if (uc1[index].groupBox1.Text != nameText)
                    {
                        uc1[index].groupBox1.Text = nameText;
                    }

                    sb.Append($"番号:{item.Number} ");
                    sb.Append($"チーム:{item.TeamId} ");
                    sb.Append($"名前:{item.PlayerName} {(item.IsFrontline ? "(前)" : "(後)")}");
                    sb.Append($"色:{item.ColorName} ");
                    sb.Append($"文明:{item.CivilizationName} ");
                    sb.Append("\r\n");

                    var iut = item.UnitInfoList.UnitGroupTypeCount;

                    sb.Append($"農系:{iut.GetOrDefault(UnitGroupType.PopType)} ");
                    sb.Append($"弓系:{iut.GetOrDefault(UnitGroupType.BowType)} ");
                    sb.Append($"散系:{iut.GetOrDefault(UnitGroupType.SkirmisherType)} ");
                    sb.Append($"槍系:{iut.GetOrDefault(UnitGroupType.SpearType)} ");
                    sb.Append($"戦系:{iut.GetOrDefault(UnitGroupType.WarriorType)} ");
                    sb.Append($"騎系:{iut.GetOrDefault(UnitGroupType.HorseType)} ");
                    sb.Append($"ゾ系:{iut.GetOrDefault(UnitGroupType.ElephantType)} ");
                    sb.Append($"ら系:{iut.GetOrDefault(UnitGroupType.CamelType)} ");
                    sb.Append($"攻系:{iut.GetOrDefault(UnitGroupType.SiegeType)} ");
                    sb.Append($"ハ系:{iut.GetOrDefault(UnitGroupType.HaskarlType)} ");
                    sb.Append($"聖系:{iut.GetOrDefault(UnitGroupType.ClergymanType)} ");
                    sb.Append($"ス系:{iut.GetOrDefault(UnitGroupType.ScorpionType)} ");
                    sb.Append("\r\n");
                    sb.Append("\r\n");

                    uc1[index].textColor.BackColor = item.GetColor();

                    uc1[index].textBowType.Text = iut.GetOrDefault(UnitGroupType.BowType).ToString();                   // 
                    uc1[index].textSkirmisherType.Text = iut.GetOrDefault(UnitGroupType.SkirmisherType).ToString();     // 
                    uc1[index].textSpearType.Text = iut.GetOrDefault(UnitGroupType.SpearType).ToString();               // 
                    uc1[index].textWarriorType.Text = iut.GetOrDefault(UnitGroupType.WarriorType).ToString();           // 
                    uc1[index].textHorseType.Text = iut.GetOrDefault(UnitGroupType.HorseType).ToString();               // 
                    uc1[index].textElephantType.Text = iut.GetOrDefault(UnitGroupType.ElephantType).ToString();         // 
                    uc1[index].textCamelType.Text = iut.GetOrDefault(UnitGroupType.CamelType).ToString();               // 
                    uc1[index].textSiegeType.Text = iut.GetOrDefault(UnitGroupType.SiegeType).ToString();               // 
                    uc1[index].textHaskarlType.Text = iut.GetOrDefault(UnitGroupType.HaskarlType).ToString();           // 
                    uc1[index].textClergymanType.Text = iut.GetOrDefault(UnitGroupType.ClergymanType).ToString();       // 
                    uc1[index].textScorpionType.Text = iut.GetOrDefault(UnitGroupType.ScorpionType).ToString();         // 

                    if (!item.AlertRush && myselfInfoList?.TeamId != item.TeamId)
                    {
                        if (_aOE2DEDataProcess.GameTime < (60000 * 18))
                        {
                            // 城主ラッシュ
                            var type = "";
                            if (iut.GetOrDefault(UnitGroupType.WarriorType) > 2) type = "戦士";
                            if (iut.GetOrDefault(UnitGroupType.BowType) > 2) type = "弓";
                            if (iut.GetOrDefault(UnitGroupType.HorseType) > 2) type = "馬";

                            if (type != "")
                            {
                                item.AlertRush = true;
                                var speakColor = item.GetColorSpeak();
                                Speech.Talk($"{speakColor}が{type}でラッシュ警告です").Forget();
                            }
                        }
                        else if (_aOE2DEDataProcess.GameTime < (60000 * 31))
                        {
                            // 帝王ラッシュ
                            var type = "";
                            if (iut.GetOrDefault(UnitGroupType.WarriorType) > 5) type = "戦士";
                            if (iut.GetOrDefault(UnitGroupType.BowType) > 5) type = "弓";
                            if (iut.GetOrDefault(UnitGroupType.HorseType) > 5) type = "馬";

                            if (type != "")
                            {
                                item.AlertRush = true;
                                var speakColor = item.GetColorSpeak();
                                Speech.Talk($"{speakColor}が{type}でラッシュ警告です").Forget();
                            }
                        }
                    }

                    // 1/3 17:30
                    var unitInfoListUnitCount = playerInfoList[item.Number - 1].UnitInfoList.UnitTypeCount.ToArray();
                    foreach (var unitCount in unitInfoListUnitCount)
                    {
                        var unitType = unitCount.Key;
                        var unitInfoItem = unitCount.Value;

                        var count = 0;

                        foreach(var aa in unitInfoItem.ObjectIds.ToArray())
                        {
                            foreach(var cc in aa.Value.ToArray())
                            {
                                if(cc.CompletionTime <= _aOE2DEDataProcess.ToolLocalTime)
                                {
                                    count += cc.Amount;
                                }
                            }
                        }

//                        var count = unitInfoItem.ObjectIds.Where((a) => a.Value.Select(_completionTime) < _pythonCall._gameTime).Sum(x => x.Value.Count);
                        count = count < 0 ? 0 : count;

                        // ビュー反映
                        var pop = 0;
                        var wagon = 0;
                        var fishingBoat = 0;
                        switch (unitType)
                        {
                            case UnitType.Pop:
                                pop = count;
                                // 農民バー
                                uc1[index].progressBar1.Value =
                                    (int)((pop < 200 ? (float)pop / 200f : 1) * 100);
                                uc1[index].textPop.Text = pop.ToString();               // 農民
                                break;
                            case UnitType.Wagon:
                                wagon = count;
                                // 荷馬車バー
                                uc1[index].progressBar2.Value =
                                    (int)((wagon < 100 ? (float)wagon / 100f : 1) * 100);
                                uc1[index].textWagon.Text = wagon.ToString();           // 荷馬車
                                break;
                            case UnitType.FishingBoat:
                                fishingBoat = count;
                                // 漁船バー
                                uc1[index].progressBar3.Value =
                                    (int)((fishingBoat < 100 ? (float)fishingBoat / 100f : 1) * 100);
                                uc1[index].textfFshing.Text = fishingBoat.ToString();   // 漁船
                                break;
                        }
                    }

                    index++;

                }
            }
            textBox2.Text = sb.ToString();

            TechInfoProc();

            timer1.Start();
        }


        private void userControl11_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InitView(Environment.GetEnvironmentVariable(Constants.KEY_ENV_REPLAY_DIR)!);
        }
    }
}