namespace DistribuTe.Framework.WorkFlowZ;

public interface IFailureFlowBuilder<TCondition>
    where TCondition : class, IUsecaseTriggerCondition
{
    void HandleFailureWith<TFlow>()
        where TFlow : class, IUsecaseFailureFlow<TCondition>;
}