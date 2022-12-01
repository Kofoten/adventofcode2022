using AOC2022.Exceptions;

namespace AOC2022.Challange
{
    public class Challange1 : IChallange
    {
        public async Task<int> Part1(Stream input) => await GetElfCalorieCounts(input).MaxAsync();

        public async Task<int> Part2(Stream input) => await GetElfCalorieCounts(input).OrderByDescending(x => x).Take(3).SumAsync();

        private static async IAsyncEnumerable<int> GetElfCalorieCounts(Stream input)
        {
            using var reader = new StreamReader(input);

            var calorieCount = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

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
                    throw new InvalidOperationException($"Input contains invalid data.");
                }
            }

            yield return calorieCount;
        }
    }
}
