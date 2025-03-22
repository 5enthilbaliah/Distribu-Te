namespace DistribuTe.Mutators.Teams.UnitTests.Domain;

using System.Text;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Teams.Domain;
using Teams.Domain.Settings;

public class DomainServiceModuleTests
{
    private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();

    [Fact]
    public void DomainServiceModule_Registration_Succeeds()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOptions();
 
        var fixture = new Fixture();
        var cacheSettings = fixture.Create<CacheSettings>();
        var distribuTeDbSettings = fixture.Create<DistribuTeDbSettings>();
        
        var settings = new
        {
            CacheSettings = cacheSettings,
            DistribuTeDbSettings = distribuTeDbSettings
        };
         
        var apiSettings = JsonSerializer.Serialize(settings);
        var builder = new ConfigurationBuilder();
        builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(apiSettings)));
        var configuration = builder.Build();
         
        // Act
        var sut = new DomainServiceModule();
        sut.Register(services, _webHostEnvironment, configuration);
        var serviceProvider = services.BuildServiceProvider();
        var cacheSettingOptions = serviceProvider.GetService<IOptions<CacheSettings>>();
        var distribuTeDbSettingOptions = serviceProvider.GetService<IOptions<DistribuTeDbSettings>>();
        
        // Assert
        cacheSettingOptions!.Value.Should().BeEquivalentTo(cacheSettings);
        distribuTeDbSettingOptions!.Value.Should().BeEquivalentTo(distribuTeDbSettings);
    }
}