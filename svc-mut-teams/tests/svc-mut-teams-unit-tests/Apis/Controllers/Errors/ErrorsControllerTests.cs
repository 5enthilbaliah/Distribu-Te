﻿namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Controllers.Errors;

using FluentAssertions;
using Framework.AppEssentials;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Teams.Apis.Controllers.Errors;

public class ErrorsControllerTests
{
    private readonly ServiceProvider _serviceProvider;

    public ErrorsControllerTests()
    {
        var services = new ServiceCollection();
        services.AddScoped<ErrorsController>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void HandleErrorDevelopment_ReturnsProblemDetails()
    {
        // Arrange
        var hostEnvironment = Substitute.For<IHostEnvironment>();
        var requestContext = Substitute.For<IRequestContext>();
        var logger = Substitute.For<ILogger>();
        
        hostEnvironment.EnvironmentName.Returns("Development");
        requestContext.GetFeature<IExceptionHandlerFeature>().Returns(new ExceptionHandlerFeature
        {
            Path = "/test",
            Error = new Exception("this is test exception")
        });
        
        // Act
        var sut = _serviceProvider.GetService<ErrorsController>();
        var result = sut!.HandleErrorDevelopment(hostEnvironment, requestContext, logger);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        var actual = actualResult!.Value;
        actual.Should().BeOfType<ProblemDetails>();
        actualResult.StatusCode.Should().Be(500);
    }
    
    [Fact]
    public void HandleErrorDevelopment_WhenNonDevelopment_ReturnsNotFound()
    {
        // Arrange
        var hostEnvironment = Substitute.For<IHostEnvironment>();
        var requestContext = Substitute.For<IRequestContext>();
        var logger = Substitute.For<ILogger>();
        
        hostEnvironment.EnvironmentName.Returns("Production");
        requestContext.GetFeature<IExceptionHandlerFeature>().Returns(new ExceptionHandlerFeature
        {
            Path = "/test",
            Error = new Exception("this is test exception")
        });
        
        // Act
        var sut = _serviceProvider.GetService<ErrorsController>();
        var result = sut!.HandleErrorDevelopment(hostEnvironment, requestContext, logger);
        
        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void HandleError_ReturnsProblemDetails()
    {
        // Arrange
        var requestContext = Substitute.For<IRequestContext>();
        var logger = Substitute.For<ILogger>();
        
        // Act
        var sut = _serviceProvider.GetService<ErrorsController>();
        var result = sut!.HandleError(requestContext, logger);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.StatusCode.Should().Be(500);
    }
}