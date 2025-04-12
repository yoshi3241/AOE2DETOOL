using static AOE2DETOOL.Definition.Enums;

namespace AOE2DETOOL.Models.Data
{
    public class UnitInfo
    {
        public struct UnitProductionInfo
        {
            private int _amount;
            public int Amount { get => _amount; set => _amount = value; }

            private int _completionTime;
            public int CompletionTime { get => _completionTime; set => _completionTime = value; }

            private long _technologyId;
            public long TechnologyId { get => _technologyId; set => _technologyId = value; }
        }

        public UnitInfo()
        {

        }

        public class UnitInfoItem
        {
            private Dictionary<long, List<UnitProductionInfo>> _objectIds = new Dictionary<long, List<UnitProductionInfo>>();
            public Dictionary<long, List<UnitProductionInfo>> ObjectIds
            {
                set { _objectIds = value; }
                get { return _objectIds; }
            }
        }

        /// <summary>
        /// 系列別ユニットカウンタ
        /// </summary>
        private Dictionary<UnitGroupType, int> _unitGroupTypeCount = new Dictionary<UnitGroupType, int>();
        public Dictionary<UnitGroupType, int> UnitGroupTypeCount
        {
            set { _unitGroupTypeCount = value; }
            get { return _unitGroupTypeCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<UnitType, UnitInfoItem> _unitTypeCount = new Dictionary<UnitType, UnitInfoItem>();
        public Dictionary<UnitType, UnitInfoItem> UnitTypeCount
        {
            set { _unitTypeCount = value; }
            get { return _unitTypeCount; }
        }
    }
}
