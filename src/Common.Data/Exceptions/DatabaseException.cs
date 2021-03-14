using System;

namespace DingDong.Backend.Common.Data.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseExceptionType DatabaseExceptionType { get; set; }

        public DatabaseException(DatabaseExceptionType databaseExceptionType, string message) : base(message)
        {
            DatabaseExceptionType = databaseExceptionType;
        }

        public DatabaseException(DatabaseExceptionType databaseExceptionType, string message, Exception innerException) : base(message, innerException)
        {
            DatabaseExceptionType = databaseExceptionType;
        }
    }

    public enum DatabaseExceptionType
    {
        DatabaseConnectionFailed,
        AddFailed,
        UpdateFailed,
        GetFailed,
        DeleteFailed
    }
}
