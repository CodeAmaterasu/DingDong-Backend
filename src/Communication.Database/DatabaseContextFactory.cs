using DingDong.Backend.Common.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace DingDong.Backend.Communication.Database
{
    /// <summary>
    /// Factory for creating new <see cref="DatabaseContext"/>
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// Creates a new instanc of <see cref="DatabaseContext"/> with an assigned connection string for MySQL
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public DatabaseContext CreateDbContext(string[] args = null)
        {
            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>();
                options.UseMySQL("server=localhost;database=dingdong;user=root;password=root");

                return new DatabaseContext(options.Options);
            }
            catch (Exception e)
            {

                throw new DatabaseException(DatabaseExceptionType.DatabaseConnectionFailed, "Failed to connect to the database", e);
            }
        }
    }
}
