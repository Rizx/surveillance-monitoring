namespace API.Models
{
    public class Lane
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public int Type { set; get; }

        public const int GATEIN = 1;
        public const int GATEOUT = 2;

        public Lane(
            long id,
            string name,
            int type)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
        }

        public void UpdateLane(
            string name,
            int type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}