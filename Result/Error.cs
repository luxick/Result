namespace luxick.Result;

/// <summary>
/// A negative result 
/// </summary>
[Serializable]
public sealed class Error : Result
{
    /// <inheritdoc />
    public override bool IsOk => false;
    
    /// <summary>
    /// New negative result
    /// </summary>
    public Error() { }

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="ex">An exception</param>
    public Error(Exception ex) => Messages.AddRange(Helpers.ParseException(ex));
    
    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msg">Custom messsage</param>
    /// <param name="ex">An exception</param>
    public Error(string msg, Exception ex)
    {
        Messages.Add(msg);
        Messages.AddRange(Helpers.ParseException(ex));
    }

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msg">Error message</param>
    public Error(string msg) => Messages.Add(msg);

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msgs">Error messages</param>
    public Error(IEnumerable<string> msgs) => Messages.AddRange(msgs);

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msgs">Error messages</param>
    public Error(params string[] msgs) => Messages.AddRange(msgs);
}

/// <summary>
/// A negative result
/// </summary>
/// <typeparam name="T">Type of the value the operation could have produced</typeparam>
[Serializable]
public sealed class Error<T> : Result<T>
{
    /// <summary>
    /// NEVER USE THIS. Negative result will not have a result value.
    /// </summary>
    public override T Value { get; set; } = default!;

    /// <inheritdoc />
    public override bool IsOk => false;
    
    /// <summary>
    /// New negative result
    /// </summary>
    public Error() { }

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="ex">An exception</param>
    public Error(Exception ex) => Messages.AddRange(Helpers.ParseException(ex));
    
    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msg">Custom messsage</param>
    /// <param name="ex">An exception</param>
    public Error(string msg, Exception ex)
    {
        Messages.Add(msg);
        Messages.AddRange(Helpers.ParseException(ex));
    }

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msg">Error message</param>
    public Error(string msg) => Messages.Add(msg);

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msgs">Error messages</param>
    public Error(IEnumerable<string> msgs) => Messages.AddRange(msgs);

    /// <summary>
    /// New negative result
    /// </summary>
    /// <param name="msgs">Error messages</param>
    public Error(params string[] msgs) => Messages.AddRange(msgs);
}