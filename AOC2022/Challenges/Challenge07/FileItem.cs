namespace AOC2022.Challenges.Challenge07;

public class FileItem : IItem
{
    public DirectoryItem? Parent { set; get; }
    public string Name { get; init; }
    public int Size { get; init; }

    public FileItem(string name, int size, DirectoryItem parent)
    {
        Name = name;
        Size = size;
        Parent = parent;
    }
}
