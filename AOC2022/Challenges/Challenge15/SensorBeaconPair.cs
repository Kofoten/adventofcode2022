namespace AOC2022.Challenges.Challenge15;

public record SensorBeaconPair(Point Sensor, Point Beacon)
{
    public bool IsWithinRange(Point position)
    {
        var sbDiff = Sensor - Beacon;
        var spDiff = Sensor - position;
        var sbDist = Math.Abs(sbDiff.X) + Math.Abs(sbDiff.Y);
        var spDist = Math.Abs(spDiff.X) + Math.Abs(spDiff.Y);
        return spDist <= sbDist;
    }
}
