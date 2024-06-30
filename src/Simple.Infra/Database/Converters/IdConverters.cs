

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simple.Domain.Tenants;
using Simple.Domain.Users;
using Simple.Domain.Surveys;

namespace Simple.Infra.Database.Converters;


public class TenantIdConverter() : ValueConverter<TenantId, Guid>(id => id.Value, guid => new TenantId(guid));


public class UserIdConverter() : ValueConverter<UserId, Guid>(id => id.Value, guid => new UserId(guid));


public class SurveyIdConverter() : ValueConverter<SurveyId, Guid>(id => id.Value, guid => new SurveyId(guid));


public class QuestionIdConverter() : ValueConverter<QuestionId, Guid>(id => id.Value, guid => new QuestionId(guid));

