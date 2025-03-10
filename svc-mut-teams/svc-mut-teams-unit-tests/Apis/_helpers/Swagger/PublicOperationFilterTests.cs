// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Helpers.Swagger;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using NSubstitute;
using Swashbuckle.AspNetCore.SwaggerGen;
using Teams.Apis.Controllers;
using Teams.Apis.Helpers.Swagger;

public class PublicOperationFilterTests
{
    private readonly PublicOperationFilter _filter;

    public PublicOperationFilterTests()
    {
        _filter = new PublicOperationFilter();
    }

    [Fact]
    public void Apply_SetApiKey_WhenPublicRoute_Succeeds()
    {
        // Arrange
        var operation = new OpenApiOperation
        {
            Parameters = []
        };
        
        var contractResolver = Substitute.For<ISerializerDataContractResolver>();
        var apiDescription = new ApiDescription
        {
            RelativePath = "public/tests"
        };
        var schemaGenerator = new SchemaGenerator(new SchemaGeneratorOptions(), contractResolver);
        var schemaRepository = new SchemaRepository();
        var methodInfo = typeof(CorrelationIdOperationFilter).GetMethod(nameof(AssociateController.SpawnAsync));
        OperationFilterContext context = new(apiDescription, schemaGenerator, schemaRepository, methodInfo);
        
        // Act
        _filter.Apply(operation, context);
        var param = operation.Parameters.FirstOrDefault(p => p.Name == PublicOperationFilter.HEADER_API_KEY);
        
        // Assert
        param.Should().NotBeNull();
        param.In.Should().Be(ParameterLocation.Header);
        param.Required.Should().BeFalse();
    }

    [Fact]
    public void Apply_DonotSetApiKey_WhenPrivateRoute_Succeeds()
    {
        // Arrange
        var operation = new OpenApiOperation
        {
            Parameters = []
        };
        
        var contractResolver = Substitute.For<ISerializerDataContractResolver>();
        var apiDescription = new ApiDescription
        {
            RelativePath = "protected/tests"
        };
        var schemaGenerator = new SchemaGenerator(new SchemaGeneratorOptions(), contractResolver);
        var schemaRepository = new SchemaRepository();
        var methodInfo = typeof(CorrelationIdOperationFilter).GetMethod(nameof(AssociateController.SpawnAsync));
        OperationFilterContext context = new(apiDescription, schemaGenerator, schemaRepository, methodInfo);
        
        // Act
        _filter.Apply(operation, context);
        var param = operation.Parameters.FirstOrDefault(p => p.Name == PublicOperationFilter.HEADER_API_KEY);
        
        // Assert
        param.Should().BeNull();
    }
}