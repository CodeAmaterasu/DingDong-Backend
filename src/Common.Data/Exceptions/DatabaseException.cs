using System;

namespace DingDong.Backend.Common.Data.Exceptions
{
    /// <summary>
    /// <see cref="Exception"/> related to Database-errors
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// Type of the exception
        /// </summary>
        public DatabaseExceptionType DatabaseExceptionType { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseExceptionType">Type of the exception</param>
        /// <param name="message">Message of the exception</param>
        public DatabaseException(DatabaseExceptionType databaseExceptionType, string message) : base(message)
        {
            DatabaseExceptionType = databaseExceptionType;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseExceptionType">Type of the exception</param>
        /// <param name="message">Message of the exception</param>
        /// <param name="innerException">Inner-Exception</param>
        public DatabaseException(DatabaseExceptionType databaseExceptionType, string message, Exception innerException) : base(message, innerException)
        {
            DatabaseExceptionType = databaseExceptionType;
        }
    }

    /// <summary>
    /// Different types of the <see cref="DatabaseException"/>
    /// </summary>
    public enum DatabaseExceptionType
    {
        DatabaseConnectionFailed,
        AddFailed,
        UpdateFailed,
        GetFailed,
        DeleteFailed
    }
}
