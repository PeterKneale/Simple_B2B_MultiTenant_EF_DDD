using Simple.Domain.Surveys.Questions;

namespace Simple.Domain.Surveys;

public class Question
{
    private Question()
    {
        // EF
    }

    private Question(SurveyId surveyId, QuestionType type)
    {
        SurveyId = surveyId;
        QuestionId = new QuestionId(Guid.NewGuid());
        Type = type;
        CreatedAt = SystemTime.UtcNow();
    }

    public static Question CreateTextQuestion(SurveyId surveyId, string title, bool mandatory, int maxLength)
    {
        return new Question(surveyId, new TextQuestionType(title, mandatory, maxLength));
    }

    public static Question CreateListQuestion(SurveyId surveyId, string title, bool mandatory, IEnumerable<string> options, bool single)
    {
        return new Question(surveyId, new ListQuestionType(title, mandatory, options, single));
    }

    public SurveyId SurveyId { get; init; } = null!;
    public QuestionType Type { get; init; } = null!;
    public QuestionId QuestionId { get; init; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
}