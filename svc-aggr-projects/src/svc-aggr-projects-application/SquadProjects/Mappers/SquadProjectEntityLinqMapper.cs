// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Projects.Application.SquadProjects.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials.DataTypes;
using Framework.AppEssentials.Linq;

public class SquadProjectEntityLinqMapper : EntityLinqMapper<SquadProjectAggregate, SquadProjectId>
{
    private const string SQUAD_ID = "squad_id";
    private const string PROJECT_ID = "project_id";
    private const string STARTED_ON = "started_on";
    private const string ENDED_ON = "ended_on";
    
    #region Sort by ascending
    protected override Dictionary<string, Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>>>
        AscendingSorters
    {
        get;
    } = new()
    {
        { SQUAD_ID, queryable => queryable.SafeAscendingOrder(x => x.SquadId) },
        { PROJECT_ID, queryable => queryable.SafeAscendingOrder(x => x.ProjectId) },
        { STARTED_ON, queryable => queryable.SafeAscendingOrder(x => x.StartedOn) }
    };
    #endregion
    
    #region Sort by descending
    protected override Dictionary<string, Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>>>
        DescendingSorters
    {
        get;
    } = new()
    {
        { SQUAD_ID, queryable => queryable.SafeDescendingOrder(x => x.SquadId) },
        { PROJECT_ID, queryable => queryable.SafeDescendingOrder(x => x.ProjectId) },
        { STARTED_ON, queryable => queryable.SafeDescendingOrder(x => x.EndedOn) }
    };
    #endregion
    
    #region Equal
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> EqualChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId == new SquadId(value.AsInt()) },
        { PROJECT_ID, (value) => sqa => sqa.ProjectId == new ProjectId(value.AsInt()) }
    };
    #endregion
    
    #region NotEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId != new SquadId(value.AsInt()) },
        { PROJECT_ID, (value) => sqa => sqa.ProjectId != new ProjectId(value.AsInt()) }
    };
    #endregion
    
    #region GreaterThan
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> GreaterThanChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId > new SquadId(value.AsInt()) },
        { PROJECT_ID, (value) => sqa => sqa.ProjectId > new ProjectId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn > value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn > value.AsDateTime() }
    };
    #endregion
    
    #region GreaterThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>>
        GreaterThanEqualChecks { get; }  = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId >= new SquadId(value.AsInt()) },
        { PROJECT_ID, (value) => sqa => sqa.ProjectId >= new ProjectId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn >= value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn >= value.AsDateTime() }
    };
    #endregion
    
    #region LessThan
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> LesserThanChecks
    {
        get;
    } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId < new SquadId(value.AsInt()) },
        { PROJECT_ID, (value) => sqa => sqa.ProjectId < new ProjectId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn < value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn < value.AsDateTime() }
    };
    #endregion
    
    #region LessThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>>
        LesserThanEqualChecks { get; } = new()
    {
        { SQUAD_ID, (value) => sqa => sqa.SquadId <= new SquadId(value.AsInt()) },
        { PROJECT_ID, (value) => sqa => sqa.ProjectId <= new ProjectId(value.AsInt()) },
        { STARTED_ON, (value) => sqa => sqa.StartedOn <= value.AsDateTime() },
        { ENDED_ON, (value) => sqa => sqa.EndedOn <= value.AsDateTime() }
    };
    #endregion
    
    #region StartsWith
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> StartsWithChecks
    {
        get;
    } = new();
    #endregion
    
    #region Contains
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new();
    #endregion
    
    #region EndsWith
    protected override Dictionary<string, Func<string, Expression<Func<SquadProjectAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new();
    #endregion
}