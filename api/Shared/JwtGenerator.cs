// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using API.Shared;
using System;
using Jose;

namespace API
{
    public static class JwtGenerator
    {
        public static string Generate(long id)
        {
            var settings =  ApplicationSettings.JwtSetting;
            // Configuring "Claims" to your JWT Token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            };

            var userClaimsIdentity = new ClaimsIdentity(claims);

            // Create a SymmetricSecurityKey for JWT Token signatures
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));

            // HmacSha256 MUST be larger than 128 bits, so the key can't be too short. At least 16 and more characters.
            // https://stackoverflow.com/questions/47279947/idx10603-the-algorithm-hs256-requires-the-securitykey-keysize-to-be-greater
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = settings.Issuer,
                //Audience = issuer, // Sometimes you don't have to define Audience.
                //NotBefore = DateTime.Now, // Default is DateTime.Now
                //IssuedAt = DateTime.Now, // Default is DateTime.Now
                Subject = userClaimsIdentity,
                SigningCredentials = signingCredentials
            };

            // Generate a JWT securityToken, than get the serialized Token result (string)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);
            return serializeToken;
        }

        public static string Encode<T>(T payload)
        {
            var settings = ApplicationSettings.JwtSetting;
            var secretKey = Encoding.UTF8.GetBytes(settings.Key);
            return JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
        }

        public static T Decode<T>(string token)
        {
            var settings = ApplicationSettings.JwtSetting;
            var secretKey = Encoding.UTF8.GetBytes(settings.Key);
            return JWT.Decode<T>(token, secretKey, JwsAlgorithm.HS256);
        }
    }
}
