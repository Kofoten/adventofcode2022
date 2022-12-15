namespace AOC2022.Challenges.Challenge15;

public record LineFunction(long K, long M)
{
    public long Y(long x) => K * x + M;

    public Point PointAtX(long x) => new(x, Y(x));
}
