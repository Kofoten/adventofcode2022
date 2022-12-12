using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AOC2022.Challenges.Challenge05;

public class Challenge05 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var cargo = await ReadCargo(reader);
        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var instruction = Instruction.Parse(line);
            for (int i = 0; i < instruction.Count; i++)
            {
                var value = cargo[instruction.From].Pop();
                cargo[instruction.To].Push(value);
            }
        }

        var result = string.Empty;
        foreach (var stack in cargo)
        {
            result += stack.Pop();
        }

        return result;
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var cargo = await ReadCargo(reader);
        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var instruction = Instruction.Parse(line);
            var current = new Stack<char>();
            for (int i = 0; i < instruction.Count; i++)
            {
                var value = cargo[instruction.From].Pop();
                current.Push(value);
            }

            foreach (var value in current)
            {
                cargo[instruction.To].Push(value);
            }
        }

        var result = string.Empty;
        foreach (var stack in cargo)
        {
            result += stack.Pop();
        }

        return result;
    }

    private static async Task<Stack<char>[]> ReadCargo(IInputReader reader)
    {
        var stackCount = 0;
        var cargo = new Stack<string>();
        while (true)
        {
            var line = await reader.ReadLineAsync();
            if (line == string.Empty)
            {
                break;
            }

            if (line.First(x => x != ' ') != '[')
            {
                stackCount = (int)char.GetNumericValue(line.TrimEnd().Last());
                continue;
            }

            cargo.Push(line);
        }

        var stacks = EnumerableExtensions.Range(() => new Stack<char>(), stackCount).ToArray();
        while (cargo.Count > 0)
        {
            var line = cargo.Pop();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '[')
                {
                    stacks[i / 4].Push(line[i + 1]);
                }
            }
        }

        return stacks;
    }
}
