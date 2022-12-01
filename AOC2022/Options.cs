﻿using System.Diagnostics.CodeAnalysis;

namespace AOC2022
{
    public record Options(int Challange, int Part, FileInfo InputFile)
    {
        public static bool TryParse(string[] args, [NotNullWhen(true)] out Options? options, [NotNullWhen(false)] out string? reason)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                options = null;
                reason = "Invalid number of arguments, allowed arguments are: challange (positional: index 0, integer), part (positional: index 1, integer), inputFile (optional, positional: index 2, filename).";
                return false;
            }

            if (!int.TryParse(args[0], out var challange) || challange < 1 || challange > 25)
            {
                options = null;
                reason = "Challange (positional argument 0) must be a valid integer with a minimum value of 1 and a maximum value of 25.";
                return false;
            }

            if (!int.TryParse(args[1], out var part) || part < 1 || part > 2)
            {
                options = null;
                reason = "Part (positional argument 1) must be a valid integer with a minimum value of 1 and a maximum value of 2.";
                return false;
            }

            var inputFile = new FileInfo($"./input/challange{challange}/input.txt");
            if (args.Length > 2)
            {
                if (args[2] == "test")
                {
                    inputFile = new FileInfo($"./input/challange{challange}/test.txt");
                }
                else if (Path.Exists(args[2]))
                {
                    inputFile = new FileInfo(args[2]);
                }
                else
                {
                    options = null;
                    reason = "Input file (optional positional argument 2) must refer to an existing file or the text test to indicate usage of the test file.";
                    return false;
                }
            }

            options = new Options(challange, part, inputFile);
            reason = null;
            return true;
        }
    }
}
