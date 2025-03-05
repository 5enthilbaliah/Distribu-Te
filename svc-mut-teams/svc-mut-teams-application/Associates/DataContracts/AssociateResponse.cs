namespace DistribuTe.Mutators.Teams.Application.Associates.DataContracts;

public class AssociateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public char Gender { get; set; }
    public string EmailId { get; set; } = null!;
}