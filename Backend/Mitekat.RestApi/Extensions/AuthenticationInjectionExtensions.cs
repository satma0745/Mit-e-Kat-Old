namespace Mitekat.RestApi.Extensions
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    internal static class AuthenticationInjectionExtensions
    {
        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var authConfig = configuration.GetSection("Auth").Get<AuthConfiguration>();
            var secretBytes = Encoding.ASCII.GetBytes(authConfig.SecretKey);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireSignedTokens = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),

                        ValidateAudience = false,
                        ValidateIssuer = false,

                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.RequireHttpsMetadata = false;
                });

            return services;
        }

        private class AuthConfiguration
        {
            public string SecretKey { get; set; }
        }
    }
}
