// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.Associates.Models;
using Microsoft.OData.ModelBuilder;

public class AssociateVmConfiguration : OdataVmConfiguration<AssociateVm>
{
    public override void Configure(EntityTypeConfiguration<AssociateVm> typeConfiguration)
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