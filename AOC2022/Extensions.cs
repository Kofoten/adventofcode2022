namespace AOC2022.Extensions;

public static class Extensions
{
    public static async Task<int> PerformChallenge(this IChallenge challenge, Stream input, int part) => part switch
    {
        1 => await challenge.Part1(input),
        2 => await challenge.Part2(input),
        _ => throw new PartDoesNotExistException(part),
    };
}
