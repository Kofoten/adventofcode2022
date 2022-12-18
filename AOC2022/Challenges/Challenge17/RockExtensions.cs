namespace AOC2022.Challenges.Challenge17;

public static class RockExtensions
{
    public static bool Intersects(this IRock me, IRock other)
    {
        var diff = Math.Abs(me.Position.X - other.Position.X);
        if (diff > 5)
        {
            return false;
        }

        return me.GetCurrentPoints().ToHashSet().Overlaps(other.GetCurrentPoints());
    }
}
