// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Application.Base;

public class AssociateElement
{
    public int Id { get; set; }
    public string First_Name { get; set; } = null!;
    public string Last_Name { get; set; } = null!;
    public string? Middle_Name { get; set; }
    public char Gender { get; set; }
    public string Email_Id { get; set; } = null!;
}