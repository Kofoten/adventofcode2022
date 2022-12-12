namespace AOC2022.Challenges;

public interface IChallenge
{
    Task<string> Part1(IInputReader reader);
    Task<string> Part2(IInputReader reader);
}
