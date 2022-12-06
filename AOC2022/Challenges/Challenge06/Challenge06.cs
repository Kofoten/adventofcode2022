namespace AOC2022.Challenges.Challenge06;

public class Challenge06 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var line = await reader.ReadLineAsync();
        return FindFirstMarker(line, 4).ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var line = await reader.ReadLineAsync();
        return FindFirstMarker(line, 14).ToString();
    }

    private static int FindFirstMarker(string line, int markerSize)
    {
        var previous = new List<char>();

        for (int i = 0; i < line.Length; i++)
        {
            previous.Add(line[i]);
            if (previous.Count == markerSize)
            {
                if (!HasDuplicates(previous))
                {
                    return (1 + i);
                }

                previous.RemoveAt(0);
            }
        }

        return -1;
    }

    private static bool HasDuplicates(IList<char> values)
    {
        for (int i = 0; i < values.Count(); i++)
        {
            for (int j = 0; j < values.Count(); j++)
            {
                if (i == j)
                {
                    continue;
                }

                if (values[i] == values[j])
                {
                    return true;
                }
            }
        }

        return false;
    }
}

