namespace AOC2022.Challenges.Challenge12;

public class Node
{
    public int Height { get; private init; }
    public int StepsTo { get; set; }

    public Node(int height) : this(height, int.MaxValue) { }

    public Node(int height, int stepsTo)
    {
        Height = height;
        StepsTo = stepsTo;
    }
}
