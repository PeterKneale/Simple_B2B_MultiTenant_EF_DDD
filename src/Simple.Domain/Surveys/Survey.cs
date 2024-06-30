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

    public Question AddTextQuestion(string text, bool mandatory, int maxLength)
    {
        var question = Question.CreateTextQuestion(SurveyId, text, mandatory, maxLength);
        _questions.Add(question);
        return question;
    }

    public Question AddListQuestion(string text, bool mandatory, IEnumerable<string> options, bool single)
    {
        var question = Question.CreateListQuestion(SurveyId, text, mandatory, options, single);
        _questions.Add(question);
        return question;
    }

    public void RemoveQuestion(QuestionId questionId)
    {
        _questions.Remove(_questions.Single(q => q.QuestionId == questionId));
    }

    public SurveyId SurveyId { get; init; } = null!;

    public SurveyName Name { get; init; } = null!;

    public TenantId TenantId { get; init; } = null!;

    public DateTimeOffset CreatedAt { get; init; }

    public IReadOnlyCollection<Question> Questions => _questions;

    public int TotalQuestions => _questions.Count;
}