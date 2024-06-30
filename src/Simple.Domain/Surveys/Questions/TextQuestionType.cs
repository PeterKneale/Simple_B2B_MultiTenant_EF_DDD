namespace Simple.Domain.Surveys.Questions;

public class TextQuestionType(string title, bool mandatory, int maxLength)
    : QuestionType(QuestionTypeNames.Text, title, mandatory)
{
    public int MaxLength { get; init; } = maxLength;
}