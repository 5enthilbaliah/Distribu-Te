namespace DistribuTe.Mutators.Teams.UnitTests.Apis.Controllers;

using AutoFixture;
using DistribuTe.Mutators.Teams.Apis.Controllers;
using DistribuTe.Mutators.Teams.Application.Associates;
using DistribuTe.Mutators.Teams.Application.Associates.DataContracts;
using ErrorOr;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

public class AssociateControllerTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    public AssociateControllerTests()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => _mediator);
        services.AddScoped<AssociateController>();
        
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void AssociateController_NullArgument_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Action act = () => new AssociateController(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'mediator')");
    }

    [Fact]
    public async Task SpawnAsync_ValidRequest_ReturnsCreatedResponse()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<AssociateRequest>();
        var response = fixture.Create<AssociateResponse>();
        
        _mediator.Send(Arg.Any<SpawnAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateController>();
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
        var request = fixture.Create<AssociateRequest>();
        var error = Error.Validation("test", "this is a test");
        
        _mediator.Send(Arg.Any<SpawnAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateController>();
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
        var request = fixture.Create<AssociateRequest>();
        var response = fixture.Create<AssociateResponse>();
        var associateId = 3;
        _mediator.Send(Arg.Any<CommitAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateController>();
        var result = await sut!.CommitAsync(associateId, request, CancellationToken.None);
        
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
        var request = fixture.Create<AssociateRequest>();
        var error = Error.Validation("test", "this is a test");
        var associateId = 3;
        _mediator.Send(Arg.Any<CommitAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateController>();
        var result = await sut!.CommitAsync(associateId, request, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
    
    [Fact]
    public async Task TrashAsync_ValidId_ReturnsNoContentResponse()
    {
        // Arrange
        var associateId = 3;
        _mediator.Send(Arg.Any<TrashAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(true);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateController>();
        var result = await sut!.TrashAsync(associateId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
    
    [Fact]
    public async Task TrashAsync_InvalidId_ReturnsProblemResponse()
    {
        // Arrange
        var associateId = 3;
        var error = Error.Validation("test", "this is a test");
        _mediator.Send(Arg.Any<TrashAssociateCommand>(), Arg.Any<CancellationToken>())
            .Returns(error);
        
        // Act
        var sut = _serviceProvider.GetService<AssociateController>();
        var result = await sut!.TrashAsync(associateId, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<ObjectResult>();
        var actualResult = result as ObjectResult;
        actualResult!.Value.Should().BeOfType<ValidationProblemDetails>();
    }
}