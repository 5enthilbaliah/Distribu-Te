// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Helpers.Swagger;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using NSubstitute;
using Swashbuckle.AspNetCore.SwaggerGen;
using Teams.Apis.Controllers;
using Teams.Apis.Helpers.Swagger;

public class CorrelationIdOperationFilterTests
{
    private readonly CorrelationIdOperationFilter _filter;

    public CorrelationIdOperationFilterTests()
    {
        _filter = new CorrelationIdOperationFilter();
    }

    [Fact]
    public void Apply_ShouldSet_CorrelationId()
    {
        // Arrange
        var operation = new OpenApiOperation
        {
            Parameters = []
        };

        var contractResolver = Substitute.For<ISerializerDataContractResolver>();
        var apiDescription = new ApiDescription();
        var schemaGenerator = new SchemaGenerator(new SchemaGeneratorOptions(), contractResolver);
        var schemaRepository = new SchemaRepository();
        var methodInfo = typeof(CorrelationIdOperationFilter).GetMethod(nameof(AssociateController.SpawnAsync));
        OperationFilterContext context = new(apiDescription, schemaGenerator, schemaRepository, methodInfo);
        
        // Act
        _filter.Apply(operation, context);
        var correlationIdParameter = operation.Parameters.FirstOrDefault(p => p.Name == CorrelationIdOperationFilter.HEADER_CORRELATION_ID);
        
        // Assert
        correlationIdParameter.Should().NotBeNull();
    }
}