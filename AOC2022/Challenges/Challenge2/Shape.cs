namespace AOC2022.Challenges.Challenge2
{
    public enum Shape
    {
        Unknown     = 0x0,
        Rock        = 0x1,
        Paper       = 0x2,
        Scissors    = 0x3,
    }

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
    }
}
