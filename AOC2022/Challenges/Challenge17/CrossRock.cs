namespace AOC2022.Challenges.Challenge17;

public class CrossRock : IRock
{
    public Point Position { get; set; }
    public long LeftEdge => Position.X;
    public long RightEdge => Position.X + 2;
    public long TopEdge => Position.Y + 2;
    public long BottomEdge => Position.Y;

    public CrossRock(Point position)
    {
        Position = position;
    }

    public IEnumerable<Point> GetCurrentPoints()
    {
        yield return new Point(Position.X, Position.Y + 1);
        yield return new Point(Position.X + 1, Position.Y);
        yield return new Point(Position.X + 1, Position.Y + 1);
        yield return new Point(Position.X + 1, Position.Y + 2);
        yield return new Point(Position.X + 2, Position.Y + 1);
    }
}
