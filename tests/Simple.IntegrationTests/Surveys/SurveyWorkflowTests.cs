namespace Simple.IntegrationTests.Surveys;

public class SurveyWorkflowTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task SurveyTest()
    {
        var tenant = Fake.Tenant();
        var user = Fake.User();

        await Command(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));

        var surveyId = Guid.NewGuid();
        var surveyName = "Customer Feedback";
        await Command(new CreateSurvey.Command(surveyId, surveyName), tenant, user);

        var q1Text = "x";
        var q1Mandatory = true;
        var q1MaxLength = 100;
        await Command(new AddQuestionOfTypeText.Command(surveyId, q1Text, q1Mandatory, q1MaxLength), tenant, user);

        var q2Text = "y";
        var q2Mandatory = true;
        var q2Options = new List<string> { "a", "b", "c" };
        var s2Single = true;
        await Command(new AddQuestionOfTypeList.Command(surveyId, q2Text, q2Mandatory, q2Options, s2Single), tenant, user);

        var getByIdResult = await Query(new GetSurveyById.Query(surveyId), tenant, user);
        getByIdResult.SurveyId.Should().Be(surveyId);
        getByIdResult.SurveyName.Should().Be(surveyName);
        getByIdResult.TotalQuestions.Should().Be(2);

        var listResult = await Query(new ListQuestions.Query(surveyId), tenant, user);
        var results = listResult.ToList();
        results.Should().HaveCount(2);
        var q1 = results.Single(x => x.Title == q1Text);
        var q2 = results.Single(x => x.Title == q2Text);
        q1.Mandatory.Should().Be(q1Mandatory);
        q2.Mandatory.Should().Be(q2Mandatory);
        q1.Type.Should().Be("text");
        q2.Type.Should().Be("list");

        await Command(new RemoveQuestion.Command(surveyId, q1.QuestionId), tenant, user);
        await Command(new RemoveQuestion.Command(surveyId, q2.QuestionId), tenant, user);
        var listResult2 = await Query(new ListQuestions.Query(surveyId), tenant, user);
        listResult2.Should().BeEmpty();
    }
}