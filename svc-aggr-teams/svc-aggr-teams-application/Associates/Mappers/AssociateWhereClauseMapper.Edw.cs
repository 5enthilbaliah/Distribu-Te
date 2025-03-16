namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using System.Linq.Expressions;
using Domain.Entities;

public partial class AssociateWhereClauseMapper
{
    protected override Dictionary<string, Func<string, Expression<Func<AssociateAggregate, bool>>>> EndsWithChecks
    {
        get;
    } = new()
    {
        { FIRST_NAME, (value) => associate => associate.FirstName.EndsWith(value) },
        { LAST_NAME, (value) => associate => associate.LastName.EndsWith(value) },
        { EMAIL_ID, (value) => associate => associate.EmailId.EndsWith(value) }
    };
}