using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simple.Domain.Surveys;

namespace Simple.Infra.Database.Converters;

public class SurveyNameConverter() : ValueConverter<SurveyName, string>(name => name.Value, s => SurveyName.Create(s));