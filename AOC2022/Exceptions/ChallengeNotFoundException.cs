namespace AOC2022.Exceptions;

public class ChallengeNotFoundException : Exception
{
    public ChallengeNotFoundException(int id) : base($"A challenge for id {id} could not be found.") { }
}
