namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories.DataContracts;

using System.Text.Json.Serialization;
using Framework.AppEssentials;
using Shared;

public class ProjectCategoryModel : IModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    
    public IList<ProjectElement>? Projects { get; set; }
    
    [JsonIgnore]
    public string ModelIdentifier => $"{Id}";
}