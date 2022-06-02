namespace luxick.Result;

[Serializable]
public sealed class Error : Result
{
    public override bool Success => false;
    
    public override List<string> Messages { get; set; } = new();
    
    public override string GetMessage() => Messages.FirstOrDefault() ?? "";
    
    public override string GetFullMessage() => string.Join("\n", Messages);
    
    public Error() { }

    public Error(Exception ex) => Messages.AddRange(Helpers.ParseException(ex));
    
    public Error(string msg, Exception ex)
    {
        Messages.Add(msg);
        Messages.AddRange(Helpers.ParseException(ex));
    }

    public Error(string msg) => Messages.Add(msg);

    public Error(IEnumerable<string> msgs) => Messages.AddRange(msgs);

    public Error(params string[] msgs) => Messages.AddRange(msgs);
}

[Serializable]
public sealed class Error<T> : Result<T>
{
    public override T Value { get; set; } = default!;

    public override bool Success => false;
    public override List<string> Messages { get; set; } = new();

    public override string GetMessage() => Messages.FirstOrDefault() ?? "";
    
    public override string GetFullMessage() => string.Join("\n", Messages);    
    public Error() { }
    
    public Error(Exception ex) => Messages.AddRange(Helpers.ParseException(ex));
    
    public Error(string msg, Exception ex)
    {
        Messages.Add(msg);
        Messages.AddRange(Helpers.ParseException(ex));
    }

    public Error(string msg) => Messages.Add(msg);

    public Error(IEnumerable<string> msgs) => Messages.AddRange(msgs);

    public Error(params string[] msgs) => Messages.AddRange(msgs);
}