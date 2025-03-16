namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials;

public partial class AssociateWhereClauseMapper
{
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> NotEqualChecks
    {
        get;
    } = new()
    {
        { ID, (value) => associate => associate.Id != new AssociateId(value.AsInt()) },
        { FIRST_NAME, (value) => associate => associate.FirstName != value },
        { LAST_NAME, (value) => associate => associate.LastName != value }
    };
}