namespace DistribuTe.Framework.OData;

using Microsoft.OData.ModelBuilder;

public static class OdataConfigurationExtensions
{
    public static void AddOdataConfigurations<TConf, TViewModel>(this ODataConventionModelBuilder builder)
        where TViewModel : class
        where TConf : OdataVmConfiguration<TViewModel>, new()
    {
        var config = new TConf();
        config.Configure(builder.EntityType<TViewModel>());
    }
}