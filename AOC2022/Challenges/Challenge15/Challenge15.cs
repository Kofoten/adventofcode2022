using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge15;

public class Challenge15 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var y = 2000000;
        var xInScanedLine = new HashSet<int>();
        var xOfOthersInY = new HashSet<int>();

        await foreach (var (sensor, beacon) in ReadSensorsAndBeacons(reader))
        {
            if (sensor.Y == y)
            {
                xOfOthersInY.Add(sensor.X);
            }

            if (beacon.Y == y)
            {
                xOfOthersInY.Add(beacon.X);
            }

            var diff = sensor - beacon;
            var manhattan = Math.Abs(diff.X) + Math.Abs(diff.Y);
            var yDiff = Math.Abs(sensor.Y - y);
            var remainder = manhattan - yDiff;

            if (remainder < 0)
            {
                continue;
            }

            var start = sensor.X - remainder;
            var stop = sensor.X + remainder + 1;
            for (int i = start; i < stop; i++)
            {
                xInScanedLine.Add(i);
            }
        }

        xInScanedLine.ExceptWith(xOfOthersInY);
        return xInScanedLine.Count.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var size = 4000000;
        var sbps = await ReadSensorsAndBeacons(reader).ToListAsync();
        var unknowns = new HashSet<Point>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var position = new Point(i, j);
                
                if (sbps.Any(sbp => sbp.IsWithinRange(position)))
                {
                    continue;
                }

                unknowns.Add(position);
            }
        }

        var result = unknowns.First();
        return (result.X * 4000000 + result.Y).ToString();
    }

    private static async IAsyncEnumerable<SensorBeaconPair> ReadSensorsAndBeacons(IInputReader reader)
    {
        var regex = new Regex(@"^Sensor at x=(-?[0-9]+), y=(-?[0-9]+): closest beacon is at x=(-?[0-9]+), y=(-?[0-9]+)$");

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var match = regex.Match(line);
            if (!match.Success)
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var sensorX = int.Parse(match.Groups[1].Value);
            var sensorY = int.Parse(match.Groups[2].Value);
            var beaconX = int.Parse(match.Groups[3].Value);
            var beaconY = int.Parse(match.Groups[4].Value);
            
            var sensor = new Point(sensorX, sensorY);
            var beacon = new Point(beaconX, beaconY);

            yield return new SensorBeaconPair(sensor, beacon);
        }
    }

    //[GeneratedRegex(@"^Sensor at x=(-?[0-9]+), y=(-?[0-9]+): closest beacon is at x=(-?[0-9]+), y=(-?[0-9]+)$")]
    //private static partial Regex SensorBeaconRegex();
}
