namespace AutogateAPI.Models
{
    public class Lanes
    {
        public long Id { protected set; get; }
        public string Name { protected set; get; }
        public int TYPE { protected set; get; }

        public const int GATEIN = 1;
        public const int GATEOUT = 2;

        public Lanes(
            long id,
            string name,
            int type)
        {
            this.Id = id;
            this.Name = name;
            this.TYPE = type;
        }

        public void UpdateLane(
            string name,
            int type)
        {
            this.Name = name;
            this.TYPE = type;
        }
    }
}