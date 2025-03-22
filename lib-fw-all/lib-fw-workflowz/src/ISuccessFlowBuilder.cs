namespace DistribuTe.Framework.WorkFlowZ;

public interface ISuccessFlowBuilder<TCondition>
    where TCondition : class, IUsecaseTriggerCondition
{
    IFailureFlowBuilder<TCondition> HandleSuccessWith<TFlow>()
        where TFlow : class, IUsecaseSuccessFlow<TCondition>;
}