namespace DistribuTe.Framework.ApiEssentials.Swagger;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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

    public static void ConfigureSwagger(this IServiceCollection services, IWebHostEnvironment environment, 
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        IConfiguration configuration, string title = "DistribuTe api", string version = "v1", string docPathPattern = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    {
        services.AddEndpointsApiExplorer();
        var svcSettings = new ServiceSettings();
        configuration.GetSection(nameof(ServiceSettings)).Bind(svcSettings);
        
        services.AddSwaggerGen(c =>
        {
            c.ConfigureSwaggerForOauth();
            c.OperationFilter<CorrelationIdOperationFilter>();
            c.OperationFilter<PublicOperationFilter>();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"{title} - {environment.EnvironmentName} - {svcSettings.Version}",
                Version = version,
            });
            c.DescribeAllParametersInCamelCase();

            if (!string.IsNullOrEmpty(docPathPattern))
            {
                foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, docPathPattern))
                {
                    c.IncludeXmlComments(file, includeControllerXmlComments: true);
                }
            }
        });
    }
}