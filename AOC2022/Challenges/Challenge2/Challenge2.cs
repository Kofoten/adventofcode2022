namespace AOC2022.Challenges.Challenge2
{
    public class Challenge2 : IChallenge
    {
        public async Task<int> Part1(Stream input)
        {
            var result = 0;
            await foreach (var (opponent, value) in GetRoundResults(input))
            {
                var me = value switch
                {
                    'X' => Shape.Rock,
                    'Y' => Shape.Paper,
                    'Z' => Shape.Scissors,
                    _ => throw new InvalidOperationException($"Input contains invalid data."),
                };

                result += (1 + RockPaperScissors(opponent, me)) * 3 + (int)me;
            }

            return result;
        }

        public async Task<int> Part2(Stream input)
        {
            var result = 0;
            await foreach (var (opponent, value) in GetRoundResults(input))
            {
                var me = value switch
                {
                    'X' => opponent.WinsOver(),
                    'Y' => opponent,
                    'Z' => opponent.LosesTo(),
                    _ => throw new InvalidOperationException($"Input contains invalid data."),
                };

                result += (1 + RockPaperScissors(opponent, me)) * 3 + (int)me;
            }

            return result;
        }

        public static async IAsyncEnumerable<KeyValuePair<Shape, char>> GetRoundResults(Stream input)
        {
            using var reader = new StreamReader(input);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                {
                    throw new InvalidOperationException($"Input contains invalid data.");
                }

                var parts = line.Split(' ');
                if (parts.Length != 2)
                {
                    throw new InvalidOperationException($"Input contains invalid data.");
                }

                var opponent = parts[0] switch
                {
                    "A" => Shape.Rock,
                    "B" => Shape.Paper,
                    "C" => Shape.Scissors,
                    _ => throw new InvalidOperationException($"Input contains invalid data."),
                };

                yield return new KeyValuePair<Shape, char>(opponent, parts[1][0]);
            }
        }

        private static int RockPaperScissors(Shape first, Shape second)
        {
            if (first == Shape.Unknown)
            {
                throw new ArgumentException("Shape must be known", nameof(first));
            }

            if (second == Shape.Unknown)
            {
                throw new ArgumentException("Shape must be known", nameof(first));
            }

            if (first != second)
            {
                if (first == Shape.Rock)
                {
                    return second == Shape.Paper ? 1 : -1;
                }

                if (first == Shape.Paper)
                {
                    return second == Shape.Scissors ? 1 : -1;
                }

                if (first == Shape.Scissors)
                {
                    return second == Shape.Rock ? 1 : -1;
                }
            }

            return 0;
        }
    }
}
