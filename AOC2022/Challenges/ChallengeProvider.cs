namespace AOC2022.Challenges;

public static class ChallengeProvider
{
    public static IChallenge GetChallenge(int id) => id switch
    {
        1 => new Challenge1.Challenge1(),
        2 => new Challenge2.Challenge2(),
        _ => throw new ChallengeNotFoundException(id),
    };
}
