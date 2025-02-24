namespace DistribuTe.Framework.WorkFlowZ.Implementations;

using Microsoft.Extensions.DependencyInjection;

public class Usecase(IServiceProvider serviceProvider) : IUsecase
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;


    public async Task<bool> ExecuteAsync<TCondition>(TCondition condition, CancellationToken cancellationToken) 
        where TCondition : class, IUsecaseTriggerCondition
    {
        try
        {
            var successFlow = _serviceProvider.GetRequiredService<IUsecaseSuccessFlow<TCondition>>();
            await successFlow.Handle(condition, cancellationToken)
                .ConfigureAwait(false);

            return true;
        }
        catch (Exception)
        {
            //TODO:: Should handle exceptions within failure flow
            var failureFlow = _serviceProvider.GetRequiredService<IUsecaseFailureFlow<TCondition>>();
            await failureFlow.Handle(condition, cancellationToken)
                .ConfigureAwait(false);
            
            throw;
        }
    }
}