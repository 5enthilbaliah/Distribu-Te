namespace DistribuTe.Framework.AppEssentials.Behaviors;

using MediatR;

public class UserTrackBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IUserTrackable trackable)
            return await next();

        trackable!.User = "test-user";
        
        return await next();
    }
}