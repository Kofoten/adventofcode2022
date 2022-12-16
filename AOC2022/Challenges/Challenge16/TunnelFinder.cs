namespace AOC2022.Challenges.Challenge16;

public record TunnelFinder(string CurrentName, HashSet<string> Visited)
{
    public TunnelFinder(string CurrentName) : this(CurrentName, new HashSet<string>(new[] { CurrentName })) { }

    public TunnelFinder Add(string name) => new(name, new HashSet<string>(Visited) { name });
}
