namespace EAS.Result;

[Serializable]
public sealed class Ok : Result
{
    public override bool Success => true;

    public override List<string> Messages { get; set; } = new();

    public override string GetMessage() => Messages.FirstOrDefault() ?? "";
    
    public override string GetFullMessage() => string.Join("\n", Messages);

    public Ok() { }

    public Ok(string msg) => Messages.Add(msg);

    public Ok(IEnumerable<string> msgs) => Messages.AddRange(msgs);

    public Ok(params string[] msgs) => Messages.AddRange(msgs);
}

[Serializable]
public sealed class Ok<T> : Result<T>
{
    public override T Value { get; set; }
    
    public override bool Success => true;

    public override List<string> Messages { get; set; } = new();
    
    public override string GetMessage() => Messages.FirstOrDefault() ?? "";
    
    public override string GetFullMessage() => string.Join("\n", Messages);
    
    public Ok() => Value = default!;

    public Ok(T value) => Value = value;
    
    public Ok(T value, string msg)
    {
        Value = value;
        Messages.Add(msg);
    }
    
    public Ok(T value, IEnumerable<string> msgs)
    {
        Value = value;
        Messages.AddRange(msgs);
    }
    
    public Ok(T value, params string[] msgs)
    {
        Value = value;
        Messages.AddRange(msgs);
    }
}