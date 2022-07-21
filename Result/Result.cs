using System.Text.Json;

namespace luxick.Result;

/// <summary>
/// Base class for all result types 
/// </summary>
[Serializable]
public abstract class Result
{
    private static JsonSerializerOptions _deserializeSettings = new()
    {
        Converters = { new ResultConverter() }
    };
    
    /// <summary>
    /// Was the opration successful?
    /// </summary>
    public abstract bool IsOk { get; }

    /// <summary>
    /// List of Messages for the operartion
    /// </summary>
    public List<string> Messages { get; set; } = new();

    /// <summary>
    /// Gets the short message for the operation
    /// </summary>
    /// <returns>First element of <see cref="Messages"/></returns>
    public string GetMessage() => Messages.FirstOrDefault() ?? "";

    /// <summary>
    /// Gets the full message for the operation
    /// </summary>
    /// <returns>All elements of <see cref="Messages"/> combined</returns>
    public string GetFullMessage()  => string.Join("\n", Messages);

    /// <summary>
    /// Deserialize a JSON string to the appropriate Result subclass
    /// </summary>
    /// <param name="json">the JSON </param>
    /// <returns>A Result subclass</returns>
    public static Result FromJson(string json) => JsonSerializer.Deserialize<Result>(json, _deserializeSettings)!;
}

/// <summary>
/// Base class for all result types that contain a value
/// </summary>
/// <typeparam name="T">Type of the contained value</typeparam>
[Serializable]
public abstract class Result<T> : Result
{
    private static JsonSerializerOptions _deserializeSettings = new()
    {
        Converters = { new GenericResultConverter<T>() }
    };
    
    /// <summary>
    /// The value contained in this result
    /// </summary>
    public abstract T Value { get; set; }
    
    /// <summary>
    /// Deserialize a JSON string to the appropriate Result subclass
    /// </summary>
    /// <param name="json">the JSON </param>
    /// <returns>A Result subclass</returns>
    public new static Result<T> FromJson(string json) => JsonSerializer.Deserialize<Result<T>>(json, _deserializeSettings)!;

}