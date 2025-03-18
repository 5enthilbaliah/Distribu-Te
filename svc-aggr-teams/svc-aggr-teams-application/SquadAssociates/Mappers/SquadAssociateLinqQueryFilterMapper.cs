// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;

public class SquadAssociateLinqQueryFilterMapper : LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId>
{
    private const string SQUAD_ID = "squad_id";
    private const string ASSOCIATE_ID = "associate_id";
    private const string STARTED_ON = "started_on";
    private const string ENDED_ON = "ended_on";
    private const string CAPACITY = "capacity";
    
    #region Equal
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> EqualChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId == new SquadId(value.AsInt()) },
        { ASSOCIATE_ID, (value) => sqa => sqa.AssociateId == new AssociateId(value.AsInt()) }
    };
    #endregion
    
    #region NotEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId != new SquadId(value.AsInt()) },
        { ASSOCIATE_ID, (value) => sqa => sqa.AssociateId != new AssociateId(value.AsInt()) }
    };
    #endregion
    
    #region GreaterThan
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> GreaterThanChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId > new SquadId(value.AsInt()) },
        { ASSOCIATE_ID, (value) => sqa => sqa.AssociateId > new AssociateId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn > value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn > value.AsDateTime() },
        { CAPACITY, (value) => sqa => sqa.Capacity > value.AsDecimal() }
    };
    #endregion
    
    #region GreaterThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>>
        GreaterThanEqualChecks { get; }  = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId >= new SquadId(value.AsInt()) },
        { ASSOCIATE_ID, (value) => sqa => sqa.AssociateId >= new AssociateId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn >= value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn >= value.AsDateTime() },
        { CAPACITY, (value) => sqa => sqa.Capacity >= value.AsDecimal() }
    };
    #endregion
    
    #region LessThan
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> LesserThanChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId < new SquadId(value.AsInt()) },
        { ASSOCIATE_ID, (value) => sqa => sqa.AssociateId < new AssociateId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn < value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn < value.AsDateTime() },
        { CAPACITY, (value) => sqa => sqa.Capacity < value.AsDecimal() }
    };
    #endregion
    
    #region LessThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>>
        LesserThanEqualChecks { get; } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId <= new SquadId(value.AsInt()) },
        { ASSOCIATE_ID, (value) => sqa => sqa.AssociateId <= new AssociateId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn <= value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn <= value.AsDateTime() },
        { CAPACITY, (value) => sqa => sqa.Capacity <= value.AsDecimal() }
    };
    #endregion
    
    #region StartsWith
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> StartsWithChecks
    {
        get;
    } = new();
    #endregion
    
    #region Contains
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new();
    #endregion
    
    #region EndsWith
    protected override Dictionary<string, Func<string, Expression<Func<SquadAssociateAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new();
    #endregion
}