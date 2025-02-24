namespace DistribuTe.Framework.WorkFlowZ;

public interface IUsecaseSuccessFlow<TCondition>
    where TCondition : class, IUsecaseTriggerCondition
{
    Task Handle(TCondition usecaseCondition, CancellationToken cancellationToken);
}