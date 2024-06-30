using Simple.Domain.Tenants;

namespace Simple.Domain.Surveys;

public class Survey : IAggregateRoot
{
    private readonly List<Question> _questions = [];

    private Survey()
    {
        // EF
    }

    public Survey(TenantId tenantId, SurveyId surveyId, SurveyName name)
    {
        TenantId = tenantId;
        SurveyId = surveyId;
        Name = name;
        CreatedAt = SystemTime.UtcNow();
    }

    public Question AddQuestion(string text, bool mandatory)
    {
        var question = new Question(SurveyId, text, mandatory);
        _questions.Add(question);
        return question;
    }

    public void RemoveQuestion(QuestionId questionId)
    {
        _questions.Remove(_questions.Single(q => q.QuestionId == questionId));
    }

    public SurveyId SurveyId { get; init; }

    public SurveyName Name { get; init; }

    public TenantId TenantId { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public IReadOnlyCollection<Question> Questions => _questions;

    public int TotalQuestions => _questions.Count;
}