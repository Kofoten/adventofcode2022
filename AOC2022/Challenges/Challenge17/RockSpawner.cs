namespace AOC2022.Challenges.Challenge17;

public class RockSpawner
{
    public int Spawns { get; private set; }

    public RockSpawner()
    {
        Spawns = 0;
    }

    public IRock Spawn(int height)
    {
        var position = new Point(2, height);
        var index = Spawns == 0 ? 0 : Spawns % 5;
        Spawns++;

        return index switch
        {
            0 => new FlatRock(position),
            1 => new CrossRock(position),
            2 => new AngledRock(position),
            3 => new SpikeRock(position),
            4 => new SquareRock(position),
            _ => throw new InvalidOperationException()
        };
    }
}
