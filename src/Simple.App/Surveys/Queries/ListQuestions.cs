using Simple.Domain.Surveys;
using Simple.Domain.Surveys.Specifications;

namespace Simple.App.Surveys.Queries;

public static class ListQuestions
{
    public record Query(Guid SurveyId) : IRequest<IEnumerable<Result>>;

    public record Result(Guid QuestionId, string Title, bool Mandatory, string Type);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.SurveyId).NotEmpty();
        }
    }

    public class Handler(IReadRepository<Survey> surveys) : IRequestHandler<Query, IEnumerable<Result>>
    {
        public async Task<IEnumerable<Result>> Handle(Query query, CancellationToken cancellationToken)
        {
            var surveyId = new SurveyId(query.SurveyId);
            var survey = await surveys.SingleOrDefaultAsync(new SurveyByIdSpec(surveyId), cancellationToken);
            if (survey == null)
            {
                PlatformException.ThrowNotFound(surveyId);
            }

            return survey.Questions.Select(q =>
            {
                var type = q.Type.Type;
                var result = new Result(q.QuestionId, q.Type.Title, q.Type.Mandatory, type);
                return result;
            });
        }
    }
}