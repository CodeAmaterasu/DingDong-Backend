using DingDong.Backend.Communication.Database.Repositories;
using DingDong.Backend.Common.Data.Exceptions;

namespace DingDong.Backend.Common.Data
{
    /// <summary>
    /// Manager for Data related to <see cref="User"/>
    /// </summary>
    public class UserManager
    {
        // Repository for user which is connected to the database
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserManager()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Accesses database and tries to find a user based on the Guid
        /// </summary>
        /// <param name="guid">Guid to search for</param>
        /// <returns>Indicates whether a user was successfully found our not</returns>
        public bool ExistGuid(string guid)
        {
            try
            {
                var task = _userRepository.FindByGuid(guid);

                return task.Result != null;
            }
            catch (DatabaseException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the given user. Checks for empty strings and 
        /// validity of the email
        /// </summary>
        /// <param name="user">user to validate</param>
        /// <returns>Indicates whether the user has valid properties or not</returns>
        public bool Validate(User user)
        {
            if (string.IsNullOrEmpty(user.Firstname)) return false;
            if (string.IsNullOrEmpty(user.Lastname)) return false;
            if (!CheckEmail(user.Email)) return false;
            return true;
        }

        /// <summary>
        /// Adds the given user to the database
        /// </summary>
        /// <param name="user">User to add to the database</param>
        /// <returns>Indicates whether the given user got successfully added or not</returns>
        public bool AddUser(User user)
        {
            try
            {
                var task = _userRepository.Add(user);
                return task.Result;
            }
            catch (DatabaseException)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the given email for its validity and if 
        /// it is already in use
        /// </summary>
        /// <param name="email">email to check</param>
        /// <returns>Indicates whether the email is valid or not</returns>
        private bool CheckEmail(string email)
        {
            try
            {
                // Check if Email-Addrress is valid
                _ = new System.Net.Mail.MailAddress(email);

                // Check if email is already in use
                return _userRepository.FindByEmail(email) == null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Signs the oldest unsigned user in the database
        /// </summary>
        /// <returns>Guid of the user</returns>
        public bool AssignBadgeToUser(string guid)
        {
            try
            {
                return _userRepository.AddGuidToUnsigned(guid).Result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes an existing user from the database
        /// </summary>
        /// <param name="input">Input to search for. Can be email or Guid</param>
        /// <returns>Indicates whether the user got removed or not</returns>
        public bool Delete(string input)
        {
            try
            {
                if (CheckEmail(input)) return _userRepository.DeleteWithEmail(input).Result;
                else return _userRepository.DeleteWithGuid(input).Result;
            }
            catch
            {
                return false;
            }
        }
    }
}
