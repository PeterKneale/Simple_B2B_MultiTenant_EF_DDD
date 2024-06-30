namespace Simple.Domain.Surveys;

public record SurveyName(string Value)
{
    public static implicit operator string(SurveyName name) => name.Value;

    public static SurveyName Create(string s)
    {
        return new SurveyName(s);
    }
}