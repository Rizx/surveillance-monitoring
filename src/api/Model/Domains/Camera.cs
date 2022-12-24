namespace API.Models
{
    public class Camera
    {
        public long Id { set; get; }
        public long LaneId { set; get; }
        public string Type { set; get; }
        public string Name { set; get; }
        public string IpAddress { set; get; }
        public string VideoUrl { set; get; }
        public string FotoUrl { set; get; }
        public string Model { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Parameters { set; get; }

        protected Camera(){}

        public Camera(
            long id,
            long laneId,
            string name,
            string ipAddress,
            string videoUrl,
            string fotoUrl,
            string model,
            string userName,
            string password,
            string parameters)
        {
            Id = id;
            LaneId = laneId;
            Name = name;
            IpAddress = ipAddress;
            VideoUrl = videoUrl;
            FotoUrl = fotoUrl;
            Model = model;
            UserName = userName;
            Password = password;
            Parameters = parameters;
        }
    }
}