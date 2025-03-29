// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials.DataTypes;
using Framework.AppEssentials.Linq;

public class ProjectCategoryEntityLinqMapper : EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId>
{
    private const string ID = "id";
    private const string NAME = "name";
    private const string CODE = "code";
    
    #region Sort by ascending
    protected override Dictionary<string, Func<IQueryable<ProjectCategoryAggregate>, IQueryable<ProjectCategoryAggregate>>>
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
    protected override Dictionary<string, Func<IQueryable<ProjectCategoryAggregate>, IQueryable<ProjectCategoryAggregate>>>
        DescendingSorters
    {
        get;
    } = new()
    {
        { ID, queryable => queryable.SafeDescendingOrder(x => x.Id) }
    };
    #endregion
    
    #region Equal
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> EqualChecks
    {
        get; 
    } = new()
    {
        { ID, (value) => category => category.Id == new ProjectCategoryId(value.AsInt()) },
        { NAME, (value) => category => category.Name == value },
        { CODE, (value) => category => category.Code == value }
    };
    #endregion
    
    #region NotEqual

    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => category => category.Id != new ProjectCategoryId(value.AsInt()) },
        { NAME, (value) => category => category.Name != value },
        { CODE, (value) => category => category.Code != value }
    };
    #endregion
    
    #region GreaterThan
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> GreaterThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => category => category.Id > new ProjectCategoryId(value.AsInt()) }
    };
    #endregion

    #region GreaterThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> GreaterThanEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => category => category.Id >= new ProjectCategoryId(value.AsInt()) }
    };
    #endregion

    #region LessThan
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> LesserThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => category => category.Id < new ProjectCategoryId(value.AsInt()) }
    };
    #endregion

    #region LessThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> LesserThanEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => category => category.Id <= new ProjectCategoryId(value.AsInt()) }
    };
    #endregion
    
    #region StartsWith
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> StartsWithChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => category => category.Name.StartsWith(value) }
    };
    #endregion
    
    #region Contains
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => category => category.Name.Contains(value) }
    };
    #endregion

    #region EndsWith
    protected override Dictionary<string, Func<string, Expression<Func<ProjectCategoryAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new()
    {
        { NAME, (value) => category => category.Name.EndsWith(value) }
    };
    #endregion
}