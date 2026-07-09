namespace Taso.Domain.Common;

public class Result : Result<string>
{
    protected Result(bool isSuccess, IEnumerable<string>? errors) : base(isSuccess, string.Empty, errors)
    {
    }

    public static Result Success() => new(true, null);
    public static new Result Failure(IEnumerable<string> errors) => new(false, errors);
    public static new Result Failure(string error) => new(false, new[] { error });
}
