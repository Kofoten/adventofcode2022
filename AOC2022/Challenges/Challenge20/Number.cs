namespace AOC2022.Challenges.Challenge20;

public class Number
{
    public Number Previous { get; set; }
    public Number Next { get; set; }
    public long Value { get; set; }

    public Number(Number previous, Number next, long value)
    {
        Previous = previous;
        Next = next;
        Value = value;
    }

    public Number()
    {
        Previous = this;
        Next = this;
        Value = 0L;
    }

    public Number Forward(long count)
    {
        var item = this;
        for (int i = 0; i < count; i++)
        {
            item = item.Next;
        }
        return item;
    }

    public Number Reverse(long count)
    {
        var item = this;
        for (int i = 0; i < count; i++)
        {
            item = item.Previous;
        }
        return item;
    }

    public void Remove()
    {
        Previous.Next = Next;
        Next.Previous = Previous;
    }

    public void Insert(Number after, Number before)
    {
        after.Next = this;
        before.Previous = this;
        Previous = after;
        Next = before;
    }
}
