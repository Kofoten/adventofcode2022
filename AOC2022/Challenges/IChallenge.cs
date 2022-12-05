namespace AOC2022.Challenges;

public interface IChallenge
{
    Task<string> Part1(InputReader reader);
    Task<string> Part2(InputReader reader);
}
