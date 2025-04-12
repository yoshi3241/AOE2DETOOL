using static AOE2DETOOL.Definition.Enums;

namespace AOE2DETOOL.Tables.Master
{
    /// <summary>
    /// ユニット種別関連定義
    /// </summary>
    static public class Unit
    {
        /// <summary>
        /// ユニット種別インデックス
        /// </summary>
        public const int UnitTypeIndex = 0;
        /// <summary>
        /// ユニット種別詳細情報インデックス
        /// </summary>
        public const int DetailDataIndex = 1;
        /// <summary>

        /// ユニットグループ種別インデックス
        /// </summary>
        public const int UnitGroupTypeIndex = 0;

        /// <summary>
        /// ローカルユニット種別からユニットグループ種別への変換テーブル
        /// </summary>
        static readonly public Dictionary<UnitType, List<object>> UnitTypeToExpansionInfo = new Dictionary<UnitType, List<object>>()
        {
            { UnitType.Pop, new List<object>() {UnitGroupType.PopType, } },                 // 農民
            { UnitType.Ram, new List<object>() {UnitGroupType.SiegeType, } },               // 破城槌
            { UnitType.Bow,new List<object>() {UnitGroupType.BowType, } },                  // 弓
            { UnitType.Spear,new List<object>() {UnitGroupType.SpearType, } },              // 槍
            { UnitType.Scorpion,new List<object>() {UnitGroupType.ScorpionType, } },        // スコーピオン
            { UnitType.Warrior,new List<object>() {UnitGroupType.WarriorType, } },          // 戦士
            { UnitType.Samurai,new List<object>() {UnitGroupType.WarriorType, } },          // 侍
            { UnitType.Bombing,new List<object>() {UnitGroupType.SiegeType, } },            // 爆破工作塀
            { UnitType.LongTrebuchet,new List<object>() {UnitGroupType.SiegeType, } },      // 遠投投石機
            { UnitType.Clergyman,new List<object>() {UnitGroupType.ClergymanType, } },      // 聖職者
            { UnitType.Knight,new List<object>() {UnitGroupType.HorseType, } },             // 騎士
            { UnitType.Cavalry,new List<object>() {UnitGroupType.HorseType, } },            // 騎兵
            { UnitType.Trebuchet,new List<object>() {UnitGroupType.SiegeType, } },          // 投石機
            { UnitType.FishingBoat,new List<object>() {UnitGroupType.PopType, } },          // 漁船
            { UnitType.Wagon,new List<object>() {UnitGroupType.PopType, } },                // 荷馬車
            { UnitType.Elephant,new List<object>() {UnitGroupType.ElephantType, } },        // 象
            { UnitType.Skirmisher,new List<object>() {UnitGroupType.SkirmisherType, } },    // 散兵
            { UnitType.Inucelli,new List<object>() {UnitGroupType.BowType, } },             // イヌチェリ
            { UnitType.Haskarl,new List<object>() {UnitGroupType.HaskarlType, } },          // ハスカール
            { UnitType.BattleElephant,new List<object>() {UnitGroupType.ElephantType, } },  // バトルエレファント
            { UnitType.BaristaElephant,new List<object>() {UnitGroupType.ScorpionType, } }, // バリスタエレファント
            { UnitType.Mangudai,new List<object>() {UnitGroupType.BowType, } },             // マングダイ
            { UnitType.HorseArcher,new List<object>() {UnitGroupType.BowType, } },          // 弓騎兵
            { UnitType.Mamluk,new List<object>() {UnitGroupType.CamelType, } },             // マムルーク
            { UnitType.longbow,new List<object>() {UnitGroupType.BowType, } },              // ロングボウ
            { UnitType.jaguarWarrior,new List<object>() {UnitGroupType.HaskarlType, } },    // ジャガーウォリアー
            { UnitType.EagleWarrior,new List<object>() {UnitGroupType.HaskarlType, } },     // イーグルウォリアー
            { UnitType.GenoeseArcher,new List<object>() {UnitGroupType.BowType, } },        // ジェノヴァ弓騎兵
            { UnitType.Camayak,new List<object>() {UnitGroupType.BowType, } },              // カマヤック
            { UnitType.Slinger,new List<object>() {UnitGroupType.BowType, } },              // スリンガー
            { UnitType.ShortelWarrior,new List<object>() {UnitGroupType.WarriorType, } },   // ショーテルウォリアー
            { UnitType.CamelCavalry,new List<object>() {UnitGroupType.CamelType, } },       // らくだ騎兵
            { UnitType.Gunner,new List<object>() {UnitGroupType.BowType, } },               // 砲撃手
            { UnitType.Cannon,new List<object>() {UnitGroupType.SiegeType, } },             // 大砲
            { UnitType.Kipchak,new List<object>() {UnitGroupType.BowType, } },              // キプチャク
            { UnitType.StepLancer,new List<object>() {UnitGroupType.HorseType, } },         // ステップランサー
            { UnitType.WardRaider,new List<object>() {UnitGroupType.WarriorType, } },       // ウォードレイダー
            { UnitType.Conquistador,new List<object>() {UnitGroupType.BowType, } },         // コンキスタドール
            { UnitType.Boyar,new List<object>() {UnitGroupType.HorseType, } },              // ボヤール
            { UnitType.Keshik,new List<object>() {UnitGroupType.HorseType, } },             // ケシク
            { UnitType.TeutonicNight,new List<object>() {UnitGroupType.WarriorType, } },    // チュートンナイト
            { UnitType.Berserk,new List<object>() {UnitGroupType.WarriorType, } },          // ベルセルク
            { UnitType.KataCraft,new List<object>() {UnitGroupType.HorseType, } },          // カタクラフト
            { UnitType.Alumbai,new List<object>() {UnitGroupType.HorseType, } },            // アランバイ
            { UnitType.Goulam,new List<object>() {UnitGroupType.HaskarlType, } },           // グーラム(インドが苦手としている対弓のアンチユニット)
            { UnitType.Frankaslow,new List<object>() {UnitGroupType.BowType, } },           // フランカスロウ
            { UnitType.Tarkan,new List<object>() {UnitGroupType.HorseType, } },             // タルカン(馬の機動力と歩兵の建物破壊を合わせ持ったユニット)
            { UnitType.RattanArcher,new List<object>() {UnitGroupType.BowType, } },         // 籐弓兵
            { UnitType.CamelArcher,new List<object>() {UnitGroupType.CamelType, } },        // らくだ弓騎兵
            { UnitType.OrganGun,new List<object>() {UnitGroupType.BowType, } },             // オルガン砲
            { UnitType.MagyarHasah,new List<object>() {UnitGroupType.HorseType, } },        // マジャールハサー
            { UnitType.FeatherArcher,new List<object>() {UnitGroupType.BowType, } },        // 羽飾射手
            { UnitType.Gbet,new List<object>() {UnitGroupType.BowType, } },                 // グベト(フランカスロウと比較すると、射程も長く移動速度も速いので、城主の時代から荒らしの運用)
            { UnitType.KarambitWarrior,new List<object>() {UnitGroupType.WarriorType, } },  // カランビットウォリアー(単体の性能は貧弱すぎる軍兵系)
            { UnitType.Raitis,new List<object>() {UnitGroupType.HorseType, } },             // レイティス(敵ユニットの物理防御を無視して攻撃できる)
            { UnitType.Crossbowmen,new List<object>() {UnitGroupType.BowType, } },          // 連弩兵
            { UnitType.KoreaTank,new List<object>() {UnitGroupType.BowType, } },            // 戦車
            { UnitType.Condottiere,new List<object>() {UnitGroupType.WarriorType, } },      // コンドッティエーレ
        };



