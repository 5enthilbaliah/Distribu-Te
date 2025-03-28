﻿// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using ProjectEntities;

public class ProjectCategoryAggregate : BaseProjectCategory
{
    public int Id { get; set; }
    public virtual ICollection<ProjectAggregate> Projects { get; set; }
}