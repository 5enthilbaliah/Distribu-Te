namespace DistribuTe.Mutators.Teams.UnitTests.Apis._helpers;

using System.Security.Claims;
using Application.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Apis.Helpers;

public class RequestContextTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

    public RequestContextTests()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => _httpContextAccessor);
        services.AddScoped<IRequestContext, RequestContext>();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void Current_ReturnsHttpContext()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>() as RequestContext;
        
        // Assert
        sut!.Current.Should().BeOfType<DefaultHttpContext>();
    }

    [Fact]
    public void User_WhenAvailable_ReturnsClaimsPrincipal()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim("name", "test"),
        ], "Basic"));
        httpContext.User = claimsPrincipal;
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>() as RequestContext;
        
        // Assert
        sut!.User!.Claims.Count().Should().Be(1);
    }

    [Fact]
    public void User_WhenNotAvailable_ReturnsEmptyClaims()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>() as RequestContext;
        
        // Assert
        sut!.User!.Claims.Should().BeEmpty();
    }

    [Fact]
    public void CorrelationId_WhenAvailable_ReturnsId()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers[RequestContext.HEADER_CORRELATION_ID] = "TestCorrelationId";
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        
        // Assert
        sut!.CorrelationId.Should().Be("TestCorrelationId");
    }

    [Fact]
    public void CorrelationId_WhenNotAvailable_ReturnsDefaultId()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        var valid = Guid.TryParse(sut!.CorrelationId, out _);
        
        // Assert
        valid.Should().BeTrue();
    }

    [Fact]
    public void UserIdentity_WhenAvailable_ReturnsUser()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(ClaimTypes.Name, "TestUser"),
        ], "Basic"));
        httpContext.User = claimsPrincipal;
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        
        // Assert
        sut!.UserIdentity.Should().Be("TestUser");
    }
    
    [Fact]
    public void UserIdentity_WhenNotAvailable_ReturnsDefaultUser()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var claimsPrincipal = new ClaimsPrincipal();
        httpContext.User = claimsPrincipal;
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        
        // Assert
        sut!.UserIdentity.Should().Be(RequestContext.DEFAULT_USER_NAME);
    }
    
    [Fact]
    public void UserEmail_WhenAvailable_ReturnsEmail()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(ClaimTypes.Email, "test@test.com"),
        ], "Basic"));
        httpContext.User = claimsPrincipal;
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        
        // Assert
        sut!.UserEmail.Should().Be("test@test.com");
    }
    
    [Fact]
    public void UserEmail_WhenNotAvailable_ReturnsEmpty()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var claimsPrincipal = new ClaimsPrincipal();
        httpContext.User = claimsPrincipal;
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        
        // Assert
        sut!.UserEmail.Should().BeEmpty();
    }

    [Fact]
    public void HttpMethod_ReturnsRequestMethod()
    {
        // Arrange
        var httpContext = Substitute.For<HttpContext>();
        var httpRequest = Substitute.For<HttpRequest>();
        httpRequest.Method.Returns("POST");
        httpContext.Request.Returns(httpRequest);
        _httpContextAccessor.HttpContext.Returns(httpContext);
        
        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        
        // Assert
        sut!.HttpMethod.Should().Be("POST");
    }

    [Fact]
    public void Features_ReturnsRequestFeatures()
    {
        // Arrange
        var httpContext = Substitute.For<HttpContext>();
        var httpRequest = Substitute.For<HttpRequest>();
        var featureCollection = Substitute.For<IFeatureCollection>();
        
        featureCollection.Get<IExceptionHandlerFeature>().Returns(new ExceptionHandlerFeature
        {
            Path = "/test",
            Error = new Exception("this is test exception")
        });
        httpRequest.Method.Returns("POST");
        httpContext.Request.Returns(httpRequest);
        httpContext.Features.Returns(featureCollection);
        _httpContextAccessor.HttpContext.Returns(httpContext);

        // Act
        var sut  = _serviceProvider.GetService<IRequestContext>();
        var result = sut!.GetFeature<IExceptionHandlerFeature>();
        
        // Assert
        result.Should().NotBeNull();
        result.Path.Should().Be("/test");
    }
}