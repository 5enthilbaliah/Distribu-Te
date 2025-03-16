// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Base;
using Framework.AppEssentials;

public class AssociateModel : IModel
{
    public int Id { get; set; }
    public string First_Name { get; set; } = null!;
    public string Last_Name { get; set; } = null!;
    public string? Middle_Name { get; set; }
    public char Gender { get; set; }
    public string Email_Id { get; set; } = null!;
    
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }

    public string ModelIdentifier => $"{Id}";
}