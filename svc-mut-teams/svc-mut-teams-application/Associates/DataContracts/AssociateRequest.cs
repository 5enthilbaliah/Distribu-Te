namespace DistribuTe.Mutators.Teams.Application.Associates.DataContracts;

using System.Text.Json.Serialization;

public class AssociateRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public char Gender { get; set; }
    public string EmailId { get; set; } = null!;
}