namespace AOC2022.Challenges.Challenge17;

public class FlatRock : IRock
{
    public Point Position { get; set; }
    public long LeftEdge => Position.X;
    public long RightEdge => Position.X + 3;
    public long TopEdge => Position.Y;
    public long BottomEdge => Position.Y;

    public FlatRock(Point position)
    {
        Position = position;
    }

    public IEnumerable<Point> GetCurrentPoints()
    {
        for (long i = Position.X; i <= RightEdge; i++)
        {
            yield return new Point(i, Position.Y);
        }
    }
}
