﻿// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

public class BaseDeployment
{
    public string Name { get; set; } = null!;
    public int SquadId { get; set; }
    public int EnvironmentId { get; set; }
    public DateTime PlannedStart { get; set; }
    public DateTime PlannedEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public string Comments { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public int StatusId { get; set; }
}

public class DeploymentAggregate : BaseDeployment
{
    public int Id { get; set; }
    public virtual SquadAggregate Squad { get; set; }
    public virtual EnvironmentAggregate Environment { get; set; }
    public virtual IList<DeploymentItemAggregate> DeploymentItems { get; set; }
    public virtual DeploymentStatusAggregate Status { get; set; }
}