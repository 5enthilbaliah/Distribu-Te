namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Controllers;

using AutoFixture;
using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Teams.Apis.Controllers;
using Teams.Application.SquadAssociates;
using Teams.Application.SquadAssociates.DataContracts;

public class SquadAssociateControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    public SquadAssociateControllerTests()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => _mediator);
        services.AddScoped<SquadAssociateController>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public void SquadAssociateController_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new SquadAssociateController(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mediator')");
    }
    
    [Fact]
    public async Task SpawnAsync_ValidRequest_ReturnsCreatedResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<SquadAssociateRequest>();
        var response = fixture.Create<SquadAssociateResponse>();
        
        _mediator.Send(Arg.Any<SpawnSquadAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
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
        var request = fixture.Create<SquadAssociateRequest>();
        var error = Error.Validation("test", "this is a test");
        
        _mediator.Send(Arg.Any<SpawnSquadAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
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
        var request = fixture.Create<SquadAssociateRequest>();
        var response = fixture.Create<SquadAssociateResponse>();
        var squadId = request.Squad_Id;
        var associateId = request.Associate_Id;
        _mediator.Send(Arg.Any<CommitSquadAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
        var result = await sut!.CommitAsync(squadId, associateId, request, CancellationToken.None);
        
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
        var request = fixture.Create<SquadAssociateRequest>();
        var error = Error.Validation("test", "this is a test");
        var squadId = request.Squad_Id;
        var associateId = request.Associate_Id;
        _mediator.Send(Arg.Any<CommitSquadAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
        var result = await sut!.CommitAsync(squadId, associateId, request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }

    [Fact]
    public async Task CommitAsync_MismatchRequest_ReturnsProblemResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<SquadAssociateRequest>();
        request.Squad_Id = 10;
        request.Associate_Id = 10;
        var squadId = 9;
        var associateId = 9;
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
        var result = await sut!.CommitAsync(squadId, associateId, request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
    
    [Fact]
    public async Task TrashAsync_ValidId_ReturnsNoContentResponse()
    {
        // Arrange
        var squadId = 9;
        var associateId = 9;
        _mediator.Send(Arg.Any<TrashSquadAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(true);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
        var result = await sut!.TrashAsync(squadId, associateId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task TrashAsync_InvalidId_ReturnsProblemResponse()
    {
        // Arrange
        var squadId = 9;
        var associateId = 9;
        var error = Error.Validation("test", "this is a test");
        _mediator.Send(Arg.Any<TrashSquadAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<SquadAssociateController>();
        var result = await sut!.TrashAsync(squadId, associateId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
}