namespace Simple.Domain.Surveys;

public record SurveyId(Guid Value)
{
    public static implicit operator Guid(SurveyId id) => id.Value;
}