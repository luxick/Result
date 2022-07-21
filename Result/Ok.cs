namespace luxick.Result;

/// <summary>
/// A positive result
/// </summary>
[Serializable]
public sealed class Ok : Result
{
    /// <inheritdoc />
    public override bool IsOk => true;

    /// <summary>
    /// New positive result
    /// </summary>
    public Ok() { }

    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="msg">Short message</param>
    public Ok(string msg) => Messages.Add(msg);

    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="msgs">Messages (the first will be the short message)</param>
    public Ok(IEnumerable<string> msgs) => Messages.AddRange(msgs);

    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="msgs">Messages (the first will be the short message)</param>
    public Ok(params string[] msgs) => Messages.AddRange(msgs);
}

/// <summary>
/// A positive result that contains a value
/// </summary>
/// <typeparam name="T">Type of the value in the result</typeparam>
[Serializable]
public sealed class Ok<T> : Result<T>
{
    /// <summary>
    /// The result value of the operation
    /// </summary>
    public override T Value { get; set; }

    /// <inheritdoc />
    public override bool IsOk => true;
    
    /// <summary>
    /// New positive result
    /// </summary>
    public Ok() => Value = default!;

    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="value">The result of an operation</param>
    public Ok(T value) => Value = value;
    
    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="value">The result of an operation</param>
    /// <param name="msg">Short message for the result</param>
    public Ok(T value, string msg)
    {
        Value = value;
        Messages.Add(msg);
    }
    
    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="value">The result of an operation</param>
    /// <param name="msgs">Extra information</param>
    public Ok(T value, IEnumerable<string> msgs)
    {
        Value = value;
        Messages.AddRange(msgs);
    }
    
    /// <summary>
    /// New positive result
    /// </summary>
    /// <param name="value">The result of an operation</param>
    /// <param name="msgs">Extra information</param>
    public Ok(T value, params string[] msgs)
    {
        Value = value;
        Messages.AddRange(msgs);
    }
}