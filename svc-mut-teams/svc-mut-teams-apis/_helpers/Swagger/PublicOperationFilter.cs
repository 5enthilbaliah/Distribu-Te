// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Helpers.Swagger;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class PublicOperationFilter : IOperationFilter
{
    // ReSharper disable once InconsistentNaming
    private const string HEADER_API_KEY = "x-api-key";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        if (context.ApiDescription.RelativePath!.StartsWith("public"))
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = HEADER_API_KEY,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Provide your API key.",
            });
        }
    }
}