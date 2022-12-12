namespace AOC2022.Challenges.Challenge12;

public class Challenge12 : IChallenge
{
    private const int LOWERCASE_A_CODE = 97;

    public async Task<string> Part1(IInputReader reader)
    {
        var start = Point.Zero;
        var peak = Point.Zero;
        var grid = new List<IList<Node>>();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var row = new List<Node>();
            foreach (var c in line)
            {
                switch (c)
                {
                    case 'S':
                        start = new(row.Count, grid.Count);
                        row.Add(new Node(0, 0));
                        break;
                    case 'E':
                        peak = new(row.Count, grid.Count);
                        row.Add(new Node(122 - LOWERCASE_A_CODE));
                        break;
                    default:
                        row.Add(new Node(c - LOWERCASE_A_CODE));
                        break;
                }
            }

            grid.Add(row);
        }

        var gridSize = new Point(grid[0].Count, grid.Count);
        var queue = new Queue<Point>(new[] { start });
        while (queue.TryDequeue(out var point))
        {
            var neighbours = point.GetNeighbours(gridSize).ToList();
            var current = grid[point.Y][point.X];

            foreach (var np in neighbours)
            {
                var neighbour = grid[np.Y][np.X];
                
                if (neighbour.Height <= current.Height + 1 && neighbour.StepsTo > current.StepsTo + 1)
                {
                    neighbour.StepsTo = current.StepsTo + 1;
                    queue.Enqueue(np);
                }
            }
        }

        return grid[peak.Y][peak.X].StepsTo.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var peak = Point.Zero;
        var grid = new List<IList<Node>>();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var row = new List<Node>();
            foreach (var c in line)
            {
                switch (c)
                {
                    case 'S':
                        row.Add(new Node(0));
                        break;
                    case 'E':
                        peak = new(row.Count, grid.Count);
                        row.Add(new Node(122 - LOWERCASE_A_CODE, 0));
                        break;
                    default:
                        row.Add(new Node(c - LOWERCASE_A_CODE));
                        break;
                }
            }

            grid.Add(row);
        }

        var gridSize = new Point(grid[0].Count, grid.Count);
        var queue = new Queue<Point>(new[] { peak });
        while (queue.TryDequeue(out var point))
        {
            var neighbours = point.GetNeighbours(gridSize).ToList();
            var current = grid[point.Y][point.X];

            foreach (var np in neighbours)
            {
                var neighbour = grid[np.Y][np.X];

                if (neighbour.Height >= current.Height - 1 && neighbour.StepsTo > current.StepsTo + 1)
                {
                    neighbour.StepsTo = current.StepsTo + 1;
                    if (neighbour.Height != 0)
                    {
                        queue.Enqueue(np);
                    }
                }
            }
        }

        var lowPoints = grid.SelectMany(x => x).Where(x => x.Height == 0).OrderBy(x => x.StepsTo);
        return lowPoints.First().StepsTo.ToString();
    }
}
