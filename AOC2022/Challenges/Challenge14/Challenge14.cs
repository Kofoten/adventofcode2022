namespace AOC2022.Challenges.Challenge14;

public class Challenge14 : IChallenge
{
    private readonly int[] xOffsetPriority = new[] { 0, -1, 1 };

    public async Task<string> Part1(IInputReader reader)
    {
        var rocksAndSand = await ReadRocksAsync(reader);
        var maxY = rocksAndSand.Select(x => x.Y).Max();
        var result = 0;

        for (int i = 0; ; i++)
        {
            var current = new Point(500, 0);
            var falling = true;
            while (falling)
            {
                var next = Point.Zero;
                foreach (var xOffset in xOffsetPriority)
                {
                    var toTest = new Point(current.X + xOffset, current.Y + 1);
                    if (!rocksAndSand.Contains(toTest))
                    {
                        next = toTest;
                        break;
                    }
                }

                if (next == Point.Zero)
                {
                    falling = false;
                }
                else
                {
                    current = next;
                    if (current.Y > maxY)
                    {
                        break;
                    }
                }
            }

            if (falling)
            {
                result = i;
                break;
            }
            else
            {
                rocksAndSand.Add(current);
            }
        }

        return result.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var rocksAndSand = await ReadRocksAsync(reader);
        var maxY = 2 + rocksAndSand.Select(x => x.Y).Max();
        var result = 0;

        for (int i = 0; ; i++)
        {
            var current = new Point(500, 0);
            if (rocksAndSand.Contains(current))
            {
                result = i;
                break;
            }

            while (true)
            {
                if (current.Y + 1 == maxY)
                {
                    break;
                }

                var next = Point.Zero;
                foreach (var xOffset in xOffsetPriority)
                {
                    var toTest = new Point(current.X + xOffset, current.Y + 1);
                    if (!rocksAndSand.Contains(toTest))
                    {
                        next = toTest;
                        break;
                    }
                }

                if (next == Point.Zero)
                {
                    break;
                }

                current = next;
            }

            rocksAndSand.Add(current);
        }

        return result.ToString();
    }

    private static async Task<HashSet<Point>> ReadRocksAsync(IInputReader reader)
    {
        var splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
        var rocks = new HashSet<Point>();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var points = line.Split(" -> ", splitOptions).Select(x => Point.Parse(x)).ToList();

            for (int i = 1; i < points.Count; i++)
            {
                var current = points[i - 1];
                var next = points[i];
                var vector = (next - current).Normalize();

                do
                {
                    rocks.Add(current);
                    current += vector;
                }
                while (next != current);

                rocks.Add(next);
            }
        }

        return rocks;
    }
}
