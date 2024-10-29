/// <summary>
/// Represents errors that occur during data access operations.
/// </summary>
public class DataAccessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataAccessException"/> class.
    /// </summary>
    public DataAccessException() { }


    /// <summary>
    /// Initializes a new instance of the <see cref="DataAccessException"/> class
    /// with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DataAccessException(string message)
        : base(message) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="DataAccessException"/> class
    /// with a specified error message and a reference to the inner exception
    /// that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public DataAccessException(string message, Exception innerException)
        : base(message, innerException) { }
}