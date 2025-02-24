namespace DistribuTe.Framework.WorkFlowZ.Implementations;

using Microsoft.Extensions.DependencyInjection;

public class UsecaseOptionBuilder : IUsecaseTriggerBuilder
{
    private IFlowRegisterer? FlowRegisterer { get; set; }


    public ISuccessFlowBuilder<TCondition> WhenTriggerCondition<TCondition>() 
        where TCondition : class, IUsecaseTriggerCondition
    {
        var usecaseBuilder = new UsecaseBuilder<TCondition>();
        FlowRegisterer = usecaseBuilder;
        return usecaseBuilder;
    }

    public void Build(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(FlowRegisterer);
        FlowRegisterer.Build(services);
    }
}