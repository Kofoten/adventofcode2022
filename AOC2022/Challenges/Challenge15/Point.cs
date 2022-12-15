namespace AOC2022.Challenges.Challenge15;

public record Point(int X, int Y)
{
    public static Point operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y);

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);
}
