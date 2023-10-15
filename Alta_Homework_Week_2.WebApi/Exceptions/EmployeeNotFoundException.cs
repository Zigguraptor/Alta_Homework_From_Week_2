namespace Alta_Homework_Week_2.WebApi.Exceptions;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException()
    {
    }

    public EmployeeNotFoundException(string? message) : base(message)
    {
    }

    public EmployeeNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
