using API.Settings;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace API.Shared
{
    public static class ApplicationSettings
    {
        public static IConfiguration Configuration { get; private set; }
        public static JwtSetting JwtSetting { get; } = new();
        public static ConnectionStrings ConnectionStrings { get; } = new();
        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}