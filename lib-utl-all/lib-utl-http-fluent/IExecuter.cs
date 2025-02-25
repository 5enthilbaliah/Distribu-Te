namespace DistribuTe.Utilities.HttpFluent;

using System.Net;
using Enumerations;

public interface IExecuter
{
    Task<TResult> ExecuteAsync<TResult>(bool useCamelCaseSerializer = false,
        CancellationToken cancellationToken = default)
        where TResult : class;

    Task<(TResult result, HttpStatusCode statusCode)> ExecuteWithStatusAsync<TResult>(
        bool useCamelCaseSerializer = false, CancellationToken cancellationToken = default)
        where TResult : class;

    Task<string> ExecuteRawAsync(CancellationToken cancellationToken = default);

    Task<(string result, HttpStatusCode statusCode)> ExecuteWithStatusRawAsync(
        CancellationToken cancellationToken = default);

    Task<(string result, HttpStatusCode statusCode)> ExecuteRawWithoutFaultHandlingAsync(
        CancellationToken cancellationToken = default);

    Task<TResult> ExecuteAsync<TResult>(JsonNamingPolicies jsonNamingPolicy,
        CancellationToken cancellationToken = default)
        where TResult : class;

    Task<(TResult result, HttpStatusCode statusCode)> ExecuteWithStatusAsync<TResult>(
        JsonNamingPolicies jsonNamingPolicy, CancellationToken cancellationToken = default)
        where TResult : class;
}