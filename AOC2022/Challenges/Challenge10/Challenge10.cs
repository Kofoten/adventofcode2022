namespace AOC2022.Challenges.Challenge10;

public class Challenge10 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var x = 1;
        var timeout = 0;
        var toAdd = 0;
        var result = 0;

        for (int i = 0; ; i++)
        {
            if ((i - 20) % 40 == 0)
            {
                result += i * x;
            }

            if (timeout == 0)
            {
                x += toAdd;

                if (!reader.CanRead)
                {
                    break;
                }

                var line = await reader.ReadLineAsync();
                var parts = line.Split(' ');
                switch (parts[0])
                {
                    case "noop":
                        toAdd = 0;
                        timeout = 1;
                        break;
                    case "addx":
                        toAdd = int.Parse(parts[1]);
                        timeout = 2;
                        break;
                    default:
                        throw new InvalidDataException("Input contains invalid data");
                }
            }

            timeout -= 1;
        }

        return result.ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var ticks = 240;
        var x = 1;
        var timeout = 0;
        var toAdd = 0;
        var pixels = Enumerable.Repeat(' ', ticks).ToArray();

        for (int i = 0; i < ticks; i++)
        {
            if (timeout == 0)
            {
                x += toAdd;

                if (!reader.CanRead)
                {
                    break;
                }

                var line = await reader.ReadLineAsync();
                var parts = line.Split(' ');
                switch (parts[0])
                {
                    case "noop":
                        toAdd = 0;
                        timeout = 1;
                        break;
                    case "addx":
                        toAdd = int.Parse(parts[1]);
                        timeout = 2;
                        break;
                    default:
                        throw new InvalidDataException("Input contains invalid data");
                }
            }

            if (Math.Abs(x - (i % 40)) <= 1)
            {
                pixels[i] = '#';
            }

            timeout -= 1;
        }

        return string.Join('\n', pixels.Chunk(40).Select(l => string.Concat(l)));
    }
}
