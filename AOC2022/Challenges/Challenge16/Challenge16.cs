using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge16;

public class Challenge16 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var timeLimit = 30;
        var startName = "AA";
        var caves = await ReadCavesAsync(reader).ToDictionaryAsync(x => x.Name);
        var visits = new Dictionary<string, Path>();
        var toCheck = new Queue<string>(new[] { startName });

        while (toCheck.TryDequeue(out var caveName))
        {
            var time = 0;
            var value = -1;
            var opened = new HashSet<string>();
            
            if (visits.TryGetValue(caveName, out var path))
            {
                time = path.Time;
                value = path.Value;
                opened = new HashSet<string>(path.Opened);
            }

            if (time >= timeLimit)
            {
                continue;
            }

            Path? highestNeigbour = null;
            var cave = caves[caveName];
            foreach (var connection in cave.Connections)
            {
                if (visits.TryGetValue(connection, out var neighbour))
                {
                    if (neighbour.Value > (highestNeigbour?.Value ?? -1))
                    {
                        highestNeigbour = neighbour;
                    }
                }
            }

            var dirty = false;
            if (highestNeigbour is not null && highestNeigbour.Value > value)
            {
                time = highestNeigbour.Time + 1;
                value = highestNeigbour.Value;
                opened = new HashSet<string>(highestNeigbour.Opened);
                dirty = true;
            }

            if (!opened.Contains(caveName) && cave.Pressure > 0)
            {
                time++;
                var remaining = timeLimit - time;
                value = cave.Pressure * remaining + (highestNeigbour?.Value ?? 0);
                opened.Add(caveName);
                dirty = true;
            }

            if (time > timeLimit)
            {
                continue;
            }

            if (dirty || highestNeigbour is null)
            {
                visits[caveName] = new Path(time, value == -1 ? 0: value, opened);
                foreach (var connection in cave.Connections)
                {
                    if (!toCheck.Contains(connection))
                    {
                        toCheck.Enqueue(connection);
                    }
                }
            }
        }

        return visits.Values.Select(v => v.Value).Max().ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        throw new PartNotImplementedException(2);
    }

    public static async IAsyncEnumerable<Cave> ReadCavesAsync(IInputReader reader)
    {
        var regex = new Regex(@"^Valve ([A-Z]{2}) has flow rate=([0-9]+); tunnels? leads? to valves? ([A-Z,\s]*)$");

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var match = regex.Match(line);
            if (!match.Success)
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var name = match.Groups[1].Value;
            var pressure = int.Parse(match.Groups[2].Value);
            var connections = match.Groups[3].Value.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            yield return new Cave(name, pressure, connections);
        }
    }
}
