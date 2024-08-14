using System.Text;

namespace HealthChecks.Domain;

public class Result
{
    public bool Success { get; } = false;
    public Exception? Error { get; } = default;
    public string BusinessError { get; } = string.Empty;
    public object? ReturnValue { get; } = default;

    private readonly List<Result> results = [];

    public Result()
    {
        Success = true;
    }

    public Result(object returnValue)
    {
        Success = true;
        ReturnValue = returnValue;
    }

    public Result(List<Result> results)
    {
        this.results = results;
        Success = results.Any(p => !p.Success);
    }

    public Result(Exception e)
    {
        Success = false;
        Error = e;
    }

    public Result(string businessError)
    {
        Success = false;
        BusinessError = businessError;
    }

    public override string ToString()
    {
        var msg = new StringBuilder(BusinessError);

        if (Error != null)
        {
            msg.Append("; ");
            msg.Append(Error.ToString());
        }

        if (results.Count > 0)
        {
            msg.AppendJoin(";", results);            
        }

        return msg.ToString();
    }
}
