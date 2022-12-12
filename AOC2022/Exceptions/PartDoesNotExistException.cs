namespace AOC2022.Exceptions;

public class PartDoesNotExistException : Exception
{
    public PartDoesNotExistException(int part) : base($"There exists no part {part}.") { }
}
