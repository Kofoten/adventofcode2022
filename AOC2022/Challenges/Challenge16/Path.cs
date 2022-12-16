namespace AOC2022.Challenges.Challenge16;

public record Path(string? Previous, int Time, int Value, HashSet<string> Opened);
