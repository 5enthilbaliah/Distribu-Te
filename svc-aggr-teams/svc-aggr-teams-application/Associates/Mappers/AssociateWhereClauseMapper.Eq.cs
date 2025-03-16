namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using System.Linq.Expressions;
using Domain.Entities;
using Framework.AppEssentials;

public partial class AssociateWhereClauseMapper
{
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
}