namespace AOC2022.IO;

public interface IInputReader
{
    bool CanRead { get; }
    ValueTask<string> ReadLineAsync();
    IAsyncEnumerable<string> ReadAllLinesAsync();
    void Dispose();
}
