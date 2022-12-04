namespace AOC2022.Challenges.Challenge04;

public class SectionRange
{
    public int Start { get; private init; }
    public int Stop { get; private init; }

    public SectionRange(int start, int stop)
    {
        Start = start;
        Stop = stop;
    }

    public bool Contains(SectionRange other)
    {
        return other.Start >= Start && other.Stop <= Stop;
    }

    public bool Intersects(SectionRange other)
    {
        // NOTE: For the ranges to intersect one start must exist within the other range.
        if (other.Start >= Start && other.Start <= Stop)
        {
            return true;
        }

        return Start >= other.Start && Start <= other.Stop;
    }

    public override string ToString()
    {
        return $"{Start} - {Stop}";
    }

    public static SectionRange Parse(string value)
    {
        var parts = value.Split('-');
        var start = int.Parse(parts[0]);
        var stop = int.Parse(parts[1]);
        return new SectionRange(start, stop);
    }
}
