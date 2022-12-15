namespace AOC2022.Options;

public record Challenge15Options : BaseOptions, IChallengeSpecificOptions
{
    public int Row { get; private init; }
    public int GridSize { get; private init; }

    public Challenge15Options(int Challenge, int Part, FileInfo InputFile, int Row, int GridSize)
        : base(Challenge, Part, InputFile)
    {
        this.Row = Row;
        this.GridSize = GridSize;
    }

    public static bool TryParse(int challenge, int part, FileInfo fileInfo, string[] specificArgs, [NotNullWhen(true)] out IOptions? options, [NotNullWhen(false)] out string? reason)
    {
        if (specificArgs.Length != 2 && specificArgs.Length != 4)
        {
            options = null;
            reason = "Invalid number of challenge specific arguments, allowed arguments are: --row (required when running part 1, named, integer), --gridsize (required when running part 2, named, integer).";
            return false;
        }

        int? row = null;
        int? gridsize = null;

        for (int i = 0; i < specificArgs.Length; i += 2)
        {
            switch (specificArgs[i])
            {
                case "--row":
                    {
                        if (int.TryParse(specificArgs[i + 1], out var value))
                        {
                            row = value;
                        }
                        else
                        {
                            options = null;
                            reason = $"Invalid value of option --row, the value must be a valid integer, allowed arguments are: --row (required when running part 1, named, integer), --gridsize (required when running part 2, named, integer).";
                            return false;
                        }
                        break;
                    }
                case "--gridsize":
                    {
                        if (int.TryParse(specificArgs[i + 1], out var value))
                        {
                            gridsize = value;
                        }
                        else
                        {
                            options = null;
                            reason = $"Invalid value of option --gridsize, the value must be a valid integer, allowed arguments are: --row (required when running part 1, named, integer), --gridsize (required when running part 2, named, integer).";
                            return false;
                        }
                        break;
                    }
                default:
                    options = null;
                    reason = $"Invalid specific argument: {specificArgs[i]}, allowed arguments are: --row (required when running part 1, named, integer), --gridsize (required when running part 2, named, integer).";
                    return false;
            }
        }

        options = new Challenge15Options(challenge, part, fileInfo, row ?? 0, gridsize ?? 0);
        reason = null;
        return true;
    }
}
