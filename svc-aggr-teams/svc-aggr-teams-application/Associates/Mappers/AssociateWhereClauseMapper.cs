// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Associates.Mappers;

using Domain.Entities;
using Framework.AppEssentials.Implementations;

public partial class AssociateWhereClauseMapper : WhereClauseMapper<AssociateAggregate, AssociateId>
{
    private const string ID = "id";
    private const string FIRST_NAME = "first_name";
    private const string LAST_NAME = "last_name";
    private const string MIDDLE_NAME = "middle_name";
    private const string GENDER = "gender";
    private const string EMAIL_ID = "email_id";
}