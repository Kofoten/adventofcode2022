using AOC2022.Challange;
using AOC2022.Exceptions;

namespace AOC2022.Extensions;

public static class Extensions
{
    public static async Task<int> PerformChallange(this IChallange challange, Stream input, int part) => part switch
    {
        1 => await challange.Part1(input),
        2 => await challange.Part2(input),
        _ => throw new PartDoesNotExistException(part),
    };
}
