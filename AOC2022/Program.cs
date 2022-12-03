namespace AOC2022;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        if (!RuntimeOptions.TryParse(args, out var options, out var reason))
        {
            Console.WriteLine($"ERROR: {reason}");
            return 1;
        }

        var runtime = new Runtime(options);

        try
        {
            var result = await runtime.Run();
            if (result.IsSuccess(out var answer, out var error))
            {
                Console.WriteLine(answer);
                return 0;
            }

            Console.WriteLine($"FATAL: {error}");
            return result.Code;
        }
        catch (Exception e)
        {
            Console.WriteLine($"CRIT: {e.Message}");
            Console.WriteLine(e.ToString());
            return 42;
        }
    }
}