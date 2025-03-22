// ReSharper disable InconsistentNaming
namespace DistribuTe.Mutators.Projects.Application.Projects.DataContracts;

public class ProjectRequest
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int Category_Id { get; set; } 
    public string? Description { get; set; }
}