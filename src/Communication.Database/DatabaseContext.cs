using DingDong.Backend.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace DingDong.Backend.Communication.Database
{
    /// <summary>
    /// Connection to the database
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Connection to the Users-Database-Table
        /// </summary>
        public DbSet<User> User { get; set; }
    }
}
