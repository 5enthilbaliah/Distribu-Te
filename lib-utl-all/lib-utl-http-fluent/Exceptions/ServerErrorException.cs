namespace DistribuTe.Utilities.HttpFluent.Exceptions;

using System.Net;

public class ServerErrorException : IOException
{
    public HttpStatusCode StatusCode { get; }
    
    public ServerErrorException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        var code = (int)statusCode;
        if (code is < 500 or > 599)
            throw new ArgumentOutOfRangeException($"ServerErrorException can take only HttpStatusCode(s) 500-599");
        
        StatusCode = statusCode;
    }
}