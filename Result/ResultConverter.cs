using System.Text.Json;
using System.Text.Json.Serialization;

namespace EAS.Result;

public class ResultConverter : JsonConverter<Result>
{
    public override bool CanConvert(Type typeToConvert) => typeof(Result).IsAssignableFrom(typeToConvert);
    
    public override Result? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readerClone = reader;
        if (readerClone.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        while (readerClone.Read())
        {
            if (readerClone.TokenType != JsonTokenType.PropertyName) continue;
            if (readerClone.GetString() != "Success") continue;
            
            readerClone.Read();
            return readerClone.GetBoolean() switch
            {
                true => JsonSerializer.Deserialize<Ok>(ref reader),
                false => JsonSerializer.Deserialize<Error>(ref reader)
            };
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options) => throw new NotSupportedException();
}

public class GenericResultConverter<T> : JsonConverter<Result>
{
    public override bool CanConvert(Type typeToConvert) => typeof(Result).IsAssignableFrom(typeToConvert);
    
    public override Result? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readerClone = reader;
        if (readerClone.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        while (readerClone.Read())
        {
            if (readerClone.TokenType != JsonTokenType.PropertyName) continue;
            if (readerClone.GetString() != "Success") continue;
            
            readerClone.Read();
            return readerClone.GetBoolean() switch
            {
                true => JsonSerializer.Deserialize<Ok<T>>(ref reader),
                false => JsonSerializer.Deserialize<Error<T>>(ref reader)
            };
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options) => throw new NotSupportedException();
}