namespace AOE2DETOOL.Models.Data
{
    internal static class Civilization
    {
        public record CivilizationInfo(string Code, int Id, string NameJa);

        // 文明定数（Code / Id / NameJa）
        public const string Mongols = "Mongols"; public const int MongolsId = 1; public const string MongolsJa = "モンゴル";
        public const string Koreans = "Koreans"; public const int KoreansId = 2; public const string KoreansJa = "韓国";
        public const string Magyars = "Magyars"; public const int MagyarsId = 3; public const string MagyarsJa = "マジャール";
        public const string Franks = "Franks"; public const int FranksId = 4; public const string FranksJa = "フランク";
        public const string Huns = "Huns"; public const int HunsId = 5; public const string HunsJa = "フン";
        public const string Slavs = "Slavs"; public const int SlavsId = 6; public const string SlavsJa = "スラブ";
        public const string Persians = "Persians"; public const int PersiansId = 7; public const string PersiansJa = "ペルシア";
        public const string Turks = "Turks"; public const int TurksId = 8; public const string TurksJa = "トルコ";
        public const string Aztecs = "Aztecs"; public const int AztecsId = 9; public const string AztecsJa = "アステカ";
        public const string Berbers = "Berbers"; public const int BerbersId = 10; public const string BerbersJa = "ベルベル";
        public const string Incas = "Incas"; public const int IncasId = 11; public const string IncasJa = "インカ";
        public const string Goths = "Goths"; public const int GothsId = 12; public const string GothsJa = "ゴート";
        public const string Khmer = "Khmer"; public const int KhmerId = 13; public const string KhmerJa = "クメール";
        public const string Saracens = "Saracens"; public const int SaracensId = 14; public const string SaracensJa = "サラセン";
        public const string Italians = "Italians"; public const int ItaliansId = 15; public const string ItaliansJa = "イタリア";
        public const string Vietnamese = "Vietnamese"; public const int VietnameseId = 16; public const string VietnameseJa = "ベトナム";
        public const string Japanese = "Japanese"; public const int JapaneseId = 17; public const string JapaneseJa = "日本";
        public const string Bohemians = "Bohemians"; public const int BohemiansId = 18; public const string BohemiansJa = "ボヘミア";
        public const string Vikings = "Vikings"; public const int VikingsId = 19; public const string VikingsJa = "バイキング";
        public const string Ethiopians = "Ethiopians"; public const int EthiopiansId = 20; public const string EthiopiansJa = "エチオピア";
        public const string Byzantines = "Byzantines"; public const int ByzantinesId = 21; public const string ByzantinesJa = "ビザンティン";
        public const string Burmese = "Burmese"; public const int BurmeseId = 22; public const string BurmeseJa = "ビルマ";
        public const string Burgundians = "Burgundians"; public const int BurgundiansId = 23; public const string BurgundiansJa = "ブルゴーニュ";
        public const string Malians = "Malians"; public const int MaliansId = 24; public const string MaliansJa = "マリ";
        public const string Britons = "Britons"; public const int BritonsId = 25; public const string BritonsJa = "ブリトン";
        public const string Poles = "Poles"; public const int PolesId = 26; public const string PolesJa = "ポーランド";
        public const string Hindustanis = "Hindustanis"; public const int HindustanisId = 27; public const string HindustanisJa = "ヒンドゥスタン";
        public const string Malay = "Malay"; public const int MalayId = 28; public const string MalayJa = "マレー";
        public const string Teutons = "Teutons"; public const int TeutonsId = 29; public const string TeutonsJa = "チュートン";
        public const string Mayans = "Mayans"; public const int MayansId = 30; public const string MayansJa = "マヤ";
        public const string Cumans = "Cumans"; public const int CumansId = 31; public const string CumansJa = "クマン";
        public const string Sicilians = "Sicilians"; public const int SiciliansId = 32; public const string SiciliansJa = "シチリア";
        public const string Spanish = "Spanish"; public const int SpanishId = 33; public const string SpanishJa = "スペイン";
        public const string Portuguese = "Portuguese"; public const int PortugueseId = 34; public const string PortugueseJa = "ポルトガル";
        public const string Chinese = "Chinese"; public const int ChineseId = 35; public const string ChineseJa = "中国";
        public const string Bulgarians = "Bulgarians"; public const int BulgariansId = 36; public const string BulgariansJa = "ブルガリア";
        public const string Celts = "Celts"; public const int CeltsId = 37; public const string CeltsJa = "ケルト";
        public const string Tatars = "Tatars"; public const int TatarsId = 38; public const string TatarsJa = "タタール";
        public const string Lithuanians = "Lithuanians"; public const int LithuaniansId = 39; public const string LithuaniansJa = "リトアニア";
        public const string Bengalis = "Bengalis"; public const int BengalisId = 40; public const string BengalisJa = "ベンガル";
        public const string Gurjaras = "Gurjaras"; public const int GurjarasId = 41; public const string GurjarasJa = "グルジャラ";
        public const string Dravidian = "Dravidian"; public const int DravidianId = 42; public const string DravidianJa = "ドラヴィダ";

