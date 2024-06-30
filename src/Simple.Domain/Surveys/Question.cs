namespace Simple.Domain.Surveys;

public class Question
{
    private Question()
    {
        // EF
    }

    public Question(SurveyId surveyId, string title, bool mandatory)
    {
        SurveyId = surveyId;
        QuestionId = new QuestionId(Guid.NewGuid());
        Title = title;
        Mandatory = mandatory;
        CreatedAt = SystemTime.UtcNow();
    }

    public SurveyId SurveyId { get; init; }
    public QuestionId QuestionId { get; init; }
    public string Title { get; init; }
    public bool Mandatory { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}