namespace DistribuTe.Framework.ApiEssentials.Odata;

using Microsoft.OData.ModelBuilder;

public abstract class OdataResponseConfiguration<TResponse>
    where TResponse : class
{
    public abstract void Configure(EntityTypeConfiguration<TResponse> typeConfiguration);
}