using System.Reflection.PortableExecutable;

namespace AOC2022.Challenges.Challenge17;

public class Challenge17 : IChallenge
{
    public async Task<string> Part1(IInputReader reader)
    {
        var jetProvider = await JetProvider.Create(reader);
        return CalculateTowerHeight(jetProvider, 2022).ToString();
    }

    public async Task<string> Part2(IInputReader reader)
    {
        throw new PartNotImplementedException(2);
    }

    private static long CalculateTowerHeight(JetProvider jetProvider, long rockCount)
    {
        return 0L;
        //var spawner = new RockSpawner();
        //var resting = new HashSet<Point>();

        //var height = 0;
        //while (spawner.Spawns < rockCount)
        //{
        //    var rock = spawner.Spawn(height + 3);

        //    while (true)
        //    {
        //        var movement = jetProvider.NextMovment();
        //        rock.Position += movement;
        //        if (rock.LeftEdge < 0 || rock.RightEdge > 6)
        //        {
        //            rock.Position -= movement;
        //        }
        //        else if (rock.BottomEdge < height && resting.Any(r => r.Intersects(rock)))
        //        {
        //            rock.Position -= movement;
        //        }

        //        var drop = new Point(0, 1);
        //        rock.Position -= drop;
        //        if (rock.BottomEdge < 0 || resting.Any(r => r.Intersects(rock)))
        //        {
        //            rock.Position += drop;
        //            break;
        //        }
        //    }

        //    resting.Add(rock);
        //    var potentialHeight = 1 + rock.TopEdge;
        //    if (potentialHeight > height)
        //    {
        //        height = potentialHeight;
        //    }
        //}

        //return height.ToString();
    }
}
