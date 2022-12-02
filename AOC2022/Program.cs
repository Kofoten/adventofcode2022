﻿using System.Diagnostics;

namespace AOC2022
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            if (!Options.TryParse(args, out var options, out var reason))
            {
                Console.WriteLine($"ERROR: {reason}");
                return 1;
            }

            if (!options.InputFile.Exists)
            {
                Console.WriteLine($"FATAL: Could not find input file: {options.InputFile.FullName}");
                return 2;
            }

            IChallenge challenge;
            try
            {
                challenge = ChallengeProvider.GetChallenge(options.Challenge);
            }
            catch (ChallengeNotFoundException e)
            {
                Console.WriteLine($"FATAL: {e.Message}");
                return 3;
            }

            using var stream = options.InputFile.OpenRead();

            int answer;
            try
            {
                answer = await challenge.PerformChallenge(stream, options.Part);
            }
            catch (PartDoesNotExistException e)
            {
                Console.WriteLine($"FATAL: {e.Message}");
                return 4;
            }
            catch (PartNotImplementedException e)
            {
                Console.WriteLine($"FATAL: {e.Message}");
                return 5;
            }
            catch (Exception e)
            {
                Console.WriteLine($"FATAL: {e.Message}");
                Console.WriteLine(e.ToString());
                return 42;
            }

            Console.WriteLine(answer);
            return 0;
        }
    }
}