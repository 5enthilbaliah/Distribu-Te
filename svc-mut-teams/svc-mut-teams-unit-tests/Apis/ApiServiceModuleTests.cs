namespace DistribuTe.Mutators.Teams.UnitTests.Apis;

using System.Text;
using System.Text.Json;
using Asp.Versioning;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Teams.Apis;
using Teams.Apis.Settings;

public class ApiServiceModuleTests
{
    private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();

    [Fact]
    public void ApiServiceModule_Registration_Succeeds()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOptions();

        var fixture = new Fixture();
        var serviceSettings = fixture.Create<ServiceSettings>();
        var swaggerSettings = fixture.Create<SwaggerSettings>();

        var settings = new
        {
            ServiceSettings = serviceSettings,
            SwaggerSettings = swaggerSettings
        };
        
        var apiSettings = JsonSerializer.Serialize(settings);
        var builder = new ConfigurationBuilder();
        builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(apiSettings)));
        var configuration = builder.Build();
        
        // Act
        var sut = new ApiServiceModule();
        sut.Register(services, _webHostEnvironment, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var serviceSettingOptions = serviceProvider.GetService<IOptions<ServiceSettings>>();
        var swaggerSettingOptions = serviceProvider.GetService<IOptions<SwaggerSettings>>();
        var versionParser = serviceProvider.GetRequiredService<IApiVersionParser>();
        
        // Assert
        serviceSettingOptions!.Value.Should().BeEquivalentTo(serviceSettings);
        swaggerSettingOptions!.Value.Should().BeEquivalentTo(swaggerSettings);
        versionParser.Should().NotBeNull();
    }
}