namespace AOC2022.Exceptions;

public class PartNotImplementedException : Exception
{
    public PartNotImplementedException(int part) : base($"The part {part} is not implemented yet.") { }
}
