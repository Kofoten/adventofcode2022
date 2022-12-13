namespace AOC2022.Challenges.Challenge13;

public class Packet
{
    private readonly int? value;
    private readonly IList<Packet> inner;

    public bool IsList => value.HasValue;

    public Packet(int? value = null)
    {
        this.value = value;
        inner = new List<Packet>();
    }

    public Packet AsList()
    {
        if (value.HasValue)
        {
            var packet = new Packet();
            packet.inner.Add(this);
            return packet;
        }

        throw new InvalidOperationException();
    }

    public override string ToString()
    {
        if (value.HasValue)
        {
            return value.Value.ToString();
        }

        return $"[{string.Join(',', inner)}]";
    }

    public static bool operator <=(Packet left, Packet right)
    {
        if (left.value.HasValue)
        {
            if (right.value.HasValue)
            {
                return left.value.Value <= right.value.Value;
            }

            return left.AsList() <= right;
        }
        
        if (right.value.HasValue)
        {
            return left <= right.AsList();
        }

        for (int i = 0; ; i++)
        {
            if (i == left.inner.Count && i < right.inner.Count)
            {
                return true;
            }

            if (i == right.inner.Count)
            {
                return false;
            }

            if (!(left.inner[i] <= right.inner[i]))
            {
                return false;
            }
        }
    }

    public static bool operator >=(Packet left, Packet right)
    {
        if (left.value.HasValue)
        {
            if (right.value.HasValue)
            {
                return left.value.Value >= right.value.Value;
            }

            return left.AsList() >= right;
        }

        if (right.value.HasValue)
        {
            return left >= right.AsList();
        }

        for (int i = 0; ; i++)
        {
            if (i == left.inner.Count && i < right.inner.Count)
            {
                return false;
            }

            if (i == right.inner.Count)
            {
                return true;
            }

            if (left.inner[i] >= right.inner[i])
            {
                return true;
            }
        }
    }

    public static Packet Parse(string data)
    {
        if (data.Length < 2)
        {
            throw new InvalidDataException("Input contains invalid data");
        }

        if (data[0] != '[')
        {
            throw new InvalidDataException("Input contains invalid data");
        }

        var contentStop = data.Length - 1;
        if (data[contentStop] != ']')
        {
            throw new InvalidDataException("Input contains invalid data");
        }

        var packet = new Packet();
        var inner = string.Empty;
        var depth = 0;
        foreach (var c in data[1..contentStop])
        {
            if (c == '[')
            {
                inner += c;
                depth++;
            }
            else if (c == ']')
            {
                inner += c;
                depth--;

                if (depth == 0)
                {
                    packet.inner.Add(Parse(inner));
                    inner = string.Empty;
                }
            }
            else if (depth > 0)
            {
                inner += c;
            }
            else if (c == ',')
            {
                if (inner.Length > 0 && int.TryParse(inner, out var v))
                {
                    packet.inner.Add(new Packet(v));
                    inner = string.Empty;
                }
                continue;
            }
            else if (char.IsDigit(c))
            {
                inner += c;                
            }
            else
            {
                throw new InvalidDataException("Input contains invalid data");
            }
        }

        if (inner.Length > 0 && int.TryParse(inner, out var val))
        {
            packet.inner.Add(new Packet(val));
        }

        return packet;
    }
}
