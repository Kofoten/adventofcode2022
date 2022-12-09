namespace AOC2022.Challenges.Challenge09;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool CanNormalize => X < -1 || X > 1 || Y < -1 || Y > 1;

    public Point Normalize()
    {
        var x = 0;
        var y = 0;

        if (X != 0)
        {
            x = X / Math.Abs(X);
        }

        if (Y != 0)
        {
            y = Y / Math.Abs(Y);
        }

        return new Point(x, y);
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }

    public static Point Zero => new(0, 0);

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

}
