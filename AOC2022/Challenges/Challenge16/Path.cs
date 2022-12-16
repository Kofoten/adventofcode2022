using System.Runtime.InteropServices;

namespace AOC2022.Challenges.Challenge16;

public record Path(string Name, int RemainingTime, int TotalPressureReleased, HashSet<string> Unopened);
