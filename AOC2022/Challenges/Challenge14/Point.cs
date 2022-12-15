namespace AOC2022.Challenges.Challenge14;

public record Point(int X, int Y)
{
    public Point Normalize() => new(NormalizeInteger(X), NormalizeInteger(Y));

    public static Point Zero => new(0, 0);

    public static Point Parse(string input)
    {
        var parts = input.Split(',');
        var x = int.Parse(parts[0]);
        var y = int.Parse(parts[1]);
        return new Point(x, y);
    }

    public static Point operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y);

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y);

    private static int NormalizeInteger(int value)
    {
        if (value == 0)
        {
            return 0;
        }
        else if (value < 0)
        {
            return value / value * -1;
        }
        else
        {
            return value / value;
        }
    }
}
