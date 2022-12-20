using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AOC2022.Challenges.Challenge20;

public class Challenge20 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        //var numbers = await ReadN2s(reader).ToListAsync();


        //var numbers = await ReadN2s(reader).ToListAsync();
        //var processed = 0;

        //while (processed < numbers.Count)
        //{

        //}


        var result = await Decrypt(reader, 1, 1);
        return result.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        var result = await Decrypt(reader, 10, 811589153);
        return result.ToString();
    }

    private static async Task<long> Decrypt(IInputReader reader, int rounds, long decryptionKey)
    {
        var numbers = await ReadNumbers(reader, decryptionKey).ToListAsync();
        var x = numbers.Count - 1;

        for (int i = 0; i < rounds; i++)
        {
            foreach (var number in numbers)
            {
                var absolute = Math.Abs(number.Value);
                var s = number.Value % x;
                var k = number.Value % numbers.Count;
                var modulated = ((number.Value % numbers.Count) + numbers.Count) % numbers.Count;
                var correction = 0L; // ((absolute - modulated) / numbers.Count) % numbers.Count;
                var steps = modulated + correction;

                if (steps == 0)
                {
                    continue;
                }

                var target = number;
                for (int j = 0; j < steps; j++)
                {
                    target = target.Next;
                }

                number.Remove();

                if (number.Value < 0)
                {
                    //var target = number.Reverse(steps);
                    var alt = number.Reverse(absolute);
                    number.Insert(alt.Previous, alt);
                }
                else
                {
                    //var target = number.Forward(steps);
                    var alt = number.Forward(absolute);
                    number.Insert(alt, alt.Next);
                }
            }
        }

        var result = 0L;
        var zero = numbers.Single(n => n.Value == 0);
        for (int i = 0; i < 3; i++)
        {
            zero = zero.Forward(1000);
            result += zero.Value;
        }

        return result;
    }

    private static async IAsyncEnumerable<Number> ReadNumbers(IInputReader reader, long decryptionKey)
    {
        var start = new Number();
        var last = start;
        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var value = long.Parse(line) * decryptionKey;
            var current = new Number(last, start, value);
            last.Next = current;
            last = current;
            yield return current;
        }

        var first = start.Next;
        first.Previous = last;
        last.Next = first;
    }

    private static async IAsyncEnumerable<long> ReadN2s(IInputReader reader, long decryptionKey = 1)
    {
        await foreach(var line in reader.ReadAllLinesAsync())
        {
            yield return long.Parse(line);
        }
    }

    public static void Mix(LinkedList<MixNode> input)
    {
        var n = input.Count - 1;
        //Console.WriteLine("Initial arrangement:");
        //Console.WriteLine(string.Join(", ", input.Select(i => i.ToString())));
        // O(n^2) = n * O(n) = iterating over O(n) mixing loop
        for (var i = 0; i < input.Count; i++)
        {
            // O(n) = finding the node that is next to mix
            var current = Nodes(input).First(node => node.Value.ShiftOrder == i);
            var shift = ((current.Value.Shift % n) + n) % n;
            if (shift == 0)
            {
                //Console.WriteLine("\n0 does not move:");
                continue;
            }
            // O(n) = shifting by at most n places in list
            var pointer = current;
            for (int j = 0; j < shift; j++)
                // O(1) finding the circular next pointer
                pointer = pointer.Next ?? input.First ?? throw new ArgumentException("Empty sequence!");
            // O(1) linked list insertion
            var inserted = input.AddAfter(pointer, current.Value);
            // O(1) linked list removal
            input.Remove(current);
            //Console.WriteLine();
            //Console.WriteLine($"{i.Value} moves between {pointer.Value} and {(inserted.Next ?? input.First ?? throw new ArgumentException("Empty sequence!")).Value}:");
            //Console.WriteLine(string.Join(", ", input.Select(i => i.ToString())));
        }
    }

    public static IEnumerable<LinkedListNode<T>> Nodes<T>(LinkedList<T> list)
    {
        for (var i = list.First; i is not null; i = i.Next)
            yield return i;
    }

    public record struct MixNode(long Shift, int ShiftOrder)
    {
        public override string ToString() => $"{Shift}";
    }
}
