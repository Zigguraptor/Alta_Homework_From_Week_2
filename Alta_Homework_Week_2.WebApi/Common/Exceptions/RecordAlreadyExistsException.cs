namespace Alta_Homework_Week_2.WebApi.Common.Exceptions;

public class RecordAlreadyExistsException : Exception
{
    public RecordAlreadyExistsException()
    {
    }

    public RecordAlreadyExistsException(string? message) : base(message)
    {
    }

    public RecordAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
