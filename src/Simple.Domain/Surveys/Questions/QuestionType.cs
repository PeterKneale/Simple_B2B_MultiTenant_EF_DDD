namespace Simple.Domain.Surveys.Questions;

public abstract class QuestionType(string type, string title, bool mandatory)
{
    public string Type { get; private init; } = type;
    public string Title { get; private init; } = title;
    public bool Mandatory { get; private init; } = mandatory;
}