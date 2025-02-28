// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

public class BaseProjectCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}

public class ProjectCategoryAggregate : BaseProjectCategory
{
    public virtual ICollection<ProjectAggregate> Projects { get; set; }
}