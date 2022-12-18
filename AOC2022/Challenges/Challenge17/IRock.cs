namespace AOC2022.Challenges.Challenge17;

public interface IRock
{
    Point Position { get; set; }
    long LeftEdge { get; }
    long RightEdge { get; }
    long TopEdge { get; }
    long BottomEdge { get; }

    IEnumerable<Point> GetCurrentPoints();
}
