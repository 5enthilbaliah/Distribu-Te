namespace DistribuTe.Mutators.Teams.UnitTests.Infrastructure;

using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application.Shared;
using Teams.Infrastructure;

public class InfrastructureServiceModuleTests
{
    private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
    
    [Fact]
    public void InfrastructureServiceModule_Registration_Succeeds()
    {
        // Arrange
        var services = new ServiceCollection();
        
        // Act
        var sut = new InfrastructureServiceModule();
        sut!.Register(services, _webHostEnvironment, _configuration);
        var serviceProvider = services.BuildServiceProvider();
        
        var dateProvider = serviceProvider.GetService<IDateTimeProvider>();
        
        // Assert
        dateProvider.Should().NotBeNull();
    }
}