// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.Associates.Models;
using Microsoft.OData.ModelBuilder;

public class AssociateVmConfiguration : OdataVmConfiguration<AssociateVm>
{
    public override void Configure(EntityTypeConfiguration<AssociateVm> typeConfiguration)
    {
        typeConfiguration.Property(a => a.FirstName)
            .IsRequired()
            .Name = "first_name";
        
        typeConfiguration.Property(a => a.LastName)
            .IsRequired()
            .Name = "last_name";
        
        typeConfiguration.Property(a => a.MiddleName)
            .Name = "middle_name";

        typeConfiguration.Property(a => a.EmailId)
            .IsRequired()
            .Name = "email_id";

        typeConfiguration.Property(a => a.Gender)
            .IsRequired()
            .Name = "gender";
    }
}