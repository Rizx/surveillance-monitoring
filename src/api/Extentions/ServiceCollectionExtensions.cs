
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using APIRepositories;
using APISettings;

namespace APIExtentions
{
    public static class ServiceCollectionExtensions
    {
        private static void AddSwaggerDocs(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "WeatherForecast",
            });

            // options.SwaggerDoc("v2", new OpenApiInfo
            // {
            //     Version = "v2.0",
            //     Title = "EARS.PHC.API",
            // });
        }

        // public static IServiceCollection AddApiVersion(this IServiceCollection services)
        // {
        //     return services.AddApiVersioning(config =>
        //     {
        //         config.DefaultApiVersion = new ApiVersion(APPLICATION.MAJOR_VERSION, APPLICATION.MINOR_VERSION);
        //         config.AssumeDefaultVersionWhenUnspecified = true;
        //         config.ReportApiVersions = true;
        //         config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
        //     });
        // }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.IsDynamic)
                    {
                        string xmlFile = $"{assembly.GetName().Name}.xml";
                        string xmlPath = Path.Combine(baseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            options.IncludeXmlComments(xmlPath);
                        }
                    }
                }

                options.AddSwaggerDocs();
                // options.DocInclusionPredicate((version, desc) =>
                // {
                //     if (!desc.TryGetMethodInfo(out var methodInfo))
                //     {
                //         return false;
                //     }

                //     var versions = methodInfo
                //         .DeclaringType?
                //         .GetCustomAttributes(true)
                //         .OfType<ApiVersionAttribute>()
                //         .SelectMany(attr => attr.Versions);

                //     var maps = methodInfo
                //         .GetCustomAttributes(true)
                //         .OfType<MapToApiVersionAttribute>()
                //         .SelectMany(attr => attr.Versions)
                //         .ToList();

                //     return versions?.Any(v => $"v{v}" == version) == true
                //            && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                // });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token to access this API",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    },
                });
                options.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$",
                    Example = new OpenApiString("02:00:00")
                });
            });
        }

        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            var options = services.GetOptions<JwtSettings>(nameof(JwtSettings));
            var key = Encoding.ASCII.GetBytes(options.Key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    SignatureValidator = (string token, TokenValidationParameters parameters) => new JwtSecurityToken(token),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                };
            });
        }

        // public static IServiceCollection RegisterOracleDb(this IServiceCollection services)
        // {
        //     var options = services.GetOptions<ConnectionStrings>(nameof(ConnectionStrings));
        //     return services.AddDbContext<EntityContext>(opt =>
        //         opt.UseOracle(options.OracleConnection));
        // }

        // public static IServiceCollection UseMongoDb(this IServiceCollection services)
        // {
        //     var options = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));

        //     var databaseName = MongoUrl.Create(options.MongoDbConnection).DatabaseName;
        //     var database = new MongoClient(options.MongoDbConnection).GetDatabase(databaseName);

        //     services.AddSingleton(database);
        //     services.AddScoped<EntityDocument>();

        //     return services;
        // }

        public static IServiceCollection RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            return services.AddCors(opt => opt.AddPolicy("CorsPolicy", policy => policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()));
        }

        private static T GetOptions<T>(this IServiceCollection services, string sectionName)
            where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection(sectionName);
            var options = new T();
            section.Bind(options);

            return options;
        }
    }
}