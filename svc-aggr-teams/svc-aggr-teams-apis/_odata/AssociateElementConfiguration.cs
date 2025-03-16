// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.Base;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class AssociateElementConfiguration : OdataResponseConfiguration<AssociateElement>
{
    public override void Configure(EntityTypeConfiguration<AssociateElement> typeConfiguration)
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