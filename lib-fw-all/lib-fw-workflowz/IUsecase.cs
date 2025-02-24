namespace DistribuTe.Framework.WorkFlowZ;

public interface IUsecase
{
    Task<bool> ExecuteAsync<TCondition>(TCondition condition, CancellationToken cancellationToken)
        where TCondition : class, IUsecaseTriggerCondition;
}