namespace AOC2022.Challenges.Challenge17;

public class SpikeRock : IRock
{
    public Point Position { get; set; }
    public long LeftEdge => Position.X;
    public long RightEdge => Position.X;
    public long TopEdge => Position.Y + 3;
    public long BottomEdge => Position.Y;

    public SpikeRock(Point position)
    {
        Position = position;
    }

    public IEnumerable<Point> GetCurrentPoints()
    {
        for (long i = Position.Y; i <= TopEdge; i++)
        {
            yield return new Point(Position.X, i);
        }
    }
}
