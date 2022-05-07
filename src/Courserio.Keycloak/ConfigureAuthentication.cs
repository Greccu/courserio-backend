using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Courserio.KeyCloak
{
    public static class ConfigureAuthentication
    {
        public static void AddKeyCloakAuthentication(this IServiceCollection services, IConfigurationSection conf)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = conf["Authority"];
                    options.Audience = conf["ClientId"];
                    options.RequireHttpsMetadata = false;
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ValidIssuer = conf["Authority"],
                        ValidateLifetime = false
                    };
                });
        }
    }
}
