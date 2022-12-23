namespace AutogateAPI.Models
{
    public record AuthenticationRequest
    {
        public string Username { set; get; }
        public string Password { set; get; }
    }
}