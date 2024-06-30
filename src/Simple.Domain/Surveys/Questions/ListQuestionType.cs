namespace Simple.Domain.Surveys.Questions;

public class ListQuestionType(string title, bool mandatory, IEnumerable<string> options, bool single)
    : QuestionType(QuestionTypeNames.List, title, mandatory)
{
    public IEnumerable<string> Options { get; init; } = options;
    public bool Single { get; init; } = single;
}

public static class QuestionTypeNames
{
    public const string List = "list";
    public const string Text = "text";
}