namespace DistribuTe.Framework.ApiEssentials.Swagger;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class CorrelationIdOperationFilter : IOperationFilter
{
    // ReSharper disable once InconsistentNaming
    public const string HEADER_CORRELATION_ID = "x-correlation-id";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = HEADER_CORRELATION_ID,
            In = ParameterLocation.Header,
            Required = false,
        });
    }
}