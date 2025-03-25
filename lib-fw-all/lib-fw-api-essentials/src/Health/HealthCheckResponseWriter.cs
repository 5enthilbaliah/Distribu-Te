namespace DistribuTe.Framework.ApiEssentials.Health;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public static class HealthCheckResponseWriter
{
    // ReSharper disable once InconsistentNaming
    private const string DEFAULT_CONTENT_TYPE = "application/json";
    private static readonly byte[] EmptyResponse = [(byte)'{', (byte)'}'];
    private static readonly Lazy<JsonSerializerOptions> Options = new(CreateJsonOptions);
    
    private static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        options.Converters.Add(new JsonStringEnumConverter());

        // for compatibility with older UI versions ( <3.0 ) we arrange
        // timespan serialization as s
        options.Converters.Add(new TimeSpanConverter());

        return options;
    }
    
    public static async Task WriteHealthCheckResponseWithoutFaultDetails(HttpContext httpContext, HealthReport? report)
    {
        if (report != null)
        {
            httpContext.Response.ContentType = DEFAULT_CONTENT_TYPE;

            var uiReport = HealthCheckReport.CreateFrom(report, _ => "Exception Occurred.");

            await JsonSerializer.SerializeAsync(httpContext.Response.Body, uiReport, Options.Value).ConfigureAwait(false);
        }
        else
        {
            await httpContext.Response.BodyWriter.WriteAsync(EmptyResponse).ConfigureAwait(false);
        }
    }
    
    public static async Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport? report)
    {
        if (report != null)
        {
            httpContext.Response.ContentType = DEFAULT_CONTENT_TYPE;

            var uiReport = HealthCheckReport.CreateFrom(report);

            var value = Options.Value;
            await JsonSerializer.SerializeAsync(httpContext.Response.Body, uiReport, value).ConfigureAwait(false);
        }
        else
        {
            await httpContext.Response.BodyWriter.WriteAsync(EmptyResponse).ConfigureAwait(false);
        }
    }
    
}