        /// <summary>
        /// AOE2DEのユニット種別からローカルユニット種別への変換テーブル
        /// </summary>
        static public Dictionary<long, UnitType> AOE2DEUnitTypeToLocalUnitType = new Dictionary<long, UnitType>()
        {
            { 4, UnitType.Bow },                // 弓
            { 5, UnitType.Gunner },             // 砲撃手
            { 7, UnitType.Skirmisher },         // 散兵
            { 8, UnitType.longbow },            // ロングボウ
            { 11, UnitType.Mangudai },          // マングダイ
            { 13, UnitType.FishingBoat },       // 漁船
            { 25, UnitType.TeutonicNight },     // チュートンナイト
            { 36, UnitType.Cannon },            // 大砲
            { 38, UnitType.Knight },            // 騎士
            { 39, UnitType.HorseArcher },       // 弓騎兵
            { 40, UnitType.KataCraft },         // カタクラフト
            { 41, UnitType.Haskarl },           // ハスカール
            { 46, UnitType.Inucelli },          // イヌチェリ
            { 73, UnitType.Crossbowmen },       // 連弩兵
            { 74, UnitType.Warrior },           // 戦士
            { 83, UnitType.Pop },               // 農民
            { 93, UnitType.Spear },             // 槍
            { 125, UnitType.Clergyman },        // 聖職者
            { 128, UnitType.Wagon },            // 荷馬車
            { 185, UnitType.Slinger },          // スリンガー
            { 232, UnitType.WardRaider },       // ウォードレイダー
            { 239, UnitType.Elephant },         // 象
            { 279, UnitType.Scorpion },         // スコーピオン
            { 280, UnitType.Trebuchet },        // 投石機
            { 281, UnitType.Frankaslow },       // フランカスロウ
            { 282, UnitType.Mamluk },           // マムルーク
            { 291, UnitType.Samurai },          // 侍
            { 331, UnitType.LongTrebuchet },    // 遠投投石機
            { 440, UnitType.Bombing },          // 爆破工作兵
            { 448, UnitType.Cavalry },          // 騎兵
            { 692, UnitType.Berserk },          // ベルセルク
            { 725, UnitType.jaguarWarrior },    // ジャガーウォリアー
            { 751, UnitType.EagleWarrior },     // イーグルウォリアー
            { 755, UnitType.Tarkan },           // タルカン
            { 763, UnitType.FeatherArcher },    // 羽飾射手
            { 771, UnitType.Conquistador },     // コンキスタドール
            { 827, UnitType.KoreaTank },        // 戦車
            { 866, UnitType.GenoeseArcher },    // ジェノヴァ石弓兵
            { 869, UnitType.MagyarHasah },      // マジャールハサー
            { 876, UnitType.Boyar },            // ボヤール
            { 879, UnitType.Camayak },          // カマヤック
            { 882, UnitType.Condottiere },      // コンドッティエーレ
            { 1001, UnitType.OrganGun },        // オルガン砲
            { 1007, UnitType.CamelArcher },     // らくだ弓騎兵
            { 1013, UnitType.Gbet },            // グベト
            { 1016, UnitType.ShortelWarrior },  // ショーテルウォリアー
            { 1120, UnitType.BaristaElephant }, // バリスタエレファント
            { 1123, UnitType.KarambitWarrior }, // カランビットウォリアー
            { 1126, UnitType.Alumbai },         // アランバイ
            { 1129, UnitType.RattanArcher },    // 籐弓兵
            { 1132, UnitType.BattleElephant },  // バトルエレファント
            { 1228, UnitType.Keshik },          // ケシク
            { 1231, UnitType.Kipchak },         // キプチャク
            { 1234, UnitType.Raitis },          // レイティス
            { 1258, UnitType.Ram },             // 破城槌
            { 1370, UnitType.StepLancer },      // ステップランサー
            { 1747, UnitType.CamelCavalry },    // らくだ騎兵
            { 1755, UnitType.Goulam },          // グーラム
        };
    }

}
