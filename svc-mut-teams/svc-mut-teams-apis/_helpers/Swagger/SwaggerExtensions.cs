// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Helpers.Swagger;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

[ExcludeFromCodeCoverage]
public static class SwaggerExtensions
{
    // ReSharper disable once InconsistentNaming
    private const string SECURITY_SCHEME_BEARER = "Bearer";
    // ReSharper disable once InconsistentNaming
    private const string SECURITY_SCHEME_OAUTH2 = "oauth2";
    public static void ConfigureSwaggerForOauth(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(SECURITY_SCHEME_BEARER, new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n
                Example: 'Bearer 12345token'",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Scheme = SECURITY_SCHEME_BEARER
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = SECURITY_SCHEME_BEARER
                    },
                    Scheme = SECURITY_SCHEME_OAUTH2,
                    Name = SECURITY_SCHEME_BEARER,
                    In = ParameterLocation.Header
                },
                []
            }
        });
    }
}