namespace AutogateAPI.Models
{
    public class Cameras
    {
        public long Id { protected set; get; }
        public long LaneId { protected set; get; }
        public string Name { protected set; get; }
        public string IPAddress { protected set; get; }
        public string VideoUrl { protected set; get; }
        public string FotoUrl { protected set; get; }
        public string Model { protected set; get; }
        public string Parameters { protected set; get; }

        public Cameras(
            long id,
            long laneId,
            string name,
            string ipAddress,
            string videoUrl,
            string fotoUrl,
            string model,
            string parameters)
        {
            Id = id;
            LaneId = laneId;
            Name = name;
            IPAddress = ipAddress;
            VideoUrl = videoUrl;
            FotoUrl = fotoUrl;
            Model = model;
            Parameters = parameters;
        }
    }
}