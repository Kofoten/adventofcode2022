namespace AOC2022.Challenges.Challenge13;

public static class InputReaderExtensions
{
    public static async ValueTask<PacketPair> ReadPacketReaderPairAsync(this IInputReader reader)
    {
        var left = await reader.ReadLineAsync();
        var right = await reader.ReadLineAsync();

        if (reader.CanRead)
        {
            await reader.ReadLineAsync();
        }

        return new(left, right);
    }
}
