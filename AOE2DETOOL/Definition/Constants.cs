namespace AOE2DETOOL.Definition
{
    static internal class Constants
    {
        public const int MaxPlayer = 8;
        public const double ElapsedTimeInterval = 1000;

        public const string KEY_ENV_OPENAI = "OPENAI_API_KEY";
        public const string KEY_ENV_AZURE_SPEECH = "AZURE_SPEECH_KEY";
        public const string KEY_ENV_AZURE_SPEECH_REGION = "AZURE_SPEECH_REGION";
        public const string KEY_ENV_AZURE_SPEECH_VOICE = "AZURE_SPEECH_VOICE";
        public const string KEY_ENV_REPLAY_DIR = "REPLAY_DIR";
        public const string KEY_ENV_PYTHON_GET_DATA_PROC = "PYTHON_GET_DATA_PROC";

        // パイソンコマンド
        public const string PYTHON_CMD_NAME = "python";
        // データ受け渡し用出力ファイル
        public const string PYTHON_OUTPUT_DATAPATH = @"out1.txt";
    }
}
