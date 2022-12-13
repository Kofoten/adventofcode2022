namespace AOC2022.Challenges.Challenge13;

public static class InputReaderExtensions
{
    public static async ValueTask<PacketReaderPair> ReadPacketReaderPairAsync(this IInputReader reader)
    {
        var leftString = await reader.ReadLineAsync();
        var rightString = await reader.ReadLineAsync();

        var left = new PacketReader(leftString);
        var right = new PacketReader(rightString);

        if (reader.CanRead)
        {
            await reader.ReadLineAsync();
        }

        return new PacketReaderPair(left, right);
    }
}
