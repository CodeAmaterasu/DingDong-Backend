﻿using DingDong.Backend.Common.Data;
using DingDong.Backend.Common.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DingDong.Backend.Communication.Database.Repositories
{
    /// <summary>
    /// Connection to the <see cref="User"/>-Table in the database
    /// </summary>
    public class UserRepository
    {
        // Factory for creating instances of the class which connects to the database
        protected readonly DatabaseContextFactory DatabaseContextFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserRepository()
        {
            DatabaseContextFactory = new DatabaseContextFactory();
        }

        /// <summary>
        /// Tries to add a <see cref="User"/> into the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Add(User user)
        {
            try
            {
                // Connects to the database
                await using var context = DatabaseContextFactory.CreateDbContext();

                // Adds the user to the database
                var result = context.Set<User>().Add(user);

                // Saves the changes
                await context.SaveChangesAsync();

                return result != null;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.AddFailed, "Exception with adding User in Database", e);
            }
        }

        /// <summary>
        /// Tries to find a user in the database based on the hashed key
        /// </summary>
        /// <param name="hashedKey">Key to search for</param>
        /// <returns>Entire <see cref="User"/> object which corresponds with the given hashed key</returns>
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

        /// <summary>
        /// Tries to find a user in the database based on the email
        /// </summary>
        /// <param name="email">Email to search for</param>
        /// <returns>Entire <see cref="User"/> object which corresponds with the given email</returns>
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


        /// <summary>
        /// Tries to sign the oldest user which isn't signed.
        /// </summary>
        /// <returns>Hashed-Key of this user</returns>
        public async Task<string> SignOldestUnsigned()
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                var user = await context.Set<User>().FirstOrDefaultAsync(u => u.IsSigned == false);

                user.IsSigned = true;
                context.Set<User>().Update(user);

                await context.SaveChangesAsync();

                return user.HashedKey;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.GetFailed, "Exception with signing oldest unsigned user in the Database", e);
            }
        }

        /// <summary>
        /// Tries to unsign the newest user which is signed.
        /// </summary>
        /// <returns>Indicates whether a user got unsigned or not</returns>
        public async Task<bool> UnsignNewestSigned()
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                var user = await context.Set<User>().LastOrDefaultAsync(u => u.IsSigned == true);

                user.IsSigned = false;
                context.Set<User>().Update(user);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.GetFailed, "Exception with unsigning newest signed user in the Database", e);
            }
        }

        /// <summary>
        /// Tries to delete an existing user based on the given hashed-key
        /// </summary>
        /// <param name="hashedKey">Key to search for</param>
        /// <returns>Indicates whether the user got successfully removed or not</returns>
        public async Task<bool> DeleteWithHashedKey(string hashedKey)
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                var user = await context.Set<User>().LastOrDefaultAsync(u => u.HashedKey == hashedKey);

                context.Set<User>().Remove(user);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.GetFailed, "Exception with removing user in database based on hashed-key", e);
            }
        }

        /// <summary>
        /// Tries to delete an existing user based on the given email
        /// </summary>
        /// <param name="email">Email to search for</param>
        /// <returns>whether the user got successfully removed or not</returns>
        public async Task<bool> DeleteWithEmail(string email)
        {
            try
            {
                await using var context = DatabaseContextFactory.CreateDbContext();
                var user = await context.Set<User>().LastOrDefaultAsync(u => u.Email == email);

                context.Set<User>().Remove(user);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new DatabaseException(DatabaseExceptionType.GetFailed, "Exception with removing user in database based on email", e);
            }
        }
    }
}
