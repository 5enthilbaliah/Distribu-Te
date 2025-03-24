namespace DistribuTe.Framework.AppEssentials.Behaviors;

using MediatR;

public class TokenTrackBehavior<TRequest, TResponse>(IRequestContext requestContext) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IRequestContext _requestContext = requestContext ?? throw new ArgumentNullException(nameof(requestContext));
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ITokenTrackable trackable)
            return await next();

        trackable!.User = _requestContext.UserIdentity;
        trackable.Token = _requestContext.Token;
        
        return await next();
    }
}