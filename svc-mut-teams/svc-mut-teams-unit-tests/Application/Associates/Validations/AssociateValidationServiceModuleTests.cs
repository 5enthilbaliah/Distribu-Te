namespace DistribuTe.Mutators.Teams.UnitTests.Application.Associates.Validations;

using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Application.Associates;
using Teams.Application.Associates.DataContracts;
using Teams.Application.Associates.Validations;

public class AssociateValidationServiceModuleTests
{
    private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();

    [Fact]
    public void AssociateValidationServiceModule_Registration_Succeeds()
    {
        // Arrange
        var services = new ServiceCollection();
        var builder = new ConfigurationBuilder();
        var configuration = builder.Build();
        
        // Act
        var sut = new AssociateValidationServiceModule();
        sut.Register(services, _webHostEnvironment, configuration);
        var serviceProvider = services.BuildServiceProvider();

        var spawnBehavior = serviceProvider.GetRequiredService <
                            IPipelineBehavior<SpawnAssociateCommand, ErrorOr<AssociateResponse>>>();
        var commitBehavior = serviceProvider.GetRequiredService <
            IPipelineBehavior<CommitAssociateCommand, ErrorOr<AssociateResponse>>>();
        var trashBehavior = serviceProvider.GetRequiredService <
            IPipelineBehavior<TrashAssociateCommand, ErrorOr<bool>>>();

        // Assert
        spawnBehavior.Should().BeOfType<SpawnAssociateCommandValidationBehavior>();
        commitBehavior.Should().BeOfType<CommitAssociateCommandValidationBehavior>();
        trashBehavior.Should().BeOfType<TrashAssociateCommandValidationBehavior>();
    }
}