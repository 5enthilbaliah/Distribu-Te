namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Controllers;

using AutoFixture;
using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Apis.Controllers;
using Teams.Application.Squads;
using Teams.Application.Squads.DataContracts;

public class SquadControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    public SquadControllerTests()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => _mediator);
        services.AddScoped<SquadController>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void SquadController_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new SquadController(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mediator')");
    }
    
    [Fact]
    public async Task SpawnAsync_ValidRequest_ReturnsCreatedResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<SquadRequest>();
        var response = fixture.Create<SquadResponse>();
        
        _mediator.Send(Arg.Any<SpawnSquadCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var sut = _serviceProvider.GetService<SquadController>();
        var result = await sut!.SpawnAsync(request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        var actual = actualResult!.Value;
        actualResult.StatusCode.Should().Be(201);
        actual.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task SpawnAsync_InvalidRequest_ReturnsProblemResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<SquadRequest>();
        var error = Error.Validation("test", "this is a test");
        
        _mediator.Send(Arg.Any<SpawnSquadCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<SquadController>();
        var result = await sut!.SpawnAsync(request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
    
    [Fact]
    public async Task CommitAsync_ValidRequest_ReturnsOkResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<SquadRequest>();
        var response = fixture.Create<SquadResponse>();
        var squadId = 3;
        _mediator.Send(Arg.Any<CommitSquadCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var sut = _serviceProvider.GetService<SquadController>();
        var result = await sut!.CommitAsync(squadId, request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var actual = (result as OkObjectResult)!.Value;
        actual.Should().BeEquivalentTo(response);
    }
    
    [Fact]
    public async Task CommitAsync_InvalidRequest_ReturnsProblemResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<SquadRequest>();
        var error = Error.Validation("test", "this is a test");
        var squadId = 3;
        _mediator.Send(Arg.Any<CommitSquadCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<SquadController>();
        var result = await sut!.CommitAsync(squadId, request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
    
    [Fact]
    public async Task TrashAsync_ValidId_ReturnsNoContentResponse()
    {
        // Arrange
        var squadId = 3;
        _mediator.Send(Arg.Any<TrashSquadCommand>(), Arg.Any<CancellationToken>())
            .Returns(true);
        
        // Act
        var sut = _serviceProvider.GetService<SquadController>();
        var result = await sut!.TrashAsync(squadId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task TrashAsync_InvalidId_ReturnsProblemResponse()
    {
        // Arrange
        var squadId = 3;
        var error = Error.Validation("test", "this is a test");
        _mediator.Send(Arg.Any<TrashSquadCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<SquadController>();
        var result = await sut!.TrashAsync(squadId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
}