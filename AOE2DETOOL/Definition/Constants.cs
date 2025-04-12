namespace AOE2DETOOL.Definition
{
    static internal class Constants
    {
        public const int MaxPlayer = 8;
        public const double ElapsedTimeInterval = 1000;
        // リプレイファイル格納ディレクトリ
        public const string REPLAY_DIR = @"C:\Users\yoshi\Games\Age of Empires 2 DE\76561198017687074\savegame\";

        public const string KEY_ENV_OPENAI = "OPENAI_API_KEY";
        public const string KEY_ENV_AZURE_SPEECH = "AZURE_SPEECH_KEY";
        public const string KEY_ENV_AZURE_SPEECH_REGION = "AZURE_SPEECH_REGION";
        public const string KEY_ENV_AZURE_SPEECH_VOICE = "AZURE_SPEECH_VOICE";

        // パイソンコマンド
        public const string PYTHON_CMD_NAME = "python";
        // データ取得パイソン処理
        public const string PYTHON_GET_DATA_PROC = @".\Python\AOE2DEReplayDataConvertCore.py";
        // データ受け渡し用出力ファイル
        public const string PYTHON_OUTPUT_DATAPATH = @"out1.txt";

    }
}
