namespace AOE2DETOOL.Definition
{
    public class Enums
    {
        /// <summary>
        /// ユニットグループ種別
        /// </summary>
        public enum UnitGroupType
        {
            None = 0,
            PopType,            // 農民系
            BowType,            // 弓系
            SkirmisherType,     // 散兵系
            SpearType,          // 槍系
            WarriorType,        // 戦士系
            HorseType,          // 馬系
            ElephantType,       // 象系
            CamelType,          // らくだ系
            SiegeType,          // 攻城系
            HaskarlType,        // ハスカール系
            ClergymanType,      // 聖職者系
            ScorpionType,       // スコーピオン系
        }

        /// <summary>
        /// ローカスユニット種別
        /// </summary>
        public enum UnitType
        {
            None = 0,
            Pop,                // 農民
            Ram,                // 破城槌
            Bow,                // 弓
            Spear,              // 槍
            Scorpion,           // スコーピオン
            Warrior,            // 戦士
            Samurai,            // 侍
            Bombing,            // 爆破工作塀
            LongTrebuchet,      // 遠投投石機
            Clergyman,          // 聖職者
            Knight,             // 騎士
            Cavalry,            // 騎兵
            Trebuchet,          // 投石機
            FishingBoat,        // 漁船
            Wagon,              // 荷馬車
            Elephant,           // 象
            Skirmisher,         // 散兵
            Inucelli,           // イヌチェリ
            Haskarl,            // ハスカール
            BattleElephant,     // バトルエレファント
            BaristaElephant,    // バリスタエレファント
            Mangudai,           // マングダイ
            HorseArcher,        // 弓騎兵
            Mamluk,             // マムルーク
            longbow,            // ロングボウ
            jaguarWarrior,      // ジャガーウォリアー
            EagleWarrior,       // イーグルウォリアー
            GenoeseArcher,      // ジェノヴァ弓騎兵
            Camayak,            // カマヤック
            Slinger,            // スリンガー
            ShortelWarrior,     // ショーテルウォリアー
            CamelCavalry,       // らくだ騎兵
            Gunner,             // 砲撃手
            Cannon,             // 大砲
            Kipchak,            // キプチャク
            StepLancer,         // ステップランサー
            WardRaider,         // ウォードレイダー
            Conquistador,       // コンキスタドール
            Boyar,              // ボヤール
            Keshik,             // ケシク
            TeutonicNight,      // チュートンナイト
            Berserk,            // ベルセルク
            KataCraft,          // カタクラフト
            Alumbai,            // アランバイ
            Goulam,             // グーラム(インドが苦手としている対弓のアンチユニット)
            Frankaslow,         // フランカスロウ
            Tarkan,             // タルカン(馬の機動力と歩兵の建物破壊を合わせ持ったユニット)
            RattanArcher,       // 籐弓兵
            CamelArcher,        // らくだ弓騎兵
            OrganGun,           // オルガン砲
            MagyarHasah,        // マジャールハサー
            FeatherArcher,      // 羽飾射手
            Gbet,               // グベト(フランカスロウと比較すると、射程も長く移動速度も速いので、城主の時代から荒らしの運用)
            KarambitWarrior,    // カランビットウォリアー(単体の性能は貧弱すぎる軍兵系)
            Raitis,             // レイティス(敵ユニットの物理防御を無視して攻撃できる)
            Crossbowmen,        // 連弩兵
            KoreaTank,          // 戦車
            Condottiere,        // コンドッティエーレ
        }



        public enum AOE2DECivilizationCode
        {
            Mongols = 0,        // モンゴル
            Koreans = 1,        // 朝鮮
            Magyars = 2,        // マヤ
            Franks = 3,         // フランク
            Huns = 4,           // フン
            Slavs = 6,          // スラブ
            Persians = 7,       // ペルシア
            Turks = 8,          // トルコ
            Aztecs = 9,         // アステカ
            Berbers = 10,       // ベルベル
            Incas = 11,         // インカ
            Goths = 12,         // ゴート
            Khmer = 13,         // クメール
            Saracens = 14,      // サラセン
            Italians = 15,      // イタリア
            Vietnamese = 16,    // ベトナム
            Japanese = 17,      // 日本
            Bohemians = 18,     // ボヘミア
            Vikings = 19,       // バイキング
            Ethiopians = 20,    // エチオピア
            Byzantines = 21,    // ビザンティン
            Burmese = 22,       // ビルマ
            Burgundians = 23,   // ブルゴーニュ
            Malians = 24,       // マリ
            Britons = 25,       // ブリトン
            Poles = 26,         // ポーランド
            Hindustanis = 27,   // ヒンドゥスタン
            Malay = 28,         // マレー
            Teutons = 29,       // チュートン
            Mayans = 30,        // マヤ
            Cumans = 31,        // クマン
            Sicilians = 32,     // シチリア
            Spanish = 33,       // スペイン
            Portuguese = 34,    // ポルトガル
            Chinese = 35,       // 中国
            Bulgarians = 36,    // ブルガリア
            Celts = 37,         // ケルト
            Tatars = 38,        // タタール
            Lithuanians = 39,   // リトアニア
            Bengalis = 40,      // ベンガル
            Gurjaras = 41,      // グルジャラ
            Dravidian = 42,     // ドラヴィダ
        }
    }
}
