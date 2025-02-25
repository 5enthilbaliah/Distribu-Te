namespace DistribuTe.Utilities.HttpFluent;

using Enumerations;

public interface IHeaderStart
{
    IHeaderMapper StartHeader(string key, string value, ParameterTypes parameterType);
    IActionMapper NoHeader();
}