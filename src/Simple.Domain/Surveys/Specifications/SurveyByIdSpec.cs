namespace Simple.Domain.Surveys.Specifications;

public class SurveyByIdSpec : SingleResultSpecification<Survey>
{
    public SurveyByIdSpec(SurveyId surveyId)
    {
        Query
            .Where(x => x.SurveyId.Equals(surveyId))
            .Include(x=>x.Questions);
    }
}