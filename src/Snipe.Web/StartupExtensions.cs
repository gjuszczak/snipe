using Snipe.Web.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Data;

namespace Snipe.Web
{
    public static class StartupExtensions
    {
        public static IHttpClientBuilder AddHttpClient<TClient, TImplementation, TFakeImplementation>(
            this IServiceCollection services,
            IConfiguration config
            )
            where TClient : class
            where TImplementation : class, TClient
            where TFakeImplementation : class, TClient
        {
            var useFakeApi = config.GetValue<bool>("UseFakeApi");

            if (useFakeApi)
            {
                return services.AddHttpClient<TClient, TFakeImplementation>();
            }
            else
            {
                return services.AddHttpClient<TClient, TImplementation>();
            }
        }

        public static void AddSnipeAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["UsersFeature:Secret"])),
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder(options.DefaultPolicy);
                policy.RequireRole("admin");
                options.DefaultPolicy = policy.Build();
            });
        }
    }
}
