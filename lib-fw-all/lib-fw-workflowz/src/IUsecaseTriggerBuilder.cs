namespace DistribuTe.Framework.WorkFlowZ;

public interface IUsecaseTriggerBuilder
{
    ISuccessFlowBuilder<TCondition> WhenTriggerCondition<TCondition>()
        where TCondition : class, IUsecaseTriggerCondition;
}