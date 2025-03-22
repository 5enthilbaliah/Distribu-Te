namespace DistribuTe.Mutators.Projects.Domain.Entities;

using DistribuTe.Domain.ProjectEntities;
using Framework.DomainEssentials;

public record SquadId(int Value);

public record SquadProjectId(SquadId SquadId, ProjectId ProjectId);

public class SquadProject : BaseSquadProject, IEntity<SquadProjectId>, IAuditableEntity
{
    public SquadProjectId Id { get; set; } = null!;

    public ProjectId ProjectId { get; set; } = null!;

    public SquadId SquadId { get; set; } = null!;
}