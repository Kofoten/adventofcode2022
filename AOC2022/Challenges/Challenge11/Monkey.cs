using System.Numerics;

namespace AOC2022.Challenges.Challenge11;

public class Monkey
{
    private readonly int ifTrue;
    private readonly int ifFalse;

    private readonly Func<int, int> operation;

    public int Id { get; private init; }
    public int TestValue { get; private init; }
    public Queue<int> Items { get; private init; }

    public int Inspections { get; private set; }

    public Monkey(int id, Queue<int> items, Func<int, int> operation, int testValue, int ifTrue, int ifFalse)
    {
        Id = id;
        TestValue = testValue;
        Items = items;
        this.operation = operation;
        this.ifTrue = ifTrue;
        this.ifFalse = ifFalse;
    }

    public int Inspect(int value)
    {
        Inspections++;
        return operation(value);
    }

    public int ThrowItemTo(int value) => value % TestValue == 0 ? ifTrue : ifFalse;
}
