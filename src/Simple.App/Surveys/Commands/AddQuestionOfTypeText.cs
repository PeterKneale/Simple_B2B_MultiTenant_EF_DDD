﻿using Simple.Domain.Surveys;
using Simple.Domain.Surveys.Specifications;

namespace Simple.App.Surveys.Commands;

public static class AddQuestionOfTypeText
{
    public record Command(Guid SurveyId, string Title, bool Mandatory, int MaxLength) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.SurveyId).NotEmpty();
            RuleFor(m => m.Title).NotEmpty();
        }
    }

    public class Handler(IRepository<Survey> surveys) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var surveyId = new SurveyId(command.SurveyId);
            var survey = await surveys.SingleOrDefaultAsync(new SurveyByIdSpec(surveyId), cancellationToken);
            if (survey == null)
            {
                PlatformException.ThrowNotFound(surveyId);
            }

            survey.AddTextQuestion(command.Title, command.Mandatory, command.MaxLength);
            await surveys.UpdateAsync(survey, cancellationToken);
        }
    }
}