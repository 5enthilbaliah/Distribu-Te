﻿// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Base;

public class AssociateModel
{
    public int Id { get; set; }
    public string First_Name { get; set; } = null!;
    public string Last_Name { get; set; } = null!;
    public string? Middle_Name { get; set; }
    public char Gender { get; set; }
    public string Email_Id { get; set; } = null!;
    
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }
}