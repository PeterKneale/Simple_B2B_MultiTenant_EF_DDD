using Simple.App.Contracts;
using Simple.Domain.Surveys;
using Simple.Domain.Surveys.Specifications;

namespace Simple.App.Surveys.Commands;

public static class CreateSurvey
{
    public record Command(Guid SurveyId, string Name) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.SurveyId).NotEmpty();
            RuleFor(m => m.Name).NotEmpty();
        }
    }

    public class Handler(IRepository<Survey> surveys, IExecutionContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var surveyId = new SurveyId(command.SurveyId);
            var survey = await surveys.SingleOrDefaultAsync(new SurveyByIdSpec(surveyId), cancellationToken);
            if (survey != null)
            {
                PlatformException.ThrowAlreadyExists(surveyId);
            }

            var name = SurveyName.Create(command.Name);
            var tenantId = context.CurrentTenantId;
            survey = new Survey(tenantId, surveyId, name);
            await surveys.AddAsync(survey, cancellationToken);
        }
    }
}