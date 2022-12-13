namespace AOC2022.Challenges.Challenge13;

public class Challenge13 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var result = new HashSet<int>();

        for (int i = 1; reader.CanRead; i++)
        {
            var (left, right) = await reader.ReadPacketReaderPairAsync();

            while (!left.EndOfPacket && !right.EndOfPacket)
            {
                var leftToken = left.ReadNextToken();
                var rightToken = right.ReadNextToken();
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
                        right.CreateListAtCurrent();
                        continue;
                    }
                }

                if (rightToken.Type == PacketTokenType.ListStart)
                {
                    if (leftToken.Type == PacketTokenType.ListEnd)
                    {
                        result.Add(i);
                        break;
                    }

                    if (leftToken.Type == PacketTokenType.Value)
                    {
                        left.CreateListAtCurrent();
                        continue;
                    }
                }

                if (leftToken.Type == PacketTokenType.ListEnd)
                {
                    result.Add(i);
                    break;
                }

                if (rightToken.Type == PacketTokenType.ListEnd)
                {
                    break;
                }

                if (leftToken.Type == PacketTokenType.Value && rightToken.Type == PacketTokenType.Value)
                {
                    if (leftToken.Value < rightToken.Value)
                    {
                        result.Add(i);
                    }

                    break;
                }
            }

            if (left.EndOfPacket && !right.EndOfPacket)
            {
                result.Add(i);
            }
        }

        return result.Sum().ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        throw new PartNotImplementedException(2);
    }
}
