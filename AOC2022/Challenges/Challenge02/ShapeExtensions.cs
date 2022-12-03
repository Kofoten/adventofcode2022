namespace AOC2022.Challenges.Challenge02;

public static class ShapeExtensions
{
    public static Shape LosesTo(this Shape shape) => shape switch
    {
        Shape.Rock => Shape.Paper,
        Shape.Paper => Shape.Scissors,
        Shape.Scissors => Shape.Rock,
        _ => throw new ArgumentException("Invalid shape", nameof(shape)),
    };

    public static Shape WinsOver(this Shape shape) => shape switch
    {
        Shape.Rock => Shape.Scissors,
        Shape.Paper => Shape.Rock,
        Shape.Scissors => Shape.Paper,
        _ => throw new ArgumentException("Invalid shape", nameof(shape)),
    };

    public static int GetScoreAgainst(this Shape me, Shape opponent)
    {
        if (me == opponent)
        {
            return 3 + (int)me;
        }

        return (int)me + me switch
        {
            Shape.Rock => opponent == Shape.Scissors ? 6 : 0,
            Shape.Paper => opponent == Shape.Rock ? 6 : 0,
            Shape.Scissors => opponent == Shape.Paper ? 6 : 0,
            _ => throw new ArgumentException("Invalid shape", nameof(me)),
        };
    }
}
