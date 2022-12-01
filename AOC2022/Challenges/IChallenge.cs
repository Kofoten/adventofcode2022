namespace AOC2022.Challenges
{
    public interface IChallenge
    {
        Task<int> Part1(Stream input);
        Task<int> Part2(Stream input);
    }
}
