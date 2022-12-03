namespace AOC2022.Data;

public class Result
{
    public int Code { get; private init; }
    public int? Answer { get; private init; }
    public string? Message { get; private init; }

    public Result(int code, int? answer, string? message)
    {
        this.Code = code;
        this.Answer = answer;
        this.Message = message;
    }

    public bool IsSuccess([NotNullWhen(true)] out int? answer, [NotNullWhen(false)] out string? message)
    {
        if (Code == 0 && Answer.HasValue)
        {
            answer = Answer.Value;
            message = null;
            return true;
        }

        answer = null;
        message = Message ?? "Unknown Error";
        return false;
    }

    public static Result Success(int answer) =>  new(0, answer, null);

    public static Result Error(int code, string message) => new(code, null, message);
}
