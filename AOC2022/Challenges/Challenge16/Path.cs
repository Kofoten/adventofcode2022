using System.Runtime.InteropServices;

namespace AOC2022.Challenges.Challenge16;

public record Path(string Name, int RemainingTime, int TotalPressureReleased, HashSet<string> Open)
{
    public Path(string Name, int RemainingTime) : this(Name, RemainingTime, 0, new HashSet<string>()) { }

    public Path AddNode(string name, int remainingTime, int flowRate)
    {
        var open = new HashSet<string>(Open) { name };
        var total = TotalPressureReleased + flowRate * remainingTime;
        return new Path(name, remainingTime, total, open);
    }
}
