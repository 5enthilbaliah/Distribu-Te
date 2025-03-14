﻿namespace DistribuTe.Aggregates.Teams.Domain;

public interface IEntity<TId>
    where TId : class
{
    public TId Id { get; set; }
}