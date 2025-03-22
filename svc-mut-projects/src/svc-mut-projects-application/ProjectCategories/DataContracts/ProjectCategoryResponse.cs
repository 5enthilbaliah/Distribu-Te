namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.DataContracts;

public class ProjectCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}