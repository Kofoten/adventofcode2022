namespace AOC2022.Challenges.Challenge16;

public record Path(int Time, int Value, HashSet<string> Opened)
{
    public override string ToString()
    {
        return $"Time: {Time}, Value; {Value}, Opened: {string.Join("; ", Opened)}";
    }
}
