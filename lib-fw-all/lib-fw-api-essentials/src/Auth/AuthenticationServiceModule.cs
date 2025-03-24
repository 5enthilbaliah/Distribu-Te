namespace DistribuTe.Framework.ApiEssentials.Auth;

using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ModuleZ.Implementations;

public class AuthenticationServiceModule : DependencyServiceModule
{
    [ExcludeFromCodeCoverage]
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var authSettings = new AuthSettings();
        configuration.GetSection(nameof(AuthSettings)).Bind(authSettings);

        services.AddAuthorization();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Audience = authSettings.JwtAudience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "name",
                ValidIssuer = authSettings.JwtIssuer,
                ValidateIssuer = true,
                ValidAudience = authSettings.JwtAudience,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                SignatureValidator = (token, _) => new JsonWebToken(token)
            };
        });
    }
}