namespace DistribuTe.Framework.WorkFlowZ.Implementations;

using Microsoft.Extensions.DependencyInjection;

internal class UsecaseBuilder<TCondition> : ISuccessFlowBuilder<TCondition>,
    IFailureFlowBuilder<TCondition>, IFlowRegisterer<TCondition>
        where TCondition : class, IUsecaseTriggerCondition
{
    private Action<IServiceCollection>? _successRegisterer = default;
    private Action<IServiceCollection>? _failureRegisterer = default;


    public IFailureFlowBuilder<TCondition> HandleSuccessWith<TFlow>() 
        where TFlow : class, IUsecaseSuccessFlow<TCondition>
    {
        _successRegisterer = (services) => services.AddScoped<IUsecaseSuccessFlow<TCondition>, TFlow>();
        return this;
    }

    public void HandleFailureWith<TFlow>() where TFlow : class, IUsecaseFailureFlow<TCondition>
    {
        _failureRegisterer = (services) => services.AddScoped<IUsecaseFailureFlow<TCondition>, TFlow>();
    }

    public void Build(IServiceCollection services)
    {
        _successRegisterer?.Invoke(services);
        _failureRegisterer?.Invoke(services);
    }
}