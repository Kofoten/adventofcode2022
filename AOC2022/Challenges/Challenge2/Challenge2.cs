namespace AOC2022.Challenges.Challenge2;

public class Challenge2 : IChallenge
{
    public async Task<int> Part1(Stream input)
    {
        var result = 0;
        await foreach (var (param, value) in GetRoundResults(input))
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

        return result;
    }

    public async Task<int> Part2(Stream input)
    {
        var result = 0;
        await foreach (var (param, value) in GetRoundResults(input))
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

        return result;
    }

    public static async IAsyncEnumerable<KeyValuePair<char, char>> GetRoundResults(Stream input)
    {
        using var reader = new StreamReader(input);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(line) || line.Length != 3)
            {
                throw new InvalidOperationException($"Input contains invalid data.");
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
