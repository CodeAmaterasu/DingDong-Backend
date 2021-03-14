using DingDong.Backend.Common.Data;
using DingDong.Backend.Common.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DingDong.Backend.Communication.Database.Repositories
{
    public class UserRepository
    {
        /// <summary>
        /// Factory for creating instances of the class which connects to the database
        /// </summary>
        protected readonly DatabaseContextFactory DatabaseContextFactory;

        public UserRepository()
        {
            DatabaseContextFactory = new DatabaseContextFactory();
        }

        public async Task<bool> Add(User user)
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                var result = context.Set<User>().Add(user);
                await context.SaveChangesAsync();

                return result != null;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.AddFailed, "Exception with adding User in Database", e);
            }
        }

        public async Task<User> FindByKey(string hashedKey)
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                var res = await context.User.FirstOrDefaultAsync(u => u.HashedKey == hashedKey);

                return res;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.GetFailed, "Exception with finding User in Database based on Hashed-Key", e);
            }
        }

        public async Task<User> FindByEmail(string email)
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                return await context.User.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.GetFailed, "Exception with finding User in Database based on Hashed-Key", e);
            }
        }
    }
}
