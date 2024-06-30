namespace Simple.Domain.Surveys;

public record QuestionId(Guid Value)
{
    public static implicit operator Guid(QuestionId id) => id.Value;
}