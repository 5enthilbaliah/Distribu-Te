// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Projects.Application.Projects.DataContracts;

using System.Text.Json.Serialization;
using Framework.AppEssentials;
using Shared;

public class ProjectModel : IModel
{
    public int Id { get; set; }
    public int Category_Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    
    public IList<SquadProjectElement>? Squad_Projects { get; set; }
    public ProjectCategoryElement? Category { get; set; }
    
    [JsonIgnore]
    public string ModelIdentifier  => $"{Id}";
}