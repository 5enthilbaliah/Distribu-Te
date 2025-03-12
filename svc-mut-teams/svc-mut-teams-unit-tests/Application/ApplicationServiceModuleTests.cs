namespace DistribuTe.Mutators.Teams.UnitTests.Application;

using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application;

public class ApplicationServiceModuleTests
{
    private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();

    [Fact]
    public void ApplicationServiceModule_Registration_Succeeds()
    {
        // Arrange
        var services = new ServiceCollection();
        
        // Act
        var sut = new ApplicationServiceModule();
        sut!.Register(services, _webHostEnvironment, _configuration);
        var serviceProvider = services.BuildServiceProvider();
        
        var mediator = serviceProvider.GetService<IMediator>();
        
        // Assert
        mediator.Should().NotBeNull();
    }
}