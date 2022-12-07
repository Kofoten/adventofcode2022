namespace AOC2022.Challenges.Challenge07;

public interface IItem
{
    public DirectoryItem? Parent { get; }
    string Name { get; }
    int Size { get; }
}
