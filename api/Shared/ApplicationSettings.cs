using API.Settings;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace API.Shared
{
    public static class ApplicationSettings
    {
        public static IConfiguration Configuration { get; private set; }
        public static JwtSetting JwtSetting { get; private set; }
        public static ConnectionStrings ConnectionStrings { get; private set; }
        public static ServiceSetting ServiceSetting { get; private set; }
        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSetting = new();
            ConnectionStrings = new();
            ServiceSetting = new();
        }
    }
}