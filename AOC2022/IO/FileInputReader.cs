namespace AOC2022.IO
{
    public class FileInputReader : IDisposable, IInputReader
    {
        private readonly StreamReader reader;

        private FileInputReader(StreamReader reader)
        {
            this.reader = reader;
        }

        public bool CanRead => !reader.EndOfStream;

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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            reader.Dispose();
        }

        public static FileInputReader Create(FileInfo file)
        {
            var stream = file.OpenRead();
            var reader = new StreamReader(stream);
            return new FileInputReader(reader);
        }
    }
}
