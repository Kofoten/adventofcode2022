namespace AOC2022.Challenges.Challenge15;

public record SensorBeaconPair(Point Sensor, Point Beacon)
{
    public long Radius => ManhattanDistance(Beacon);

    public bool IsWithinRadius(Point position) => ManhattanDistance(position) <= Radius;

    public long ManhattanDistance(Point other)
    {
        var diff = Sensor - other;
        return Math.Abs(diff.X) + Math.Abs(diff.Y);
    }
}
