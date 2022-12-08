namespace AOC2022.Challenges.Challenge08;

public class Challenge08 : IChallenge
{
    public async Task<string> Part1(InputReader reader)
    {
        var trees = new List<List<int>>();
        var visibility = new List<List<bool>>();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var row = line.Select(x => (int)char.GetNumericValue(x)).ToList();
            visibility.Add(Enumerable.Repeat(false, row.Count).ToList());
            trees.Add(row);
        }

        for (int i = 0; i < trees.Count; i++)
        {
            var leftHigh = -1;
            var row = trees[i];
            for (int j = 0; j < row.Count; j++)
            {
                if (row[j] > leftHigh)
                {
                    leftHigh = row[j];
                    visibility[i][j] = true;
                }
                else if (row.Skip(j + 1).All(x => x < row[j]))
                {
                    visibility[i][j] = true;
                }
            }
        }

        for (int i = 0; i < trees[0].Count; i++)
        {
            var topHigh = -1;
            for (int j = 0; j < trees.Count; j++)
            {
                if (trees[j][i] > topHigh)
                {
                    topHigh = trees[j][i];
                    visibility[j][i] = true;
                }
                else if (trees.Skip(j + 1).All(x => x[i] < trees[j][i]))
                {
                    visibility[j][i] = true;
                }
            }
        }

        return visibility.SelectMany(x => x).Count(x => x).ToString();
    }

    public async Task<string> Part2(InputReader reader)
    {
        var trees = new List<List<int>>();

        await foreach (var line in reader.ReadAllLinesAsync())
        {
            var row = line.Select(x => (int)char.GetNumericValue(x)).ToList();
            trees.Add(row);
        }

        var highScore = 0;

        for (int i = 0; i < trees.Count; i++)
        {
            for (int j = 0; j < trees[i].Count; j++)
            {
                var left = 0;
                for (int k = j - 1; k >= 0; k--)
                {
                    left += 1;
                    if (trees[i][k] >= trees[i][j])
                    {
                        break;
                    }
                }

                var right = 0;
                for (int k = j + 1; k < trees[i].Count; k++)
                {
                    right += 1;
                    if (trees[i][k] >= trees[i][j])
                    {
                        break;
                    }
                }

                var top = 0;
                for (int k = i - 1; k >= 0; k--)
                {
                    top += 1;
                    if (trees[k][j] >= trees[i][j])
                    {
                        break;
                    }
                }

                var bottom = 0;
                for (int k = i + 1; k < trees.Count; k++)
                {
                    bottom += 1;
                    if (trees[k][j] >= trees[i][j])
                    {
                        break;
                    }
                }

                var socre = left * right * top * bottom;
                if (socre > highScore)
                {
                    highScore = socre;
                }
            }
        }

        return highScore.ToString();
    }
}
