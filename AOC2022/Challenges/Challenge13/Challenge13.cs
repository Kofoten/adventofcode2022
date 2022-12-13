using AOC2022.Data;

namespace AOC2022.Challenges.Challenge13;

public class Challenge13 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var result = new List<int>();

        for (int i = 1; reader.CanRead; i++)
        {
            var (left, right) = await reader.ReadPacketReaderPairAsync();
            if (IsLower(left, right))
            {
                result.Add(i);
            }
        }

        return result.Sum().ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var dividers = new[] { "[[2]]", "[[6]]" };
        var packets = await reader.ReadAllLinesAsync()
            .Where(x => !string.IsNullOrEmpty(x))
            .ToListAsync();

        packets.AddRange(dividers);

        var result = 1;
        foreach (var divider in dividers)
        {
            result *= 1 + packets.Where(x => IsLower(x, divider)).Count();
        }

        return result.ToString();
    }

    private static bool IsLower(string left, string right)
    {
        var leftReader = new PacketReader(left);
        var rightReader = new PacketReader(right);

        while (!leftReader.EndOfPacket && !rightReader.EndOfPacket)
        {
            var leftToken = leftReader.ReadNextToken();
            var rightToken = rightReader.ReadNextToken();
            if (leftToken == rightToken)
            {
                continue;
            }

            if (leftToken.Type == PacketTokenType.ListStart)
            {
                if (rightToken.Type == PacketTokenType.ListEnd)
                {
                    break;
                }

                if (rightToken.Type == PacketTokenType.Value)
                {
                    rightReader.CreateListAtCurrent();
                    continue;
                }
            }

            if (rightToken.Type == PacketTokenType.ListStart)
            {
                if (leftToken.Type == PacketTokenType.ListEnd)
                {
                    return true;
                }

                if (leftToken.Type == PacketTokenType.Value)
                {
                    leftReader.CreateListAtCurrent();
                    continue;
                }
            }

            if (leftToken.Type == PacketTokenType.ListEnd)
            {
                return true;
            }

            if (rightToken.Type == PacketTokenType.ListEnd)
            {
                break;
            }

            if (leftToken.Type == PacketTokenType.Value && rightToken.Type == PacketTokenType.Value)
            {
                if (leftToken.Value < rightToken.Value)
                {
                    return true;
                }

                break;
            }
        }

        if (leftReader.EndOfPacket && !rightReader.EndOfPacket)
        {
            return true;
        }

        return false;
    }
}
