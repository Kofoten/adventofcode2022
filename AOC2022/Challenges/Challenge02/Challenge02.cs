namespace AOC2022.Challenges.Challenge02;

public class Challenge02 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var result = 0;
        await foreach (var (param, value) in GetRoundResults(reader))
        {
            var opponent = ConvertFromChar(param);
            var me = value switch
            {
                'X' => Shape.Rock,
                'Y' => Shape.Paper,
                'Z' => Shape.Scissors,
                _ => throw new InvalidOperationException($"Input contains invalid data."),
            };

            result += me.GetScoreAgainst(opponent);
        }

        return result.ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var result = 0;
        await foreach (var (param, value) in GetRoundResults(reader))
        {
            var opponent = ConvertFromChar(param);
            var me = value switch
            {
                'X' => opponent.WinsOver(),
                'Y' => opponent,
                'Z' => opponent.LosesTo(),
                _ => throw new InvalidOperationException($"Input contains invalid data."),
            };

            result += me.GetScoreAgainst(opponent);
        }

        return result.ToString();
    }

    public static async IAsyncEnumerable<KeyValuePair<char, char>> GetRoundResults(InputReader reader)
    {
        await foreach (var line in reader.ReadAllLinesAsync())
        {
            if (string.IsNullOrEmpty(line) || line.Length != 3)
            {
                throw new InvalidDataException($"Input contains invalid data.");
            }

            yield return new KeyValuePair<char, char>(line.First(), line.Last());
        }
    }

    private static Shape ConvertFromChar(char ch) => ch switch
    {
        'A' => Shape.Rock,
        'B' => Shape.Paper,
        'C' => Shape.Scissors,
        _ => throw new ArgumentException($"Invalid value", nameof(ch)),
    };
}
