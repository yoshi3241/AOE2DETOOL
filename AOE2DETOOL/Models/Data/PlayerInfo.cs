namespace AOE2DETOOL.Models.Data
{
    public class Posion
    {
        private float _x = 0;
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        private float _y = 0;
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }

    /// <summary>
    /// プレイヤー情報
    /// </summary>
    public class PlayerInfo
    {
        public PlayerInfo()
        {

        }

        private int _number = 0;
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private int _teamId = 0;
        public int TeamId
        {
            get { return _teamId; }
            set { _teamId = value; }
        }

        private int _playerId = 0;
        public int PlayerId
        {
            get { return _playerId; }
            set { _playerId = value; }
        }

        private string _colorName = "";
        public string ColorName
        {
            get { return _colorName; }
            set { _colorName = value; }
        }

        private int _civilizationId = 0;
        public int CivilizationId
        {
            get { return _civilizationId; }
            set { _civilizationId = value; }
        }

        private string _civilizationName = "";
        public string CivilizationName
        {
            get { return _civilizationName; }
            set { _civilizationName = value; }
        }

        private Posion _posion = new Posion();
        public Posion Posion
        {
            get { return _posion; }
            set { _posion = value; }
        }

        private string _playerName = "";
        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        private UnitInfo _unitInfoList = new UnitInfo();
        public UnitInfo UnitInfoList
        {
            get { return _unitInfoList; }
            set { _unitInfoList = value; }
        }

        private bool _isMyself = false;
        public bool IsMyself
        {
            get { return _isMyself; }
            set { _isMyself = value; }
        }

        private bool _alertRush = false;
        public bool AlertRush
        {
            get { return _alertRush; }
            set { _alertRush = value; }
        }

        private bool _alertEmperorAttack = false;
        public bool AlertEmperorAttack
        {
            get { return _alertEmperorAttack; }
            set { _alertEmperorAttack = value; }
        }

        private PlayerInfo _frontEnemy;
        public PlayerInfo FrontEnemy
        {
            get { return _frontEnemy; }
            set { _frontEnemy = value; }
        }

        private PlayerInfo _backEnemy;
        public PlayerInfo BackEnemy
        {
            get { return _backEnemy; }
            set { _backEnemy = value; }
        }

        private PlayerInfo _sideAlly;
        public PlayerInfo SideAlly
        {
            get { return _sideAlly; }
            set { _sideAlly = value; }
        }

        private string _sideAllyName = "";
        public string SideAllyName
        {
            get { return _sideAllyName; }
            set { _sideAllyName = value; }
        }

        private string _frontEnemyName = "";
        public string FrontEnemyName
        {
            get { return _frontEnemyName; }
            set { _frontEnemyName = value; }
        }

        private string _backEnemyName = "";
        public string BackEnemyName
        {
            get { return _backEnemyName; }
            set { _backEnemyName = value; }
        }

        private bool _isFrontline = false;
        public bool IsFrontline
        {
            get { return _isFrontline; }
            set { _isFrontline = value; }
        }

        private bool _isSneakyForward = false;
        public bool IsSneakyForward
        {
            get { return _isSneakyForward; }
            set { _isSneakyForward = value; }
        }

        public Color GetColor()
        {
            return ColorCodeToColor(_colorName);
        }

        public string GetColorSpeak()
        {
            return ColorCodeToSpeak(_colorName);
        }

        /// <summary>
        /// カラーコードから色変換
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        static public Color ColorCodeToColor(string colorName)
        {
            Color cl = new Color();
            switch (colorName)
            {
                case "Blue": // 青
                    cl = Color.Blue;
                    break;
                case "Red": // 赤
                    cl = Color.Red;
                    break;
                case "Green": // 緑
                    cl = Color.Green;
                    break;
                case "Yellow": // 黄
                    cl = Color.Yellow;
                    break;
                case "Teal": // 水色
                    cl = Color.Teal;
                    break;
                case "Purple": // 紫
                    cl = Color.Purple;
                    break;
                case "Gray": // グレー
                    cl = Color.Gray;
                    break;
                case "Orange": // 橙
                    cl = Color.Orange;
                    break;
                default:
                    cl = Color.Silver;
                    break;
            }

            return cl;
        }

        static public string ColorCodeToSpeak(string colorName)
        {
            string speakStr = "";
            switch (colorName)
            {
                case "Blue": // 青
                    speakStr = "ブルー";
                    break;
                case "Red": // 赤
                    speakStr = "レッド";
                    break;
                case "Green": // 緑
                    speakStr = "グリーン";
                    break;
                case "Yellow": // 黄
                    speakStr = "イエロー";
                    break;
                case "Teal": // 水
                    speakStr = "水色";
                    break;
                case "Purple": // 紫
                    speakStr = "パープル";
                    break;
                case "Gray": // グレー
                    speakStr = "グレー";
                    break;
                case "Orange": // 橙
                    speakStr = "オレンジ";
                    break;
                default:
                    break;
            }

            return speakStr;
        }
    }
}
