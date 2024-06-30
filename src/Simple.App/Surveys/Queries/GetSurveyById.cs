using Simple.Domain.Surveys;
using Simple.Domain.Surveys.Specifications;

namespace Simple.App.Surveys.Queries;

public static class GetSurveyById
{
    public record Query(Guid SurveyId) : IRequest<Result>;

    public record Result(Guid SurveyId, string SurveyName, int TotalQuestions);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.SurveyId).NotEmpty();
        }
    }

    public class Handler(IReadRepository<Survey> surveys) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
        {
            var surveyId = new SurveyId(query.SurveyId);
            var survey = await surveys.SingleOrDefaultAsync(new SurveyByIdSpec(surveyId), cancellationToken);
            if (survey == null)
            {
                PlatformException.ThrowNotFound(surveyId);
            }

            return new Result(survey.SurveyId, survey.Name, survey.TotalQuestions);
        }
    }
}