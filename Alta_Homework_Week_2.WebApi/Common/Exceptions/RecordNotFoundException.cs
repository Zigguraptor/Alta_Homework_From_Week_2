namespace Alta_Homework_Week_2.WebApi.Common.Exceptions;

public class RecordNotFoundException : Exception
{
    public RecordNotFoundException()
    {
    }

    public RecordNotFoundException(string? message) : base(message)
    {
    }

    public RecordNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
