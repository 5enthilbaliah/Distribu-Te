namespace DistribuTe.Utilities.HttpFluent.Implementations;

using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Web;
using Enumerations;
using Exceptions;
using Microsoft.AspNetCore.Http;

// TODO:: Use result pattern

public class HttpFluentClient(HttpClient httpClient) : IUrlMapper, IQueryStringStart, IQueryStringMapper, 
    IHeaderStart, IHeaderMapper, IActionMapper, IContentMapper, IExecuter
{
    private readonly HttpClient _httpClient = httpClient;
    private string _url;
    private HttpMethod _method;
    private Dictionary<string, string> _queryStringKvs = new Dictionary<string, string>();

    private Dictionary<string, IEnumerable<string>>
        _queryStringArrayKvs = new Dictionary<string, IEnumerable<string>>();

    private Dictionary<string, string> _headerKvs = new Dictionary<string, string>();
    private Dictionary<string, string> _contentKvs = new Dictionary<string, string>();
    private HttpContent _content;

    private async Task<HttpResponseMessage> GetResponseAsync(CancellationToken cancellationToken = default)
    {
        var query = string.Empty;

        if (_queryStringKvs.Count > 0)
            query = _queryStringKvs.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}")
                .Aggregate((curr, next) => $"{curr}&{next}");

        if (_queryStringArrayKvs.Count > 0)
        {
            foreach (var qarray in _queryStringArrayKvs)
            {
                var qryArray = qarray.Value.Select(value => $"{qarray.Key}={HttpUtility.UrlEncode(value)}")
                    .Aggregate((curr, next) => $"{curr}&{next}");

                if (string.IsNullOrEmpty(query))
                    query = qryArray;
                else
                    query += $"&{qryArray}";
            }
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

        if (_content != null)
        {
            request.Content = _content;
            foreach (var kv in _contentKvs)
            {
                if (request.Content.Headers.Contains(kv.Key))
                    request.Content.Headers.Remove(kv.Key);
                request.Content.Headers.Add(kv.Key, kv.Value);
            }
        }

        return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken)
            .ConfigureAwait(false);
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
            _content = new StringContent(body.ToString()!, Encoding.UTF8, "text/plain");
            return this;
        }

        var json = useCamelCaseSerializer
            ? JsonSerializer.Serialize(body, new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
            : JsonSerializer.Serialize(body);
        
        _content = new StringContent(body.ToString()!, Encoding.UTF8, "application/json");
        return this;
    }

    public IExecuter WithPrimitiveBody<TPrimitive>(TPrimitive body) where TPrimitive : struct
    {
        var json = JsonSerializer.Serialize(body);
        
        _content = new StringContent(body.ToString()!, Encoding.UTF8, "application/json");
        return this;
    }

    public IExecuter WithFormBody<TForm>(TForm body) where TForm : class
    {
        var properties = body.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .ToList();
        var formFields = properties.Where(p => p.PropertyType == typeof(IFormFile));
        var multipartFormDataContent = new MultipartFormDataContent();

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(IFormFile))
            {
                var formFile = property.GetValue(body) as IFormFile;
                var fileContent = new StreamContent(formFile.OpenReadStream())
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

            var content = new StringContent((property.GetValue(body) ?? string.Empty).ToString());
            multipartFormDataContent.Add(content, property.Name);
        }

        _content = multipartFormDataContent;
        return this;
    }

    public async Task<TResult> ExecuteAsync<TResult>(bool useCamelCaseDeserializer = false, CancellationToken cancellationToken = default) where TResult : class
    {
        using var result = await GetResponseAsync(cancellationToken)
            .ConfigureAwait(false);

        if (result.StatusCode == HttpStatusCode.NotFound)
            return default;

        var statusCode = (int)result.StatusCode;
        if (statusCode is >= 400 and <= 499)
        {
            var err = await result.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            err = $"Client error occurred while accessing url {_httpClient.BaseAddress}{_url} method {_method}" +
                  $"Error {err}";
            throw new ClientErrorException(err, result.StatusCode);
        }
        
        if (statusCode is >= 500 and <= 599)
        {
            var err = await result.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            err = $"Server error occurred while accessing url {_httpClient.BaseAddress}{_url} method {_method}" +
                  $"Error {err}";
            
            throw new ServerErrorException(err, result.StatusCode);
        }

        await using var responseStream = await result.Content.ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        return (useCamelCaseDeserializer
            ? await JsonSerializer.DeserializeAsync<TResult>(responseStream, new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }, cancellationToken).ConfigureAwait(false)
            : await JsonSerializer.DeserializeAsync<TResult>(responseStream, new JsonSerializerOptions
            {
                AllowTrailingCommas = false
            }, cancellationToken).ConfigureAwait(false))!;
    }

    public async Task<(TResult result, HttpStatusCode statusCode)> ExecuteWithStatusAsync<TResult>(bool useCamelCaseDeserializer = false,
        CancellationToken cancellationToken = default) where TResult : class
    {
        using var result = await GetResponseAsync(cancellationToken)
            .ConfigureAwait(false);

        if (result.StatusCode == HttpStatusCode.NotFound)
            return (default, HttpStatusCode.NotFound);
        if (result.StatusCode == HttpStatusCode.NoContent)
            return (default, HttpStatusCode.NoContent);
        
        var statusCode = (int)result.StatusCode;
        if (statusCode is >= 400 and <= 499)
        {
            var err = await result.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            err = $"Client error occurred while accessing url {_httpClient.BaseAddress}{_url} method {_method}" +
                  $"Error {err}";
            
            throw new ClientErrorException(err, result.StatusCode);
        }
        
        if (statusCode is >= 500 and <= 599)
        {
            var err = await result.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
            err = $"Server error occurred while accessing url {_httpClient.BaseAddress}{_url} method {_method}" +
                  $"Error {err}";
            
            throw new ServerErrorException(err, result.StatusCode);
        }
        
        await using var responseStream = await result.Content.ReadAsStreamAsync(cancellationToken)
            .ConfigureAwait(false);

        return (useCamelCaseDeserializer
            ? (await JsonSerializer.DeserializeAsync<TResult>(responseStream, new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }, cancellationToken).ConfigureAwait(false), result.StatusCode)
            : (await JsonSerializer.DeserializeAsync<TResult>(responseStream, new JsonSerializerOptions
            {
                AllowTrailingCommas = false
            }, cancellationToken).ConfigureAwait(false), result.StatusCode))!;
    }

    public async Task<string> ExecuteRawAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<(string result, HttpStatusCode statusCode)> ExecuteWithStatusRawAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<(string result, HttpStatusCode statusCode)> ExecuteRawWithoutFaultHandlingAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TResult> ExecuteAsync<TResult>(JsonNamingPolicies jsonNamingPolicy, CancellationToken cancellationToken = default) where TResult : class
    {
        throw new NotImplementedException();
    }

    public Task<(TResult result, HttpStatusCode statusCode)> ExecuteWithStatusAsync<TResult>(JsonNamingPolicies jsonNamingPolicy,
        CancellationToken cancellationToken = default) where TResult : class
    {
        throw new NotImplementedException();
    }
}