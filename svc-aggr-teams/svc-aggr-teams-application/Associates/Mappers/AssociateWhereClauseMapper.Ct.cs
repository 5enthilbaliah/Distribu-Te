namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using System.Linq.Expressions;
using Domain.Entities;

public partial class AssociateWhereClauseMapper
{
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> ContainsChecks
    {
        get;
    } = new()
    {
        { FIRST_NAME, (value) => associate => associate.FirstName.Contains(value) },
        { LAST_NAME, (value) => associate => associate.LastName.Contains(value) },
        { EMAIL_ID, (value) => associate => associate.EmailId.Contains(value) }
    };
}