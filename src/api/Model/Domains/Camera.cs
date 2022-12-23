namespace API.Models
{
    public class Camera
    {
        public long Id { set; get; }
        public long LaneId { set; get; }
        public string Name { set; get; }
        public string IPAddress { set; get; }
        public string VideoUrl { set; get; }
        public string FotoUrl { set; get; }
        public string Model { set; get; }
        public string Parameters { set; get; }

        public Camera(
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