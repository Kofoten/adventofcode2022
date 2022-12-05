namespace AOC2022.Data;

public class Result
{
    public int Code { get; private init; }
    public string? Answer { get; private init; }
    public string? Message { get; private init; }

    public Result(int code, string? answer, string? message)
    {
        this.Code = code;
        this.Answer = answer;
        this.Message = message;
    }

    public bool IsSuccess([NotNullWhen(true)] out string? answer, [NotNullWhen(false)] out string? message)
    {
        if (Code == 0 && Answer is not null)
        {
            answer = Answer;
            message = null;
            return true;
        }

        answer = null;
        message = Message ?? "Unknown Error";
        return false;
    }

    public static Result Success(string answer) =>  new(0, answer, null);

    public static Result Error(int code, string message) => new(code, null, message);
}
