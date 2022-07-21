using System.Text.Json;
using System.Text.Json.Serialization;

namespace luxick.Result;

/// <summary>
/// Custom JSON Converter for deserializing the correct Type of Result based on the <see cref="Result.IsOk"/> discriminator.
/// </summary>
public class ResultConverter : JsonConverter<Result>
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert) => typeof(Result).IsAssignableFrom(typeToConvert);

    /// <inheritdoc />
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
            if (readerClone.GetString() != nameof(Result.IsOk)) continue;

            readerClone.Read();
            return readerClone.GetBoolean() switch
            {
                true => JsonSerializer.Deserialize<Ok>(ref reader),
                false => JsonSerializer.Deserialize<Error>(ref reader)
            };
        }

        throw new JsonException();
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}

/// <summary>
/// Custom JSON Converter for deserializing the correct Type of Result based on the <see cref="Result.IsOk"/> discriminator.
/// </summary>
/// <typeparam name="T">Type of the value within the result</typeparam>
public class GenericResultConverter<T> : JsonConverter<Result>
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert) => typeof(Result).IsAssignableFrom(typeToConvert);

    /// <inheritdoc />
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
            if (readerClone.GetString() != nameof(Result.IsOk)) continue;

            readerClone.Read();
            return readerClone.GetBoolean() switch
            {
                true => JsonSerializer.Deserialize<Ok<T>>(ref reader),
                false => JsonSerializer.Deserialize<Error<T>>(ref reader)
            };
        }

        throw new JsonException();
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}