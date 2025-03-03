namespace DistribuTe.Framework.OData;

using Microsoft.OData.ModelBuilder;

public abstract class OdataVmConfiguration<TViewModel>
    where TViewModel : class
{
    public abstract void Configure(EntityTypeConfiguration<TViewModel> typeConfiguration);
}