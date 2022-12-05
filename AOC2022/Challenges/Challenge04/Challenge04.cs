namespace AOC2022.Challenges.Challenge04;

public class Challenge04 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var result = 0;

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var parts = line.Split(',');
            var range1 = SectionRange.Parse(parts[0]);
            var range2 = SectionRange.Parse(parts[1]);

            if (range1.Contains(range2))
            {
                result++;
            }
            else if (range2.Contains(range1))
            {
                result++;
            }
        }

        return result.ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var result = 0;

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var parts = line.Split(',');
            var range1 = SectionRange.Parse(parts[0]);
            var range2 = SectionRange.Parse(parts[1]);

            if (range1.Intersects(range2))
            {
                result++;
            }
        }

        return result.ToString();
    }
}
