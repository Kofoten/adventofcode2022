namespace AOC2022.Challenges.Challenge17;

public class SquareRock : IRock
{
    public Point Position { get; set; }
    public long LeftEdge => Position.X;
    public long RightEdge => Position.X + 1;
    public long TopEdge => Position.Y + 1;
    public long BottomEdge => Position.Y;

    public SquareRock(Point position)
    {
        Position = position;
    }

    public IEnumerable<Point> GetCurrentPoints()
    {
        yield return new Point(LeftEdge, TopEdge);
        yield return new Point(LeftEdge, BottomEdge);
        yield return new Point(RightEdge, TopEdge);
        yield return new Point(RightEdge, BottomEdge);
    }
}
