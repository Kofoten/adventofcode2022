using AOC2022.Data;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Xml;

namespace AOC2022.Challenges.Challenge16;

public class Challenge16 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var result = 0;
        var startNode = "AA";
        var timeLimit = 30;

        var caves = await ReadCavesAsync(reader).ToDictionaryAsync(x => x.Name);
        var pressurized = caves.Where(c => c.Value.FlowRate > 0).Select(c => c.Key).ToHashSet();
        var tunnels = FindTunnels(caves, pressurized, startNode);
        var paths = new Queue<Path>(new[] { new Path(startNode, timeLimit, 0, new HashSet<string>()) });

        while (paths.TryDequeue(out var path))
        {
            if (path.Open.Count == pressurized.Count)
            {
                if (path.TotalPressureReleased > result)
                {
                    result = path.TotalPressureReleased;
                }

                continue;
            }

            foreach (var targetName in pressurized.Except(path.Open))
            {
                var tunnel = tunnels[$"{path.Name}{targetName}"];
                var remaining = path.RemainingTime - tunnel - 1;

                if (remaining < 0)
                {
                    if (path.TotalPressureReleased > result)
                    {
                        result = path.TotalPressureReleased;
                    }

                    continue;
                }

                paths.Enqueue(path.AddNode(targetName, remaining, caves[targetName].FlowRate));
            }
        }

        return result.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var result = 0;
        var startNode = "AA";
        var timeLimit = 26;

        var caves = await ReadCavesAsync(reader).ToDictionaryAsync(x => x.Name);
        var pressurized = caves.Where(c => c.Value.FlowRate > 0).Select(c => c.Key).ToHashSet();
        var tunnels = FindTunnels(caves, pressurized, startNode);
        var paths = new Queue<Path>(new[] { new Path(startNode, timeLimit, 0, new HashSet<string>()) });

        while (paths.TryDequeue(out var path))
        {
            if (path.Open.Count == pressurized.Count)
            {
                continue;
            }

            foreach (var targetName in pressurized.Except(path.Open))
            {
                var tunnel = tunnels[$"{path.Name}{targetName}"];
                var remaining = path.RemainingTime - tunnel - 1;

                if (remaining < 0)
                {
                    continue;
                }

                paths.Enqueue(path.AddNode(targetName, remaining, caves[targetName].FlowRate));
            }
        }

        return result.ToString();
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

    public static IDictionary<string, int> FindTunnels(IDictionary<string, Cave> caves, HashSet<string> pressurized, string startNode)
    {
        var tunnels = new Dictionary<string, int>();

        foreach (var cave in pressurized.Append(startNode).Select(n => caves[n]))
        {
            var finders = new Queue<TunnelFinder>(cave.Connections.Select(c => new TunnelFinder(c)));
            while (finders.TryDequeue(out var finder))
            {
                if (finder.CurrentName == cave.Name)
                {
                    continue;
                }

                var current = caves[finder.CurrentName];
                if (current.FlowRate > 0 || current.Name == startNode)
                {
                    var key = $"{cave.Name}{current.Name}";
                    if (tunnels.TryGetValue(key, out var travelTime))
                    {
                        if (finder.Visited.Count < travelTime)
                        {
                            tunnels[key] = finder.Visited.Count;
                        }
                    }
                    else
                    {
                        tunnels.Add(key, finder.Visited.Count);
                    }
                }

                foreach (var connection in current.Connections)
                {
                    if (!finder.Visited.Contains(connection))
                    {
                        finders.Enqueue(finder.Add(connection));
                    }
                }
            }
        }

        return tunnels;
    }
}
