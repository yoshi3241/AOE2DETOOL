namespace AOE2DETOOL.Models.Data
{
    /// <summary>
    /// ビルド情報
    /// </summary>
    public class BuildInfo
    {
        // ACTION:1,Action.BUILD,{'player_id': 1, 'building_id': 70, 'object_ids': [30559, 30560], 'x': 100.0, 'y': 102.0, 'sequence': 15996014}

        public BuildInfo()
        {

        }

        private int _playerId = 0;
        public int PlayerId
        {
            get { return _playerId; }
            set { _playerId = value; }
        }

        private long _buildingId = 0;
        public long BuildingId
        {
            get { return _buildingId; }
            set { _buildingId = value; }
        }

        private long[]? _objectIds = null;
        public long[]? ObjectIds
        {
            get { return _objectIds; }
            set { _objectIds = value; }
        }

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

        private float _xEnd = 0;
        public float XEnd
        {
            get { return _xEnd; }
            set { _xEnd = value; }
        }

        private float _yEnd = 0;
        public float YEnd
        {
            get { return _yEnd; }
            set { _yEnd = value; }
        }

        public int _sequence { get; set; }
        public int Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }
    }
}
