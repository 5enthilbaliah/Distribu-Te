namespace DistribuTe.Utilities.HttpFluent.Exceptions;

using System.Net;

public class ClientErrorException : IOException
{
    public HttpStatusCode StatusCode { get; }
    
    public ClientErrorException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        var code = (int)statusCode;
        if (code is < 400 or > 499)
            throw new ArgumentOutOfRangeException($"ClientErrorException can take only HttpStatusCode(s) 400-499");
        
        StatusCode = statusCode;
    }
}