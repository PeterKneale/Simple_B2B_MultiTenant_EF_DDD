using FluentMigrator.Runner;
using Polly;

namespace Simple.Infra.Database.Migrations;

public class MigrationRunner(IMigrationRunner runner, ILogger<IMigrationRunner> logs)
{
    public void Run(bool reset = false)
    {
        var RetryInterval = 1; // seconds
        var RetryAttempts = 10;

        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetry(
                RetryAttempts,
                retryAttempt => TimeSpan.FromSeconds(RetryInterval),
                (exception, timeSpan, attempt, context) =>
                    logs.LogWarning($"Attempt {attempt} of {RetryAttempts} failed with exception {exception.Message}. Delaying {timeSpan.TotalMilliseconds}ms"));

        policy.Execute(() =>
        {
            logs.LogInformation("Migrating database schema");
            if (reset)
            {
                runner.MigrateDown(0);
            }

            runner.MigrateUp();

            logs.LogInformation("Migrated database schema");
        });
    }
}