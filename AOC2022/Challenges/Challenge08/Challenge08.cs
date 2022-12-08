namespace AOC2022.Challenges.Challenge08;

public class Challenge08 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var trees = new List<List<int>>();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            trees.Add(line.Select(x => (int)char.GetNumericValue(x)).ToList());
        }

        var currentHighsTop = trees.First();
        var currentHighsBot = trees.Last();
        var result = currentHighsTop.Count + currentHighsBot.Count;

        for (int i = 1; i < trees.Count; i++)
        {
            result += 2;

            var row = trees[i];
            var currentHighLeft = row.First();
            var currentHighRight = row.Last();

        }

    }

    public async Task<string> Part2(InputReader reader)
    {
        throw new PartNotImplementedException(2);
    }
}
