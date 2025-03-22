namespace DistribuTe.Utilities.HttpFluent;

public interface IQueryStringStart
{
    IQueryStringMapper StartQueryString(string key, string value);
    IQueryStringMapper StartQueryString(string key, params string [] value);
    IHeaderStart NoQueryString();
}