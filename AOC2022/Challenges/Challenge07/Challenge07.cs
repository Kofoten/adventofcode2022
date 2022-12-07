using System.Collections;
using System.ComponentModel.Design.Serialization;

namespace AOC2022.Challenges.Challenge07;

public class Challenge07 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var root = await ReadFileSystem(reader);
        return root.Flatten().Where(x => x.Size <= 100000).Sum(x => x.Size).ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var totalDiskSpace = 70000000;
        var requiredDiskSpace = 30000000;

        var root = await ReadFileSystem(reader);
        var availableDiskSpace = totalDiskSpace - root.Size;

        var sizes = new List<int>();
        foreach (var directory in root.Flatten())
        {
            var directorySize = directory.Size;
            if (availableDiskSpace + directorySize > requiredDiskSpace)
            {
                sizes.Add(directorySize);
            }
        }

        return sizes.Min().ToString();
    }

    private static async Task<DirectoryItem> ReadFileSystem(InputReader reader)
    {
        var root = new DirectoryItem(string.Empty);
        var current = root;

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            if (line.StartsWith("$ cd"))
            {
                var argument = line[5..line.Length];
                switch (line[5..line.Length])
                {
                    case "/":
                        current = root;
                        break;
                    case "..":
                        current = current.Parent!;
                        break;
                    default:
                        if (current.Children.TryGetValue(argument, out var item))
                        {
                            current = (DirectoryItem)item;
                        }
                        else
                        {
                            current = new DirectoryItem(argument, current);
                            current.Parent!.Children.Add(argument, current);
                        }
                        break;
                }
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else if (line.StartsWith("dir"))
            {
                var directory = new DirectoryItem(line[4..line.Length], current);
                current.Children.Add(directory.Name, directory);
            }
            else
            {
                var values = line.Split(' ');
                var size = int.Parse(values[0]);
                var file = new FileItem(values[1], size, current);
                current.Children.Add(file.Name, file);
            }
        }

        return root;
    }
}
