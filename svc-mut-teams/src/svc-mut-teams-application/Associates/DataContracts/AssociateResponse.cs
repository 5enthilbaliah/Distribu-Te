// ReSharper disable InconsistentNaming
namespace DistribuTe.Mutators.Teams.Application.Associates.DataContracts;

public class AssociateResponse
{
    public int Id { get; set; }
    public string First_Name { get; set; } = null!;
    public string Last_Name { get; set; } = null!;
    public string? Middle_Name { get; set; }
    public char Gender { get; set; }
    public string Email_Id { get; set; } = null!;
}