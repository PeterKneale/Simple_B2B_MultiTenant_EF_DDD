﻿using Newtonsoft.Json;
using Simple.App.Contracts;

namespace Simple.Infra.IntegrationEvents;

public class IntegrationEvent
{
    public Guid Id { get; private init; }
    public string Type { get; private init; } = null!;
    public string Data { get; private init; } = null!;
    public DateTimeOffset CreatedAt { get; private init; }
    public DateTimeOffset? ProcessedAt { get; private set; }

    public void MarkProcessed()
    {
        ProcessedAt = SystemTime.UtcNow();
    }

    public static IntegrationEvent CreateFrom<T>(T integrationEvent) where T : IIntegrationEvent =>
        new()
        {
            Id = Guid.NewGuid(),
            Type = integrationEvent.GetType().AssemblyQualifiedName!,
            Data = JsonConvert.SerializeObject(integrationEvent),
            CreatedAt = DateTime.UtcNow
        };

    public static IIntegrationEvent ToIntegrationEvent(IntegrationEvent message)
    {
        var messageType = System.Type.GetType(message.Type);
        if (messageType == null) throw new Exception("Unable to find type: " + messageType);
        return JsonConvert.DeserializeObject(message.Data, messageType) as IIntegrationEvent ?? throw new InvalidOperationException();
    }
}