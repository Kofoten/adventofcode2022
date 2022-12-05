namespace AOC2022;

public class Runtime
{
    public readonly RuntimeOptions options;

    public Runtime (RuntimeOptions options)
    {
        this.options = options;
    }

    public async Task<Result> Run()
    {
        if (!options.InputFile.Exists)
        {
            return Result.Error(2, $"Could not find input file: {options.InputFile.FullName}");
        }

        IChallenge challenge;
        try
        {
            challenge = ChallengeProvider.GetChallenge(options.Challenge);
        }
        catch (ChallengeNotFoundException e)
        {
            return Result.Error(3, e.Message);
        }

        using var stream = options.InputFile.OpenRead();
        using var reader = new InputReader(stream);

        string answer;
        try
        {
            answer = await challenge.PerformChallenge(reader, options.Part);
        }
        catch (PartDoesNotExistException e)
        {
            return Result.Error(4, e.Message);
        }
        catch (PartNotImplementedException e)
        {
            return Result.Error(5, e.Message);
        }

        return Result.Success(answer);
    }
}
