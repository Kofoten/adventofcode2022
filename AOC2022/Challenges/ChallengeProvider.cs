namespace AOC2022.Challenges;

public static class ChallengeProvider
{
    public static IChallenge GetChallenge(int id) => id switch
    {
        01 => new Challenge01.Challenge01(),
        02 => new Challenge02.Challenge02(),
        03 => new Challenge03.Challenge03(),
        04 => new Challenge04.Challenge04(),
        05 => new Challenge05.Challenge05(),
        06 => new Challenge06.Challenge06(),
        07 => new Challenge07.Challenge07(),
        08 => new Challenge08.Challenge08(),
        09 => new Challenge09.Challenge09(),
        10 => new Challenge10.Challenge10(),
        11 => new Challenge11.Challenge11(),
        12 => new Challenge12.Challenge12(),
        13 => new Challenge13.Challenge13(),
        14 => new Challenge14.Challenge14(),
        15 => new Challenge15.Challenge15(),
        16 => new Challenge16.Challenge16(),
        17 => new Challenge17.Challenge17(),
        18 => new Challenge18.Challenge18(),
        19 => new Challenge19.Challenge19(),
        20 => new Challenge20.Challenge20(),
        21 => new Challenge21.Challenge21(),
        22 => new Challenge22.Challenge22(),
        23 => new Challenge23.Challenge23(),
        24 => new Challenge24.Challenge24(),
        25 => new Challenge25.Challenge25(),
        _ => throw new ChallengeNotFoundException(id),
    };
}
