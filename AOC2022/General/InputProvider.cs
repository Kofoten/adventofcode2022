﻿using System.Reflection;

namespace AOC2022.Input;

public class InputProvider
{
    private readonly DirectoryInfo inputDirectory;

    public InputProvider(DirectoryInfo inputDirectory)
    {
        this.inputDirectory = inputDirectory;
    }

    public FileInfo GetInputFile(int challegeNumber, bool useTestData)
    {
        if (TryGetInputFile(challegeNumber, useTestData, out var file))
        {
            return file;
        }

        throw new IOException($"Could not find any file for challenge {challegeNumber}");
    }

    public bool TryGetInputFile(int challegeNumber, bool useTestData, [NotNullWhen(true)] out FileInfo? file)
    {
        var fileName = useTestData ? "test.txt" : "input.txt";
        var folder = $"challenge{challegeNumber}";
        var path = Path.Combine(inputDirectory.FullName, folder, fileName);

        if (Path.Exists(path))
        {
            file = new FileInfo(path);
            return true;
        }

        file = null;
        return false;
    }

    public static InputProvider Create()
    {
        var assembly = Assembly.GetAssembly(typeof(InputProvider));
        if (assembly is null)
        {
            throw new InvalidOperationException($"Could not find assembly");
        }

        var location = Path.GetDirectoryName(assembly.Location);
        if (location is null)
        {
            throw new NullReferenceException("Could not get assembly directory");
        }

        var directory = new DirectoryInfo(Path.Combine(location, "input"));
        if (directory.Exists)
        {
            return new InputProvider(directory);
        }

        throw new IOException($"Input directory not found");
    }
}
