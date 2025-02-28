namespace DistribuTe.Mutators.Teams.Application._shared;

public interface IRequestContext
{
    string CorrelationId { get; }
    string UserIdentity { get; }
    string UserEmail { get; }
}