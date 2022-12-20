using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge19;

public class Challenge19 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        throw new PartNotImplementedException(1);
    }

    public async Task<string> Part2(IInputReader reader)
    {
        throw new PartNotImplementedException(2);
    }

    private static async IAsyncEnumerable<object> ReadBlueprints(IInputReader reader)
    {
        var robotRegex = new Regex(@"^Each (ore|clay|obsidian|geode) robot costs (.+)$");
        var costRegex = new Regex(@"(?:([0-9]+) (ore|clay|obsidian))");
        var splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var nameEndIndex = line.IndexOf(':');
            var name = line[0..nameEndIndex];
            
            foreach (var recipeDescription in line[(nameEndIndex + 1)..line.Length].Split('.', splitOptions))
            {
                var robotMatch = robotRegex.Match(recipeDescription);
                if (!robotMatch.Success)
                {
                    throw new InvalidDataException("Input contains invalid data");
                }

                var costMatch = costRegex.Match(robotMatch.Groups[2].Value);
                if (!costMatch.Success)
                {
                    throw new InvalidDataException("Input contains invalid data");
                }

                yield return new object();
                var robotType = robotMatch.Groups[1].Value;
            }
        }
    }
}
