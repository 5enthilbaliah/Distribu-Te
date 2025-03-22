namespace DistribuTe.Utilities.HttpFluent;

using Enumerations;

public interface IHeaderMapper
{
    IHeaderMapper NextHeader(string key, string value, ParameterTypes parameterType);
    IActionMapper EndHeader();
}