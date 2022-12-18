using System.Security.Cryptography.X509Certificates;

namespace AOC2022.Challenges.Challenge18;

public class Challenge18 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var input = await ReadInput(reader);
        var sides = 0L;

        for (long i = input.Min; i <= input.Max; i++)
        {
            for (long j = input.Min; j <= input.Max; j++)
            {
                var previousX = false;
                for (long x = input.Min; x <= input.Max; x++)
                {
                    var point = new Point(x, i, j);
                    var exists = input.Points.Contains(point);

                    if (exists != previousX)
                    {
                        sides++;
                        previousX = exists;
                    }
                }

                var previousY = false;
                for (long y = input.Min; y <= input.Max; y++)
                {
                    var point = new Point(i, y, j);
                    var exists = input.Points.Contains(point);

                    if (exists != previousY)
                    {
                        sides++;
                        previousY = exists;
                    }
                }

                var previousZ = false;
                for (long z = input.Min; z <= input.Max; z++)
                {
                    var point = new Point(i, j, z);
                    var exists = input.Points.Contains(point);

                    if (exists != previousZ)
                    {
                        sides++;
                        previousZ = exists;
                    }
                }
            }
        }

        return sides.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var input = await ReadInput(reader);
        var minPoint = new Point(input.Min, input.Min, input.Min);
        var air = new HashSet<Point>();
        var next = new Queue<Point>(new[] { minPoint });

        var count = 0L;
        while (next.TryDequeue(out var current))
        {
            air.Add(current);

            var neighbours = GetNeighbours(current, input.Min, input.Max).ToList();
            foreach (var neighbour in neighbours)
            {
                if (air.Contains(neighbour))
                {
                    continue;
                }

                if (input.Points.Contains(neighbour))
                {
                    count++;
                }
                else if (!next.Contains(neighbour))
                {
                    next.Enqueue(neighbour);
                }
            }
        }

        return count.ToString();
    }

    private static IEnumerable<Point> GetNeighbours(Point point, long min, long max)
    {
        if (point.X > min)
        {
            yield return new Point(point.X - 1, point.Y, point.Z); 
        }

        if (point.Y > min)
        {
            yield return new Point(point.X, point.Y - 1, point.Z);
        }

        if (point.Z > min)
        {
            yield return new Point(point.X, point.Y, point.Z - 1);
        }

        if (point.X < max)
        {
            yield return new Point(point.X + 1, point.Y, point.Z);
        }

        if (point.Y < max)
        {
            yield return new Point(point.X, point.Y + 1, point.Z);
        }

        if (point.Z < max)
        {
            yield return new Point(point.X, point.Y, point.Z + 1);
        }
    }

    private static async Task<Input> ReadInput(IInputReader reader)
    {
        var points = new HashSet<Point>();
        var min = long.MaxValue;
        var max = long.MinValue;

        while (reader.CanRead)
        {
            var point = Point.Parse(await reader.ReadLineAsync());
            var pMin = point.Values().Min();
            var pMax = point.Values().Max();

            if (pMin < min)
            {
                min = pMin;
            }

            if (pMax > max)
            {
                max = pMax;
            }

            points.Add(point);
        }

        return new Input(min - 1, max + 1, points);
    }
}
