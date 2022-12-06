using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge05;

public partial record Instruction(int Count, int From, int To)
{
    private static readonly Regex parseRegex = GeneratedParseRegex();

    public static Instruction Parse(string value)
    {
        var match = parseRegex.Match(value);
        if (match.Success)
        {
            var count = int.Parse(match.Groups[1].Value);
            var from = int.Parse(match.Groups[2].Value) - 1;
            var to = int.Parse(match.Groups[3].Value) - 1;
            return new Instruction(count, from, to);
        }

        throw new ArgumentException("Input was not in the correct format", nameof(value));
    }

    [GeneratedRegex("^move ([0-9]+) from ([0-9]+) to ([0-9]+)$")]
    private static partial Regex GeneratedParseRegex();
}
