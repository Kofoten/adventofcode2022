using System.Text;

namespace AOC2022.Challenges.Challenge20;

public class ReadNumbersResult
{
    public Number First { get; init; }
    public long Count { get; init; }

    public ReadNumbersResult(Number first, long count)
    {
        First = first;
        Count = count;
    }
}
