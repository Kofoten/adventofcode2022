namespace AOC2022.Challenges.Challenge01;

public class Challenge01 : IChallenge
{
    public async Task<int> Part1(InputReader reader) => await GetElfCalorieCounts(reader).MaxAsync();

    public async Task<int> Part2(InputReader reader) => await GetElfCalorieCounts(reader).OrderByDescending(x => x).Take(3).SumAsync();

    private static async IAsyncEnumerable<int> GetElfCalorieCounts(InputReader reader)
    {
        var calorieCount = 0;
        
        await foreach (var line in reader.ReadAllLinesAsync())
        { 
            if (string.IsNullOrEmpty(line))
            {
                yield return calorieCount;
                calorieCount = 0;
            }
            else if (int.TryParse(line, out var calories))
            {
                calorieCount += calories;
            }
            else
            {
                throw new InvalidDataException($"Input contains invalid data.");
            }
        }

        yield return calorieCount;
    }
}
