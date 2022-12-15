namespace AOC2022.Challenges.Challenge15;

public record Line(Point Start, Point End, LineFunction Function)
{
    public bool TryCombineWith(Line other, [NotNullWhen(true)] out Line? result)
    {
        if (Function != other.Function)
        {
            result = null;
            return false;
        }

        if (Start.X > other.End.X + 1 || other.Start.X > End.X + 1)
        {
            result = null;
            return false;
        }

        var start = Start.X < other.Start.X ? Start : other.Start;
        var end = End.X > other.End.X ? End : other.End;
        result = new Line(start, end, Function);
        return true;
    }

    public bool TryFindIntersection(Line other, [NotNullWhen(true)] out Point? intersection)
    {
        if (Function.K == other.Function.K)
        {
            intersection = null;
            return false;
        }

        long x;
        if (Function.K < 0)
        {
            x = Function.M - other.Function.M;
        }
        else
        {
            x = other.Function.M - Function.M;
        }

        if ((x & 1L) != 0)
        {
            intersection = null;
            return false;
        }

        x /= 2;
        if (x > End.X || x > other.End.X || x < Start.X || x < other.Start.X)
        {
            intersection = null;
            return false;
        }

        intersection = new Point(x, Function.Y(x));
        return true;
    }

    public static Line Create(Point start, Point end)
    {
        var k = NormalizeInteger(end.Y - start.Y);
        var m = start.Y - k * start.X;
        var function = new LineFunction(k, m);
        return new Line(start, end, function);
    }

    private static long NormalizeInteger(long value)
    {
        if (value == 0)
        {
            return 0;
        }
        else if (value < 0)
        {
            return value / value * -1;
        }
        else
        {
            return value / value;
        }
    }
}
