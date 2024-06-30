namespace Simple.IntegrationTests.Surveys;

public class SurveyWorkflowTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task SurveyTest()
    {
        var tenant = Fake.Tenant();
        var user = Fake.User();

        await Execute(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));

        var surveyId = Guid.NewGuid();
        var surveyName = "Customer Feedback";
        await Execute(new CreateSurvey.Command(surveyId, surveyName), tenant, user);

        var q1Text = "x";
        var q1Mandatory = true;
        await Execute(new AddQuestion.Command(surveyId, q1Text, q1Mandatory), tenant, user);

        var q2Text = "y";
        var q2Mandatory = true;
        await Execute(new AddQuestion.Command(surveyId, q2Text, q2Mandatory), tenant, user);

        var getByIdResult = await Execute(new GetSurveyById.Query(surveyId), tenant, user);
        getByIdResult.SurveyId.Should().Be(surveyId);
        getByIdResult.SurveyName.Should().Be(surveyName);
        getByIdResult.TotalQuestions.Should().Be(2);
        
        var listResult = await Execute(new ListQuestions.Query(surveyId), tenant, user);
        var results = listResult.ToList();
        results.Should().HaveCount(2);
        var q1 = results.Single(x => x.Title == q1Text);
        var q2 = results.Single(x => x.Title == q2Text);
        q1.Mandatory.Should().Be(q1Mandatory);
        q2.Mandatory.Should().Be(q2Mandatory);

        await Execute(new RemoveQuestion.Command(surveyId, q1.QuestionId), tenant, user);
        await Execute(new RemoveQuestion.Command(surveyId, q2.QuestionId), tenant, user);
        var listResult2 = await Execute(new ListQuestions.Query(surveyId), tenant, user);
        listResult2.Should().BeEmpty();
    }
}