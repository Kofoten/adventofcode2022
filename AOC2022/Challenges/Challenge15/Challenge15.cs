using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge15;

public class Challenge15 : IChallenge
{
    private readonly Challenge15Options options;

    public Challenge15(Challenge15Options options)
    {
        this.options = options;
    }

    public async Task<string> Part1(IInputReader reader)
    {
        var xInScanedLine = new HashSet<long>();
        var xOfOthersInY = new HashSet<long>();

        await foreach (var (sensor, beacon) in ReadSensorsAndBeacons(reader))
        {
            if (sensor.Y == options.Row)
            {
                xOfOthersInY.Add(sensor.X);
            }

            if (beacon.Y == options.Row)
            {
                xOfOthersInY.Add(beacon.X);
            }

            var diff = sensor - beacon;
            var manhattan = Math.Abs(diff.X) + Math.Abs(diff.Y);
            var yDiff = Math.Abs(sensor.Y - options.Row);
            var remainder = manhattan - yDiff;

            if (remainder < 0)
            {
                continue;
            }

            var start = sensor.X - remainder;
            var stop = sensor.X + remainder + 1;
            for (long i = start; i < stop; i++)
            {
                xInScanedLine.Add(i);
            }
        }

        xInScanedLine.ExceptWith(xOfOthersInY);
        return xInScanedLine.Count.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var sbps = await ReadSensorsAndBeacons(reader).ToListAsync();
        var edgePoints = new HashSet<Point>();

        var lines = new HashSet<Line>();
        foreach (var sbp in sbps)
        {
            var radius = sbp.Radius + 1;
            var xOffset = new Point(radius, 0);
            var yOffset = new Point(0, radius);
            var left = sbp.Sensor - xOffset;
            var right = sbp.Sensor + xOffset;
            var bottom = sbp.Sensor - yOffset;
            var top = sbp.Sensor + yOffset;
            var newLines = new[]
            {
                Line.Create(left, top),
                Line.Create(left, bottom),
                Line.Create(top, right),
                Line.Create(bottom, right),
            };

            for (int i = 0; i < newLines.Length; i++)
            {
                var toRemove = new HashSet<Line>();
                foreach (var line in lines)
                {
                    if (newLines[i].TryCombineWith(line, out var combined))
                    {
                        toRemove.Add(line);
                        newLines[i] = combined;
                    }
                }

                lines.ExceptWith(toRemove);
                lines.Add(newLines[i]);
            }
        }

        var intersections = new HashSet<Point>();
        foreach (var line in lines)
        {
            foreach (var other in lines)
            {
                if (line == other)
                {
                    continue;
                }

                if (line.TryFindIntersection(other, out var intersection))
                {
                    if (intersection.X < 0)
                    {
                        continue;
                    }

                    if (intersection.Y < 0)
                    {
                        continue;
                    }

                    if (intersection.X > options.GridSize)
                    {
                        continue;
                    }

                    if (intersection.Y > options.GridSize)
                    {
                        continue;
                    }

                    intersections.Add(intersection);
                }
            }
        }

        var result = 0L;
        foreach (var point in intersections)
        {
            if (sbps.Any(sbp => sbp.IsWithinRadius(point)))
            {
                continue;
            }

            result = point.X * 4000000 + point.Y;
            break;
        }

        return result.ToString();
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

            var sensorX = long.Parse(match.Groups[1].Value);
            var sensorY = long.Parse(match.Groups[2].Value);
            var beaconX = long.Parse(match.Groups[3].Value);
            var beaconY = long.Parse(match.Groups[4].Value);
            
            var sensor = new Point(sensorX, sensorY);
            var beacon = new Point(beaconX, beaconY);

            yield return new SensorBeaconPair(sensor, beacon);
        }
    }

    //[GeneratedRegex(@"^Sensor at x=(-?[0-9]+), y=(-?[0-9]+): closest beacon is at x=(-?[0-9]+), y=(-?[0-9]+)$")]
    //private static partial Regex SensorBeaconRegex();
}
