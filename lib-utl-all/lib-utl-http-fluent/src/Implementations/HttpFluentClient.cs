namespace DistribuTe.Utilities.HttpFluent.Implementations;

using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Web;
using Ardalis.GuardClauses;
using Enumerations;
using Exceptions;
using Microsoft.AspNetCore.Http;

// TODO:: Use result pattern

public class HttpFluentClient(HttpClient httpClient) : IUrlMapper, IQueryStringStart, IQueryStringMapper,
    IHeaderStart, IHeaderMapper, IActionMapper, IContentMapper, IExecuter
{
    private readonly HttpClient _httpClient = httpClient;
    private string? _url;
    private HttpMethod? _method;

    private readonly Dictionary<string, string> _queryStringKvs = new();

    private readonly Dictionary<string, IEnumerable<string>>
        _queryStringArrayKvs = new();

    private readonly Dictionary<string, string> _headerKvs = new();
    private readonly Dictionary<string, string> _contentKvs = new();

    private HttpContent? _content;

    // ReSharper disable once InconsistentNaming
    private static string PLAIN_TEXT => "text/plain";

    // ReSharper disable once InconsistentNaming
    private static string APPLICATION_JSON => "application/json";

    private static Func<KeyValuePair<string, string>, string> QueryStringGenerate =>
        (kv) => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}";

    private static Func<KeyValuePair<string, IEnumerable<string>>, IEnumerable<string>> QueryArrayStringGenerate =>
        kv => kv.Value.Select(value => $"{kv.Key}={HttpUtility.UrlEncode(value)}");

    private static Func<string, string, string> QueryStringAggregate =>
        (curr, next) => $"{curr}&{next}";

    private async Task<HttpResponseMessage> GetResponseAsync(CancellationToken cancellationToken = default)
    {
        Guard.Against.NullOrEmpty(_url);
        Guard.Against.Null(_method);

        var query = string.Empty;

        if (_queryStringKvs.Count > 0)
            query = _queryStringKvs.Select(QueryStringGenerate)
                .Aggregate(QueryStringAggregate);

        if (_queryStringArrayKvs.Count > 0)
        {
            var qryArray = _queryStringArrayKvs.SelectMany(QueryArrayStringGenerate)
                .Aggregate(QueryStringAggregate);

            if (string.IsNullOrEmpty(query))
                query = qryArray;
            else
                query += $"&{qryArray}";
        }

        if (!string.IsNullOrEmpty(query))
            _url += $"?{query}";

        var request = new HttpRequestMessage(_method, _url);
        foreach (var kv in _headerKvs)
        {
            if (request.Headers.Contains(kv.Key))
                request.Headers.Remove(kv.Key);
            request.Headers.Add(kv.Key, kv.Value);
        }

        if (_content == null)
            return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken)
                .ConfigureAwait(false);

        request.Content = _content;
        foreach (var kv in _contentKvs)
        {
            if (request.Content.Headers.Contains(kv.Key))
                request.Content.Headers.Remove(kv.Key);
            request.Content.Headers.Add(kv.Key, kv.Value);
        }

        return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken)
            .ConfigureAwait(false);
    }
    
    private async Task<TOut> HandleResponse<TOut>(HttpResponseMessage message,
        Func<Stream, HttpResponseMessage, Task<TOut>> handleSuccessWith,
        CancellationToken cancellationToken = default)
    {
        var statusCode = (int)message.StatusCode;
        switch (statusCode)
        {
            case >= 400 and <= 499:
            {
                var err = await message.Content.ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);
                err = $"Client error occurred while accessing url {_httpClient.BaseAddress}{_url} method {_method}" +
                      $"Error {err}";

                throw new ClientErrorException(err, message.StatusCode);
            }
            // ReSharper disable once PatternIsRedundant
            case >= 500 and <= 599:
            {
                var err = await message.Content.ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);
                err = $"Server error occurred while accessing url {_httpClient.BaseAddress}{_url} method {_method}" +
                      $"Error {err}";

                throw new ServerErrorException(err, message.StatusCode);
            }
            default:
            {
                await using var responseStream = await message.Content.ReadAsStreamAsync(cancellationToken)
                    .ConfigureAwait(false);

                return await handleSuccessWith(responseStream, message);
            }
        }
    }

    private async Task<TResult?> ExecuteGenericAsync<TResult>(
        Func<Stream, Task<TResult?>> onSuccess,
        CancellationToken cancellationToken = default)
    {
        using var result = await GetResponseAsync(cancellationToken)
            .ConfigureAwait(false);

        if (result.StatusCode == HttpStatusCode.NotFound)
            return default;
        
        return await HandleResponse(result, async (stream, _) => await onSuccess(stream), 
            cancellationToken).ConfigureAwait(false);
    }
    
    private async Task<(TResult?, HttpStatusCode)> ExecuteGenericWithStatusAsync<TResult>(
        Func<Stream, Task<TResult?>> onSuccess, bool skipFault = false,
        CancellationToken cancellationToken = default)
    {
        using var result = await GetResponseAsync(cancellationToken)
            .ConfigureAwait(false);

        if (result.StatusCode == HttpStatusCode.NotFound)
            return (default, HttpStatusCode.NotFound);
        if (result.StatusCode == HttpStatusCode.NoContent)
            return (default, HttpStatusCode.NoContent);

        if (skipFault)
        {
            await using var responseStream = await result.Content.ReadAsStreamAsync(cancellationToken)
                .ConfigureAwait(false);
            return (await onSuccess(responseStream), result.StatusCode);
        }

        return await HandleResponse(result, async (stream, message) => (await onSuccess(stream), message.StatusCode), 
            cancellationToken).ConfigureAwait(false);
    }
    
    public IQueryStringStart Url(string url)
    {
        _url = url;
        return this;
    }

    public IHeaderStart NoQueryString()
    {
        return this;
    }

    public IQueryStringMapper StartQueryString(string key, string value)
    {
        _queryStringKvs.Add(key, value);
        return this;
    }

    public IQueryStringMapper StartQueryString(string key, params string[] values)
    {
        _queryStringArrayKvs.Add(key, values);
        return this;
    }


    public IQueryStringMapper NextQueryString(string key, string value)
    {
        _queryStringKvs.Add(key, value);
        return this;
    }

    public IQueryStringMapper NextQueryString(string key, params string[] values)
    {
        _queryStringArrayKvs.Add(key, values);
        return this;
    }

    public IHeaderStart EndQueryString()
    {
        return this;
    }

    public IHeaderMapper StartHeader(string key, string value, ParameterTypes parameterType)
    {
        if (parameterType == ParameterTypes.Header)
        {
            _headerKvs.Add(key, value);
            return this;
        }

        _contentKvs.Add(key, value);
        return this;
    }

    public IActionMapper NoHeader()
    {
        return this;
    }

    public IHeaderMapper NextHeader(string key, string value, ParameterTypes parameterType)
    {
        if (parameterType == ParameterTypes.Header)
        {
            _headerKvs.Add(key, value);
            return this;
        }

        _contentKvs.Add(key, value);
        return this;
    }

    public IActionMapper EndHeader()
    {
        return this;
    }

    public IExecuter Get()
    {
        _method = HttpMethod.Get;
        return this;
    }

    public IContentMapper Post()
    {
        _method = HttpMethod.Post;
        return this;
    }

    public IContentMapper Put()
    {
        _method = HttpMethod.Put;
        return this;
    }

    public IContentMapper Patch()
    {
        _method = HttpMethod.Patch;
        return this;
    }

    public IExecuter Delete()
    {
        _method = HttpMethod.Delete;
        return this;
    }

    public IExecuter WithBody<TBody>(TBody body, bool useCamelCaseSerializer = false) where TBody : class
    {
        if (typeof(TBody).IsSubclassOf(typeof(HttpContent)))
        {
            _content = (body as HttpContent)!;
            return this;
        }

        if (typeof(TBody).FullName!.Equals("System.String", StringComparison.InvariantCultureIgnoreCase))
        {
            _content = new StringContent(body.ToString()!, Encoding.UTF8, PLAIN_TEXT);
            return this;
        }

        var json = useCamelCaseSerializer
            ? JsonSerializer.Serialize(body, new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
            : JsonSerializer.Serialize(body);

        _content = new StringContent(json, Encoding.UTF8, APPLICATION_JSON);
        return this;
    }

    public IExecuter WithPrimitiveBody<TPrimitive>(TPrimitive body) where TPrimitive : struct
    {
        var json = JsonSerializer.Serialize(body);

        _content = new StringContent(json, Encoding.UTF8, APPLICATION_JSON);
        return this;
    }

    public IExecuter WithFormBody<TForm>(TForm body) where TForm : class
    {
        var properties = body.GetType().GetProperties(
            BindingFlags.Instance | BindingFlags.Public).ToList();

        var multipartFormDataContent = new MultipartFormDataContent();

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(IFormFile))
            {
                var formFile = property.GetValue(body) as IFormFile;
                var fileContent = new StreamContent(formFile!.OpenReadStream())
                {
                    Headers =
                    {
                        ContentLength = formFile.Length,
                        ContentType = new MediaTypeHeaderValue(formFile.ContentType)
                    }
                };
                multipartFormDataContent.Add(fileContent, property.Name, formFile.FileName);
                continue;
            }

            var content = new StringContent((property.GetValue(body) ?? string.Empty).ToString()!);
            multipartFormDataContent.Add(content, property.Name);
        }

        _content = multipartFormDataContent;
        return this;
    }

    public async Task<TResult?> ExecuteAsync<TResult>(CancellationToken cancellationToken = default)
        where TResult : class
    {
        return await ExecuteGenericAsync(async stream => await JsonSerializer.DeserializeAsync<TResult>(stream,
                new JsonSerializerOptions
                {
                    AllowTrailingCommas = false
                }, cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<TResult?> ExecuteAsync<TResult>(JsonNamingPolicies jsonNamingPolicy,
        CancellationToken cancellationToken = default) where TResult : class
    {
        var jsonPolicy = jsonNamingPolicy switch
        {
            JsonNamingPolicies.Kebab => new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            },
            JsonNamingPolicies.Snake => new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            },
            JsonNamingPolicies.Camel => new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            },
            _ => new JsonSerializerOptions
            {
                AllowTrailingCommas = false
            },
        };
        
        return await ExecuteGenericAsync(async stream => await JsonSerializer.DeserializeAsync<TResult>(stream,
                jsonPolicy, cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<string?> ExecuteRawAsync(CancellationToken cancellationToken = default)
    {
        return await ExecuteGenericAsync(async stream =>
        {
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync(cancellationToken);
        }, cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<(TResult? result, HttpStatusCode statusCode)> ExecuteWithStatusAsync<TResult>(
        CancellationToken cancellationToken = default) where TResult : class
    {
        return await ExecuteGenericWithStatusAsync(async stream => await JsonSerializer.DeserializeAsync<TResult>(stream,
                new JsonSerializerOptions
                {
                    AllowTrailingCommas = false
                }, cancellationToken).ConfigureAwait(false),
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<(TResult? result, HttpStatusCode statusCode)> ExecuteWithStatusAsync<TResult>(
        JsonNamingPolicies jsonNamingPolicy,
        CancellationToken cancellationToken = default) where TResult : class
    {
        var jsonPolicy = jsonNamingPolicy switch
        {
            JsonNamingPolicies.Kebab => new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            },
            JsonNamingPolicies.Snake => new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            },
            JsonNamingPolicies.Camel => new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            },
            _ => new JsonSerializerOptions
            {
                AllowTrailingCommas = false
            },
        };
        
        return await ExecuteGenericWithStatusAsync(async stream => await JsonSerializer.DeserializeAsync<TResult>(stream,
                jsonPolicy, cancellationToken).ConfigureAwait(false),
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<(string? result, HttpStatusCode statusCode)> ExecuteWithStatusRawAsync(
        CancellationToken cancellationToken = default)
    {
        return await ExecuteGenericWithStatusAsync(async stream =>
        {
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync(cancellationToken);
        }, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<(string? result, HttpStatusCode statusCode)> ExecuteRawWithoutFaultHandlingAsync(
        CancellationToken cancellationToken = default)
    {
        return await ExecuteGenericWithStatusAsync(async stream =>
        {
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync(cancellationToken);
        }, skipFault: true, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}