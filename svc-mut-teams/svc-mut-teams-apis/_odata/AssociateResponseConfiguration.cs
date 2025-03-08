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
        
        typeConfiguration.Property(a => a.First_Name)
            .Name = "first_name";
        
        typeConfiguration.Property(a => a.Last_Name)
            .Name = "last_name";
        
        typeConfiguration.Property(a => a.Middle_Name)
            .Name = "middle_name";
        
        typeConfiguration.Property(a => a.Email_Id)
            .Name = "email_id";
        
        typeConfiguration.Property(a => a.Gender)
            .Name = "gender";
    }
}