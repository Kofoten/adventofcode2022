using System.Diagnostics;

namespace AOC2022.Challenges.Challenge21;

public static class OperationTypeExtensions
{
    public static OperationType Invert(this OperationType type)
    {
        return type switch
        {
            OperationType.Divide => OperationType.Multiplication,
            OperationType.Multiplication => OperationType.Divide,
            OperationType.Addition => OperationType.Subtraction,
            OperationType.Subtraction=> OperationType.Addition,
            _ => throw new UnreachableException("Can not invert type"),
        };
    }
}
