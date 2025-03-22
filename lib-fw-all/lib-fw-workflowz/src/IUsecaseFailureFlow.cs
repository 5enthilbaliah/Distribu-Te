namespace DistribuTe.Framework.WorkFlowZ;

public interface IUsecaseFailureFlow<TCondition>
    where TCondition : class, IUsecaseTriggerCondition
{
    Task Handle(TCondition usecaseCondition, CancellationToken cancellationToken);
}