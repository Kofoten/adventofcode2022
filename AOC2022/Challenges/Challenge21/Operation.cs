using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge21;

public partial class Operation
{
    public string Registry { get; private init; }
    public string Operand1 { get; private init; }
    public string Operand2 { get; private init; }
    public OperationType Type { get; private init; }

    public bool IsValueOperation => Type == OperationType.Value;

    private readonly long? value;

    public Operation(string registry, long value)
    {
        Registry = registry;
        this.value = value;
        Operand1 = string.Empty;
        Operand2 = string.Empty;
        Type = OperationType.Value;
    }

    public Operation(string registry, string operand1, string operand2, OperationType type)
    {
        Registry = registry;
        value = null;
        Operand1 = operand1;
        Operand2 = operand2;
        Type = type;
    }

    public long Perform(IReadOnlyDictionary<string, long> values)
    {
        if (Type == OperationType.Value)
        {
            if (value is not null)
            {
                return value.Value;
            }

            throw new InvalidOperationException("Operation with type value must contain a value");
        }

        return Perform(values[Operand1], values[Operand2], Type);
    }

    public static long Perform(long operand1, long operand2, OperationType type)
    {
        return type switch
        {
            OperationType.Addition => operand1 + operand2,
            OperationType.Subtraction => operand1 - operand2,
            OperationType.Multiplication => operand1 * operand2,
            OperationType.Divide => operand1 / operand2,
            _ => throw new UnreachableException("Unknown operation type encountered"),
        };
    }

    public static Operation Parse(string value)
    {
        var match = ParseRegex().Match(value);
        if (!match.Success)
        {
            throw new InvalidDataException("Input contains invalid data");
        }

        if (!string.IsNullOrEmpty(match.Groups[2].Value))
        {
            return new Operation(match.Groups[1].Value, long.Parse(match.Groups[2].Value));
        }

        var type = match.Groups[4].Value switch
        {
            "+" => OperationType.Addition,
            "-" => OperationType.Subtraction,
            "*" => OperationType.Multiplication,
            "/" => OperationType.Divide,
            _ => throw new InvalidDataException("Input contains invalid data"),
        };

        return new Operation(match.Groups[1].Value, match.Groups[3].Value, match.Groups[5].Value, type);
    }

    [GeneratedRegex(@"^([a-z]+): (?:([0-9]+)|([a-z]+) (\+|-|\*|\/) ([a-z]+))$")]
    private static partial Regex ParseRegex();
}
