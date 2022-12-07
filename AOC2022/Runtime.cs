﻿using System.Diagnostics;

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
        var stopwatch = new Stopwatch();

        string answer;
        try
        {
            stopwatch.Start();
            answer = await challenge.PerformChallenge(reader, options.Part);
            stopwatch.Stop();
        }
        catch (PartDoesNotExistException e)
        {
            stopwatch.Stop();
            return Result.Error(4, e.Message, stopwatch.Elapsed);
        }
        catch (PartNotImplementedException e)
        {
            stopwatch.Stop();
            return Result.Error(5, e.Message, stopwatch.Elapsed);
        }

        return Result.Success(answer, stopwatch.Elapsed);
    }
}
