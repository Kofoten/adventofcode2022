using System.Numerics;
using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge11;

public partial class Challenge11 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var monkeys = await ReadMonkeys(reader).ToListAsync();

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.TryDequeue(out var item))
                {
                    item = monkey.Inspect(item) / 3;
                    var throwsTo = monkey.ThrowItemTo(item);
                    monkeys[throwsTo].Items.Enqueue(item);
                }
            }
        }

        return monkeys.OrderByDescending(x => x.Inspections).Take(2).Aggregate(1L, (acc, monkey) => acc * monkey.Inspections).ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var monkeys = await ReadMonkeys(reader).ToListAsync();
        var a = monkeys.Aggregate(1, (acc, x) => acc * x.TestValue);

        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.TryDequeue(out var item))
                {
                    var value = monkey.Inspect(item) % a;
                    var throwsTo = monkey.ThrowItemTo(value);
                    monkeys[throwsTo].Items.Enqueue(value);
                }
            }
        }

        var ordered = monkeys.OrderByDescending(x => x.Inspections).ToList();
        return ordered.Take(2).Aggregate(1L, (acc, monkey) => acc * monkey.Inspections).ToString();
    }

    private static async IAsyncEnumerable<Monkey> ReadMonkeys(InputReader reader)
    {
        var startingItemsStartingWith = "  Starting items:";
        var monkeyRegex = MonkeyRegex();
        var operationRegex = OperationRegex();
        var testRegex = TestRegex();
        var throwsToRegex = ThrowsToRegex();

        while (true)
        {
            var currentLine = await reader.ReadLineAsync();
            var monkeyMatch = monkeyRegex.Match(currentLine);
            if (!monkeyMatch.Success)
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var id = int.Parse(monkeyMatch.Groups[1].Value);

            currentLine = await reader.ReadLineAsync();
            if (!currentLine.StartsWith(startingItemsStartingWith))
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var items = new Queue<long>(currentLine[startingItemsStartingWith.Length..currentLine.Length].Trim().Split(", ").Select(long.Parse));

            currentLine = await reader.ReadLineAsync();
            var operationMatch = operationRegex.Match(currentLine);
            if (!operationMatch.Success)
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var func = operationMatch.Groups[1].Value;
            if (!long.TryParse(operationMatch.Groups[2].Value, out var operationValue))
            {
                func += operationMatch.Groups[2].Value;
            }
            
            Func<long, long> operation = func switch
            {
                "*" => (x) => x * operationValue,
                "+" => (x) => x + operationValue,
                "*old" => (x) => x * x,
                "+old" => (x) => x + x,
                _ => throw new InvalidDataException("Input contains invalid data")
            };

            currentLine = await reader.ReadLineAsync();
            var testMatch = testRegex.Match(currentLine);
            if (!testMatch.Success)
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var testValue = int.Parse(testMatch.Groups[1].Value);

            currentLine = await reader.ReadLineAsync();
            var throwsToIfTrueMatch = throwsToRegex.Match(currentLine);
            if (!throwsToIfTrueMatch.Success || throwsToIfTrueMatch.Groups[1].Value != "true")
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var throwToIfTrue = int.Parse(throwsToIfTrueMatch.Groups[2].Value);

            currentLine = await reader.ReadLineAsync();
            var throwsToIfFalseMatch = throwsToRegex.Match(currentLine);
            if (!throwsToIfFalseMatch.Success || throwsToIfFalseMatch.Groups[1].Value != "false")
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            var throwToIfFalse = int.Parse(throwsToIfFalseMatch.Groups[2].Value);

            if (!reader.CanRead)
            {
                yield return new Monkey(id, items, operation, testValue, throwToIfTrue, throwToIfFalse);
                break;
            }

            currentLine = await reader.ReadLineAsync();
            if (!string.IsNullOrEmpty(currentLine))
            {
                throw new InvalidDataException("Input contains invalid data");
            }

            yield return new Monkey(id, items, operation, testValue, throwToIfTrue, throwToIfFalse);
        }
    }

    [GeneratedRegex(@"^Monkey ([0-9]+):$")]
    private static partial Regex MonkeyRegex();

    [GeneratedRegex(@"^  Operation: new = old (\*|\+) (old|[0-9]+)$")]
    private static partial Regex OperationRegex();

    [GeneratedRegex(@"^  Test: divisible by ([0-9]+)$")]
    private static partial Regex TestRegex();

    [GeneratedRegex(@"^    If (true|false): throw to monkey ([0-9]+)$")]
    private static partial Regex ThrowsToRegex();
}
