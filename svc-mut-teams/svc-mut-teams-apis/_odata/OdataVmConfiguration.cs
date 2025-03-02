// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Microsoft.OData.ModelBuilder;

public abstract class OdataVmConfiguration<TViewModel>
    where TViewModel : class
{
    public abstract void Configure(EntityTypeConfiguration<TViewModel> typeConfiguration);
}