namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.DataContracts;

public class ProjectCategoryRequest
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}