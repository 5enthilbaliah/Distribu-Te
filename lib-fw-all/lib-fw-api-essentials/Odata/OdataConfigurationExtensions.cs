namespace DistribuTe.Framework.ApiEssentials.Odata;

using Microsoft.OData.ModelBuilder;

public static class OdataConfigurationExtensions
{
    public static void AddOdataConfigurations<TConf, TResponse>(this ODataConventionModelBuilder builder)
        where TResponse : class
        where TConf : OdataResponseConfiguration<TResponse>, new()
    {
        var config = new TConf();
        config.Configure(builder.EntityType<TResponse>());
    }
}