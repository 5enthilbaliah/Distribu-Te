namespace DistribuTe.Utilities.HttpFluent;

public interface IQueryStringMapper
{
    IQueryStringMapper NextQueryString(string key, string value);
    IQueryStringMapper NextQueryString(string key, params string [] values);
    IHeaderStart EndQueryString();
}