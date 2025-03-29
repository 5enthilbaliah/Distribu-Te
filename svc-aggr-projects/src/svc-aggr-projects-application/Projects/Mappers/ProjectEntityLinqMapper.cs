// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Projects.Application.Projects.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials.DataTypes;
using Framework.AppEssentials.Linq;

public class ProjectEntityLinqMapper : EntityLinqMapper<ProjectAggregate, ProjectId>
{
    private const string ID = "id";
    private const string NAME = "name";
    private const string CODE = "code";
    
    #region Sort by ascending
    protected override Dictionary<string, Func<IQueryable<ProjectAggregate>, IQueryable<ProjectAggregate>>>
        AscendingSorters
    {
        get;
    } = new()
    {
        { ID, queryable => queryable.SafeAscendingOrder(x => x.Id) },
        { NAME, queryable => queryable.SafeAscendingOrder(x => x.Name) },
        { CODE, queryable => queryable.SafeAscendingOrder(x => x.Code) }
    };
    #endregion
    
    #region Sort by descending
    protected override Dictionary<string, Func<IQueryable<ProjectAggregate>, IQueryable<ProjectAggregate>>>
        DescendingSorters
    {
        get;
    } = new()
    {
        { ID, queryable => queryable.SafeDescendingOrder(x => x.Id) }
    };
    #endregion
    
    #region Equal
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> EqualChecks
    {
        get; 
    } = new()
    {
        { ID, (value) => project => project.Id == new ProjectId(value.AsInt()) },
        { NAME, (value) => project => project.Name == value },
        { CODE, (value) => project => project.Code == value }
    };
    #endregion
    
    #region NotEqual

    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => project => project.Id != new ProjectId(value.AsInt()) },
        { NAME, (value) => project => project.Name != value },
        { CODE, (value) => project => project.Code != value }
    };
    #endregion
    
    #region GreaterThan
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> GreaterThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => project => project.Id > new ProjectId(value.AsInt()) }
    };
    #endregion

    #region GreaterThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> GreaterThanEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => project => project.Id >= new ProjectId(value.AsInt()) }
    };
    #endregion

    #region LessThan
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> LesserThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => project => project.Id < new ProjectId(value.AsInt()) }
    };
    #endregion

    #region LessThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> LesserThanEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => project => project.Id <= new ProjectId(value.AsInt()) }
    };
    #endregion
    
    #region StartsWith
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> StartsWithChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => project => project.Name.StartsWith(value) }
    };
    #endregion
    
    #region Contains
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => project => project.Name.Contains(value) }
    };
    #endregion

    #region EndsWith
    protected override Dictionary<string, Func<string, Expression<Func<ProjectAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => project => project.Name.EndsWith(value) }
    };
    #endregion
}