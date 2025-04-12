namespace AOE2DETOOL.Models.Data
{
    public class CommandDetail
    {
        public CommandDetail()
        {

        }
        // 
        public int player_id { get; set; }
        public long command_id { get; set; }
        public long building_id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float x_end { get; set; }
        public float y_end { get; set; }
        public int sequence { get; set; }
        public long[]? object_ids { get; set; }
        public int amount { get; set; }
        public long unit_id { get; set; }
        public long target_id { get; set; }
        public long slot_id { get; set; }
        public long order_id { get; set; }
        public long technology_id { get; set; }

        public int PlayerId
        {
            get { return player_id; }
            set { player_id = value; }
        }

        public long CommandId
        {
            get { return command_id; }
            set { command_id = value; }
        }

        public long BuildingId
        {
            get { return building_id; }
            set { building_id = value; }
        }


        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float XEnd
        {
            get { return x_end; }
            set { x_end = value; }
        }

        public float YEnd
        {
            get { return y_end; }
            set { y_end = value; }
        }

        public int Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

        public long[]? ObjectIds
        {
            get { return object_ids; }
            set { object_ids = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public long UnitId
        {
            get { return unit_id; }
            set { unit_id = value; }
        }

        public long TargetId
        {
            get { return target_id; }
            set { target_id = value; }
        }

        public long SlotId
        {
            get { return slot_id; }
            set { slot_id = value; }
        }

        public long OrderId
        {
            get { return order_id; }
            set { order_id = value; }
        }

        public long TechnologyId
        {
            get { return technology_id; }
            set { technology_id = value; }
        }
        
        public string GetGameTimeStr()
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, sequence);

            return ts.ToString(@"hh\:mm\:ss");
        }
    }
}
