using System.Text.Json;

namespace luxick.Result;

[Serializable]
public abstract class Result
{
    private static JsonSerializerOptions _deserializeSettings = new()
    {
        Converters = { new ResultConverter() }
    };
    
    public abstract bool Success { get; }

    public abstract List<string> Messages { get; set; }

    public abstract string GetMessage();

    public abstract string GetFullMessage();

    public static Result FromJson(string json) => JsonSerializer.Deserialize<Result>(json, _deserializeSettings)!;
}

[Serializable]
public abstract class Result<T> : Result
{
    private static JsonSerializerOptions _deserializeSettings = new()
    {
        Converters = { new GenericResultConverter<T>() }
    };
    
    public abstract T Value { get; set; }
    
    public new static Result FromJson(string json) => JsonSerializer.Deserialize<Result<T>>(json, _deserializeSettings)!;

}