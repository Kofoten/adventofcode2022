using System.Numerics;

namespace AOC2022.Challenges.Challenge11;

public class Monkey
{
    private readonly int ifTrue;
    private readonly int ifFalse;

    private readonly Func<long, long> operation;

    public int Id { get; private init; }
    public int TestValue { get; private init; }
    public Queue<long> Items { get; private init; }

    public long Inspections { get; private set; }

    public Monkey(int id, Queue<long> items, Func<long, long> operation, int testValue, int ifTrue, int ifFalse)
    {
        Id = id;
        TestValue = testValue;
        Items = items;
        this.operation = operation;
        this.ifTrue = ifTrue;
        this.ifFalse = ifFalse;
    }

    public long Inspect(long value)
    {
        Inspections++;
        return operation(value);
    }

    public int ThrowItemTo(long value) => value % TestValue == 0 ? ifTrue : ifFalse;
}
