namespace DistribuTe.Framework.WorkFlowZ;

using Microsoft.Extensions.DependencyInjection;

public interface IFlowRegisterer<TCondition> : IFlowRegisterer
    where TCondition : class, IUsecaseTriggerCondition
{ }

public interface IFlowRegisterer
{
    void Build(IServiceCollection services);
}