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

        var start = caves[startName];
        var startPressure = (timeLimit - 1) * start.Pressure;
        var startVisit = new Path(null, startPressure == 0 ? 0 : 1, startPressure, new HashSet<string>());
        if (startPressure != 0)
        {
            startVisit.Opened.Add(startName);
        }
        visits.Add(startName, startVisit);

        while (toCheck.TryDequeue(out var caveName))
        {
            var currentVisit = visits[caveName];
            if (currentVisit.Time >= timeLimit)
            {
                continue;
            }

            var currentCave = caves[caveName];
            foreach (var connection in currentCave.Connections)
            {
                var opened = new HashSet<string>(currentVisit.Opened);
                if (!visits.TryGetValue(connection, out var visit))
                {
                    visit = new Path(connection, currentVisit.Time + 1, 0, opened);
                }

                if (!opened.Contains(connection))
                {
                    var time = currentVisit.Time + 2;
                    var remainingTime = timeLimit - time;
                    var totalPressure = remainingTime * caves[connection].Pressure + currentVisit.Value;
                    if (visit.Value < totalPressure)
                    {
                        opened.Add(connection);
                        visit = new Path(caveName, time, totalPressure, opened);
                    }
                }

                if (visit.Time > timeLimit)
                {
                    continue;
                }

                visits[connection] = visit;
                toCheck.Enqueue(connection);
            }
        }
        

        return "asd";
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
