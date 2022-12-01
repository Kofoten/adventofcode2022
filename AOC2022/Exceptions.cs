namespace AOC2022.Exceptions;

public class ChallengeNotFoundException : Exception
{
    public ChallengeNotFoundException(int id) : base($"A challenge for id {id} could not be found.") { }
}

public class PartDoesNotExistException : Exception
{
    public PartDoesNotExistException(int part) : base($"There exists no part {part}.") { }
}

public class PartNotImplementedException : Exception
{
    public PartNotImplementedException(int part) : base($"The part {part} is not implemented yet.") { }
}
