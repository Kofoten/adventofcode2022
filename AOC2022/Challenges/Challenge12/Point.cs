namespace AOC2022.Challenges.Challenge12;

public record Point(int X, int Y)
{
    public IEnumerable<Point> GetNeighbours(Point gridSize)
    {
        if (X - 1 >= 0)
        {
            yield return new Point(X - 1, Y);
        }

        if (X + 1 < gridSize.X)
        {
            yield return new Point(X + 1, Y);
        }

        if (Y - 1 >= 0)
        {
            yield return new Point(X, Y - 1);
        }

        if (Y + 1 < gridSize.Y)
        {
            yield return new Point(X, Y + 1);
        }
    }

    public static Point Zero => new(0, 0);
}
