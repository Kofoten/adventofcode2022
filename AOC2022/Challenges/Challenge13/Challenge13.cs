namespace AOC2022.Challenges.Challenge13;

public class Challenge13 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var result = 0;

        for (int i = 1; reader.CanRead; i++)
        {
            var leftStr = await reader.ReadLineAsync();
            var rightStr = await reader.ReadLineAsync();
            if (reader.CanRead)
            {
                await reader.ReadLineAsync();
            }

            var left = Packet.Parse(leftStr);
            var right = Packet.Parse(rightStr);

            if (left <= right)
            {
                result += i;
            }

            Console.WriteLine(left.ToString() + right.ToString());
            //var leftLower = false;

            //for (int j = 0; ; j++)
            //{
            //    if (j == left.Length && j < right.Length)
            //    {
            //        result += i;
            //        leftLower = true;
            //        break;
            //    }
            //    else if (j == right.Length)
            //    {
            //        break;
            //    }
            //    var cl = left[j];
            //    var cr = right[j];
            //    if (cl == cr)
            //    {
            //        continue;
            //    }

            //    if (cl == '[')
            //    {
            //        if (cr == ']')
            //        {
            //            break;
            //        }

            //        var x = right.Remove(j, 1);
            //        right = x.Insert(j, $"[{cr}]");
            //        continue;
            //    }

            //    if (cr == '[')
            //    {
            //        if (cl == ']')
            //        {
            //            leftLower = true;
            //            result += i;
            //            break;
            //        }

            //        var x = left.Remove(j, 1);
            //        left = x.Insert(j, $"[{cl}]");
            //        continue;
            //    }

            //    if (cl == ']')
            //    {
            //        leftLower = true;
            //        result += i;
            //        break;
            //    }
            //    else if (cr == ']')
            //    {
            //        break;
            //    }

            //    var lv = (int)char.GetNumericValue(cl);
            //    var rv = (int)char.GetNumericValue(cr);
            //    if (lv > rv)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        leftLower = true;
            //        result += i;
            //        break;
            //    }
            //}

            //Console.WriteLine(leftLower);

            //if (reader.CanRead)
            //{
            //    var empty = await reader.ReadLineAsync();
            //    if (!string.IsNullOrEmpty(empty))
            //    {
            //        throw new InvalidDataException("Input contains invalid data");
            //    }
            //}
        }

        return result.ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        throw new PartNotImplementedException(2);
    }
}
