namespace AOC2022.Challenges.Challenge17;

public class JetProvider
{
    private readonly string data;
    private int index;

    private JetProvider(string data)
    {
        this.data = data;
        index = -1;
    }

    public Point NextMovment()
    {
        index++;

        if (index == data.Length)
        {
            index = 0;
        }

        return data[index] switch
        {
            '<' => new Point(-1, 0),
            '>' => new Point(1, 0),
            _ => throw new InvalidDataException("Input contains invalid data"),
        };
    }

    public static async Task<JetProvider> Create(IInputReader reader)
    {
        var data = string.Empty;
        
        await foreach(var row in reader.ReadAllLinesAsync())
        {
            data += row;
        }

        return new JetProvider(data);
    }
}
