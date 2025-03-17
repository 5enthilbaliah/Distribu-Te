// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Squads.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Implementations;

public class SquadLinqQueryFilterMapper : LinqQueryFilterMapper<SquadAggregate, SquadId>
{
    private const string ID = "id";
    private const string NAME = "name";
    private const string CODE = "code";

    #region Equal
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> EqualChecks
    {
        get; 
    } = new()
    {
        { ID, (value) => associate => associate.Id == new SquadId(value.AsInt()) },
        { NAME, (value) => associate => associate.Name == value },
        { CODE, (value) => associate => associate.Code == value }
    };
    #endregion
    
    #region NotEqual

    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id != new SquadId(value.AsInt()) },
        { NAME, (value) => associate => associate.Name != value },
        { CODE, (value) => associate => associate.Code != value }
    };
    #endregion
    
    #region GreaterThan
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> GreaterThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id > new SquadId(value.AsInt()) }
    };
    #endregion

    #region GreaterThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> GreaterThanEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id >= new SquadId(value.AsInt()) }
    };
    #endregion

    #region LessThan
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> LesserThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id < new SquadId(value.AsInt()) }
    };
    #endregion

    #region LessThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> LesserThanEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id <= new SquadId(value.AsInt()) }
    };
    #endregion
    
    #region StartsWith
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> StartsWithChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => associate => associate.Name.StartsWith(value) }
    };
    #endregion
    
    #region Contains
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => associate => associate.Name.Contains(value) }
    };
    #endregion

    #region EndsWith
    protected override Dictionary<string, Func<string, Expression<Func<SquadAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => associate => associate.Name.EndsWith(value) }
    };
    #endregion
}