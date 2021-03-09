using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
            var options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseMySQL("server=localhost;database=dingdong;user=root;password=root");

            return new DatabaseContext(options.Options);
        }
    }
}
