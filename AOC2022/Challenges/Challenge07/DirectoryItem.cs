namespace AOC2022.Challenges.Challenge07;

public class DirectoryItem : IItem
{
    public DirectoryItem? Parent { set; get; }
    public string Name { get; init; }
    public int Size => Children.Values.Sum(x => x.Size);
    public IDictionary<string, IItem> Children { get; private init; }

    public DirectoryItem(string name, DirectoryItem? parent)
    {
        Parent = parent;
        Name = name;
        Children = new Dictionary<string, IItem>();
    }

    public DirectoryItem(string name) : this(name, null) { }

    public IEnumerable<DirectoryItem> Flatten()
    {
        var directories = Children.Values.OfType<DirectoryItem>();
        return directories.Union(directories.SelectMany(x => x.Flatten()));
    }
}
