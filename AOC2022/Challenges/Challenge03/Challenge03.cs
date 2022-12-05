namespace AOC2022.Challenges.Challenge03;

public class Challenge03 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var result = 0;

        await foreach(var line in reader.ReadAllLinesAsync())
        {
            var compartmentIndex = line.Length / 2;
            var compartment1 = line[0..compartmentIndex];
            var compartment2 = line[compartmentIndex..line.Length];
            var sharedItem = compartment1.First(compartment2.Contains);

            result += GetPriority(sharedItem);
        }

        return result.ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var result = 0;
        var init = await reader.ReadLineAsync();
        var common = init.ToHashSet();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            if (common.Count == 1)
            {
                result += GetPriority(common.First());
                common = line.ToHashSet();
            }
            else
            {
                common.IntersectWith(line);
            }
        }

        result += GetPriority(common.First());
        return result.ToString();
    }

    private static int GetPriority(char c)
    {
        var priority = (int)c;
        if (priority > 90)
        {
            return priority - 96;
        }

        return priority - 38;
    }
}
