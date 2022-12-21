using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace AOC2022.Challenges.Challenge21;

public class Challenge21 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var operations = await reader.ReadAllLinesAsync()
            .Select(Operation.Parse)
            .ToDictionaryAsync(o => o.Registry);

        var stack = StackOperations(operations, "root");
        var result = CalculateStack(operations, stack);
        return result.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var operations = await reader.ReadAllLinesAsync()
            .Select(Operation.Parse)
            .ToDictionaryAsync(o => o.Registry);

        if (!TryFinPathToOperation(operations, "root", "humn", out var path))
        {
            throw new InvalidDataException("Input contains invalid data");
        }

        var current = operations["root"];
        long? value = null;
        while (current.Registry != "humn")
        {
            var next = string.Empty;
            Stack<string> stack;
            if (path.Contains(current.Operand1))
            {
                stack = StackOperations(operations, current.Operand2);
                next = current.Operand1;
            }
            else
            {
                stack = StackOperations(operations, current.Operand1);
                next = current.Operand2;
            }

            var currentValue = CalculateStack(operations, stack);
            if (value is null)
            {
                value = currentValue;
            }
            else
            {
                var operationType = current.Type.Invert();
                value = Operation.Perform(value.Value, currentValue, operationType);
            }

            current = operations[next];
        }

        return (value ?? 0).ToString();
    }

    private static Stack<string> StackOperations(IReadOnlyDictionary<string, Operation> operations, string root)
    {
        var stack = new Stack<string>();
        var queue = new Queue<string>(new[] { root });
        while (queue.TryDequeue(out var name))
        {
            var operation = operations[name];
            stack.Push(operation.Registry);
            
            if (!operation.IsValueOperation)
            {
                queue.Enqueue(operation.Operand1);
                queue.Enqueue(operation.Operand2);
            }
        }
        return stack;
    }

    private static long CalculateStack(IReadOnlyDictionary<string, Operation> operations, Stack<string> stack)
    {
        var last = string.Empty;
        var values = new Dictionary<string, long>();

        while (stack.TryPop(out var name))
        {
            last = name;
            var operation = operations[name];
            var value = operation.Perform(values);
            values.Add(name, value);
        }

        return values[last];
    }

    private static bool TryFinPathToOperation(IReadOnlyDictionary<string, Operation> operations, string root, string registry, [NotNullWhen(true)] out List<string>? path)
    {
        if (root == registry)
        {
            path = new List<string> { registry };
            return true;
        }

        var current = operations[root];
        if (current.IsValueOperation)
        {
            path = null;
            return false;
        }

        if (TryFinPathToOperation(operations, current.Operand1, registry, out var inner))
        {
            path = new List<string>() { current.Registry };
            path.AddRange(inner);
            return true;
        }

        if (TryFinPathToOperation(operations, current.Operand2, registry, out inner))
        {
            path = new List<string>() { current.Registry };
            path.AddRange(inner);
            return true;
        }

        path = null;
        return false;
    }
}
