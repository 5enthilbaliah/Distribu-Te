﻿// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials.DataTypes;
using Framework.AppEssentials.Linq;

public class AssociateEntityLinqMapper : EntityLinqMapper<AssociateAggregate, AssociateId>
{
    private const string ID = "id";
    private const string FIRST_NAME = "first_name";
    private const string LAST_NAME = "last_name";
    private const string MIDDLE_NAME = "middle_name";
    private const string GENDER = "gender";
    private const string EMAIL_ID = "email_id";
    
    #region Sort by ascending
    protected override Dictionary<string, Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>>
        AscendingSorters
    {
        get;
    } = new()
    {
        { ID, queryable => queryable.SafeAscendingOrder(x => x.Id) },
        { FIRST_NAME, queryable => queryable.SafeAscendingOrder(x => x.FirstName) },
        { LAST_NAME, queryable => queryable.SafeAscendingOrder(x => x.LastName) }
    };
    #endregion
    
    #region Sort by descending
    protected override Dictionary<string, Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>>
        DescendingSorters
    {
        get;
    } = new()
    {
        { ID, queryable => queryable.SafeDescendingOrder(x => x.Id) }
    };
    #endregion
    
    #region Equal
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> EqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id == new AssociateId(value.AsInt()) },
        { FIRST_NAME, (value) => associate => associate.FirstName == value },
        { LAST_NAME, (value) => associate => associate.LastName == value },
        { MIDDLE_NAME, (value) => associate => associate.MiddleName == value },
        { GENDER, (value) => associate => associate.Gender == value[0] },
        { EMAIL_ID, (value) => associate => associate.EmailId == value }
    };
    #endregion
    
    #region NotEqual
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id != new AssociateId(value.AsInt()) },
        { FIRST_NAME, (value) => associate => associate.FirstName != value },
        { LAST_NAME, (value) => associate => associate.LastName != value }
    };
    #endregion
    
    #region GreaterThan
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> GreaterThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id > new AssociateId(value.AsInt()) }
    };
    #endregion
    
    #region GreaterThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>>
        GreaterThanEqualChecks { get; }  = new()
    {
        { ID, (value) => associate => associate.Id >= new AssociateId(value.AsInt()) }
    };
    #endregion
    
    #region LessThan
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> LesserThanChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id < new AssociateId(value.AsInt()) }
    };
    #endregion
    
    #region LessThanOrEqual
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>>
        LesserThanEqualChecks { get; } = new()
    {
        { ID, (value) => associate => associate.Id <= new AssociateId(value.AsInt()) }
    };
    #endregion
    
    #region StartsWith
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> StartsWithChecks
    {
        get;
    } = new()
    {
        { FIRST_NAME, (value) => associate => associate.FirstName.StartsWith(value) },
        { LAST_NAME, (value) => associate => associate.LastName.StartsWith(value) },
        { EMAIL_ID, (value) => associate => associate.EmailId.StartsWith(value) }
    };
    #endregion
    
    #region Contains
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new()
    {
        { FIRST_NAME, (value) => associate => associate.FirstName.Contains(value) },
        { LAST_NAME, (value) => associate => associate.LastName.Contains(value) },
        { EMAIL_ID, (value) => associate => associate.EmailId.Contains(value) }
    };
    #endregion
    
    #region EndsWith
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new()
    {
        { FIRST_NAME, (value) => associate => associate.FirstName.EndsWith(value) },
        { LAST_NAME, (value) => associate => associate.LastName.EndsWith(value) },
        { EMAIL_ID, (value) => associate => associate.EmailId.EndsWith(value) }
    };
    #endregion
}