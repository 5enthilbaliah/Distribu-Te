// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
namespace DistribuTe.Aggregates.Projects.Application.Shared;

public class ProjectCategoryElement
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}