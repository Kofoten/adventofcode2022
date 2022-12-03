namespace AOC2022.IO
{
    public class InputReader : IDisposable
    {
        private readonly StreamReader reader;

        public InputReader(Stream stream)
        {
            reader = new StreamReader(stream);
        }

        public async ValueTask<string> ReadLineAsync()
        {
            var line = await reader.ReadLineAsync();
            if (line is null)
            {
                throw new InvalidDataException("Input is invalid");
            }

            return line;
        }

        public async IAsyncEnumerable<string> ReadAllLinesAsync()
        {
            while (!reader.EndOfStream)
            {
                yield return await ReadLineAsync();
            }
        }

        public void Dispose() => reader.Dispose();
    }
}
