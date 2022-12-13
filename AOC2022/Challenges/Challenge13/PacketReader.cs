namespace AOC2022.Challenges.Challenge13;

public class PacketReader
{
    private int position;
    private int previousDigitStart;

    public string Data { get; private set; }
    public bool EndOfPacket => position == Data.Length;

    public PacketReader(string data)
    {
        position = 0;
        previousDigitStart = 0;
        Data = data;
    }

    public void CreateListAtCurrent()
    {
        var number = string.Empty;
        for (int i = previousDigitStart; char.IsDigit(Data[i]); i++)
        {
            number += Data[i];
        }

        var clean = Data.Remove(previousDigitStart, number.Length);
        Data = clean.Insert(previousDigitStart, $"[{number}]");
        position = previousDigitStart + 1;
    }

    public PacketToken ReadNextToken()
    {
        if (position == Data.Length)
        {
            throw new IndexOutOfRangeException();
        }

        var c = Data[position++];
        if (c == ',')
        {
            c = Data[position++];
        }
        
        if (char.IsDigit(c))
        {
            previousDigitStart = position - 1;
            var value = c.ToString();
            while (true)
            {
                c = Data[position++];

                if (!char.IsDigit(c))
                {
                    if (c == ']')
                    {
                        position--;
                    }

                    break;
                }

                value += c;
            }

            return new PacketToken(PacketTokenType.Value, int.Parse(value));
        }
        else if (c == '[')
        {
            return new PacketToken(PacketTokenType.ListStart);
        }
        else if (c == ']')
        {
            return new PacketToken(PacketTokenType.ListEnd);
        }
        else
        {
            throw new InvalidDataException("Input contains invalid data");
        }
    }
}