        // 一覧定義
        public static readonly List<CivilizationInfo> All = new()
        {
            new(Mongols, MongolsId, MongolsJa),
            new(Koreans, KoreansId, KoreansJa),
            new(Magyars, MagyarsId, MagyarsJa),
            new(Franks, FranksId, FranksJa),
            new(Huns, HunsId, HunsJa),
            new(Slavs, SlavsId, SlavsJa),
            new(Persians, PersiansId, PersiansJa),
            new(Turks, TurksId, TurksJa),
            new(Aztecs, AztecsId, AztecsJa),
            new(Berbers, BerbersId, BerbersJa),
            new(Incas, IncasId, IncasJa),
            new(Goths, GothsId, GothsJa),
            new(Khmer, KhmerId, KhmerJa),
            new(Saracens, SaracensId, SaracensJa),
            new(Italians, ItaliansId, ItaliansJa),
            new(Vietnamese, VietnameseId, VietnameseJa),
            new(Japanese, JapaneseId, JapaneseJa),
            new(Bohemians, BohemiansId, BohemiansJa),
            new(Vikings, VikingsId, VikingsJa),
            new(Ethiopians, EthiopiansId, EthiopiansJa),
            new(Byzantines, ByzantinesId, ByzantinesJa),
            new(Burmese, BurmeseId, BurmeseJa),
            new(Burgundians, BurgundiansId, BurgundiansJa),
            new(Malians, MaliansId, MaliansJa),
            new(Britons, BritonsId, BritonsJa),
            new(Poles, PolesId, PolesJa),
            new(Hindustanis, HindustanisId, HindustanisJa),
            new(Malay, MalayId, MalayJa),
            new(Teutons, TeutonsId, TeutonsJa),
            new(Mayans, MayansId, MayansJa),
            new(Cumans, CumansId, CumansJa),
            new(Sicilians, SiciliansId, SiciliansJa),
            new(Spanish, SpanishId, SpanishJa),
            new(Portuguese, PortugueseId, PortugueseJa),
            new(Chinese, ChineseId, ChineseJa),
            new(Bulgarians, BulgariansId, BulgariansJa),
            new(Celts, CeltsId, CeltsJa),
            new(Tatars, TatarsId, TatarsJa),
            new(Lithuanians, LithuaniansId, LithuaniansJa),
            new(Bengalis, BengalisId, BengalisJa),
            new(Gurjaras, GurjarasId, GurjarasJa),
            new(Dravidian, DravidianId, DravidianJa),
        };

        public static string? GetNameById(int id) =>
            All.FirstOrDefault(c => c.Id == id)?.NameJa;

        public static string? GetNameByCode(string code) =>
            All.FirstOrDefault(c => c.Code == code)?.NameJa;

        public static string? GetCodeById(int id) =>
            All.FirstOrDefault(c => c.Id == id)?.Code;

        public static int? GetIdByCode(string code) =>
            All.FirstOrDefault(c => c.Code == code)?.Id;

        public static string? GetCodeByName(string nameJa) =>
            All.FirstOrDefault(c => c.NameJa == nameJa)?.Code;
    }
}
