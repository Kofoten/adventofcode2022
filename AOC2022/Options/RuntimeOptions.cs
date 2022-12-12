namespace AOC2022.Options;

public record RuntimeOptions(int Challenge, int Part, FileInfo InputFile)
{
    public static bool TryParse(string[] args, [NotNullWhen(true)] out RuntimeOptions? options, [NotNullWhen(false)] out string? reason)
    {
        if (args.Length < 2 || args.Length > 3)
        {
            options = null;
            reason = "Invalid number of arguments, allowed arguments are: challenge (positional: index 0, integer), part (positional: index 1, integer), inputFile (optional, positional: index 2, filename).";
            return false;
        }

        if (!int.TryParse(args[0], out var challenge) || challenge < 1 || challenge > 25)
        {
            options = null;
            reason = "Challenge (positional argument 0) must be a valid integer with a minimum value of 1 and a maximum value of 25.";
            return false;
        }

        if (!int.TryParse(args[1], out var part) || part < 1 || part > 2)
        {
            options = null;
            reason = "Part (positional argument 1) must be a valid integer with a minimum value of 1 and a maximum value of 2.";
            return false;
        }

        var fileName = args.Length == 3 ? args[2] : null;
        if (!TryGetInputFile(challenge, fileName, out var inputFile))
        {
            options = null;
            reason = "Input file (optional positional argument 2) must refer to an existing file or the text test to indicate usage of the test file.";
            return false;
        }

        options = new RuntimeOptions(challenge, part, inputFile);
        reason = null;
        return true;
    }

    private static bool TryGetInputFile(int challangeNumber, string? value, [NotNullWhen(true)] out FileInfo? file)
    {
        var inputProvider = InputFileProvider.Create();
        if (value is null)
        {
            return inputProvider.TryGetInputFile(challangeNumber, false, out file);
        }
        else if (value == "test")
        {
            return inputProvider.TryGetInputFile(challangeNumber, true, out file);
        }
        else if (Path.Exists(value))
        {
            file = new FileInfo(value);
            return true;
        }

        file = null;
        return false;
    }
}
