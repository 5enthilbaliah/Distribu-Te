// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.Associates.DataContracts;
using Framework.OData;
using Microsoft.OData.ModelBuilder;

public class AssociateResponseConfiguration : OdataResponseConfiguration<AssociateResponse>
{
    public override void Configure(EntityTypeConfiguration<AssociateResponse> typeConfiguration)
    {
        typeConfiguration.Property(a => a.Id)
            .Name = "id";
        
        typeConfiguration.Property(a => a.FirstName)
            .Name = "first_name";
        
        typeConfiguration.Property(a => a.LastName)
            .Name = "last_name";
        
        typeConfiguration.Property(a => a.MiddleName)
            .Name = "middle_name";

        typeConfiguration.Property(a => a.EmailId)
            .Name = "email_id";

        typeConfiguration.Property(a => a.Gender)
            .Name = "gender";
    }
}