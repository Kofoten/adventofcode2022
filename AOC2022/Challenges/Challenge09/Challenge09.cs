namespace AOC2022.Challenges.Challenge09;

public class Challenge09 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var head = Point.Zero;
        var tail = Point.Zero;
        var visitations = new HashSet<string>() { tail.ToString() };
        
        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var direction = line.First() switch
            {
                'R' => new Point(1, 0),
                'L' => new Point(-1, 0),
                'U' => new Point(0, 1),
                'D' => new Point(0, -1),
                _ => throw new InvalidDataException("Input contains invalid data"),
            };
            var count = int.Parse(line[2..line.Length]);

            for (var i = 0; i < count; i++)
            {
                head += direction;
                
                var diff = head - tail;
                if (diff.CanNormalize)
                {
                    tail += diff.Normalize();
                }
                
                visitations.Add(tail.ToString());
            }
        }

        return visitations.Count.ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var rope = new List<Point>()
        {
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
            Point.Zero,
        };
        var visitations = new HashSet<string>() { rope.Last().ToString() };

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var direction = line.First() switch
            {
                'R' => new Point(1, 0),
                'L' => new Point(-1, 0),
                'U' => new Point(0, 1),
                'D' => new Point(0, -1),
                _ => throw new InvalidDataException("Input contains invalid data"),
            };
            var count = int.Parse(line[2..line.Length]);

            for (var i = 0; i < count; i++)
            {
                rope[0] += direction;

                for (int j = 1; j < rope.Count; j++)
                {
                    var diff = rope[j - 1] - rope[j];
                    if (diff.CanNormalize)
                    {
                        rope[j] += diff.Normalize();
                    }
                }
                
                visitations.Add(rope.Last().ToString());
            }
        }

        return visitations.Count.ToString();
    }
}
