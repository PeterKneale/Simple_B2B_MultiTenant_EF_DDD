using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Simple.Domain.Surveys;
using Simple.Domain.Surveys.Questions;

namespace Simple.Infra.Database.Converters;

public class QuestionInfoConverter() : ValueConverter<QuestionType, string>(
    x => JsonConvert.SerializeObject(x),
    x => JsonConvert.DeserializeObject<QuestionType>(x, new QuestionInfoDeserializer())!
);

public class QuestionInfoDeserializer : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(QuestionType).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var type = (string)jo["Type"]!;
        return type switch
        {
            QuestionTypeNames.Text => jo.ToObject<TextQuestionType>()!,
            QuestionTypeNames.List => jo.ToObject<ListQuestionType>()!,
            _ => throw new NotSupportedException($"Type is not supported '{type}'")
        };
    }

    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}