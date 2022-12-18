namespace AOC2022.Challenges.Challenge18;

public record Point(long X, long Y, long Z)
{
    public static Point operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    public static Point operator +(Point left, Point right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public Point Max(Point other)
    {
        var x = X < other.X ? other.X : X;
        var y = Y < other.Y ? other.Y : Y;
        var z = Z < other.Z ? other.Z : Z;
        return new Point(x, y, z);
    }

    public Point Min(Point other)
    {
        var x = X < other.X ? X : other.X;
        var y = Y < other.Y ? Y : other.Y;
        var z = Z < other.Z ? Z : other.Z;
        return new Point(x, y, z);
    }

    public IEnumerable<long> Values()
    {
        yield return X;
        yield return Y;
        yield return Z;
    }

    public static Point Parse(string input)
    {
        var numbers = input
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        return new Point(numbers[0], numbers[1], numbers[2]);
    }
}
