using System.Diagnostics.CodeAnalysis;
using Simple.Domain.Surveys;
using Simple.Domain.Tenants;
using Simple.Domain.Users;

namespace Simple.App;

public class PlatformException : Exception
{
    private PlatformException(string message) : base(message)
    {
    }

    [DoesNotReturn]
    public static void ThrowNotFound(UserId userId) => throw new PlatformException($"User not found: {userId}");

    [DoesNotReturn]
    public static void ThrowNotFound(EmailAddress email) => throw new PlatformException($"User not found: {email}");

    [DoesNotReturn]
    public static void ThrowNotFound(TenantId tenantId) => throw new PlatformException($"Tenant not found: {tenantId}");

    [DoesNotReturn]
    public static void ThrowNotFound(TenantName name) => throw new PlatformException($"Tenant not found: {name}");

    [DoesNotReturn]
    public static void ThrowAlreadyExists(TenantId tenantId) => throw new PlatformException($"Tenant already exists: {tenantId}");

    [DoesNotReturn]
    public static void ThrowAlreadyExists(TenantName name) => throw new PlatformException($"Tenant already exists: {name}");

    [DoesNotReturn]
    public static void ThrowAlreadyExists(UserId userId) => throw new PlatformException($"User already exists: {userId}");

    [DoesNotReturn]
    public static void ThrowNotFound(SurveyId surveyId) => throw new PlatformException($"Survey not found: {surveyId}");

    [DoesNotReturn]
    public static void ThrowAlreadyExists(SurveyId surveyId)=> throw new PlatformException($"Survey already exists: {surveyId}");

}