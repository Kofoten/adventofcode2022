namespace AOC2022.Challenges;

public interface IChallenge
{
    Task<int> Part1(InputReader reader);
    Task<int> Part2(InputReader reader);
}
