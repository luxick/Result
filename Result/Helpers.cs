namespace luxick.Result;

internal static class Helpers
{
    internal static IEnumerable<string> ParseException(Exception ex)
    {
        var result = new List<string>
        {
            ex.GetType().Name,
            ex.Message
        };
        
        if (ex.StackTrace != null)
            result.Add(ex.StackTrace);
        
        if (ex.InnerException != null)
            result.AddRange(ParseException(ex.InnerException));
        
        return result;
    }
}