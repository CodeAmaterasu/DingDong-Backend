using DingDong.Backend.Communication.Database.Repositories;
using System.Security.Cryptography;
using System.Text;

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
        /// Generates a Hashed-Key with the firstname and lastname
        /// of the user
        /// </summary>
        /// <param name="firstname">Firstname of the user</param>
        /// <param name="lastname">Lastname of the user</param>
        /// <returns>Hashed-Key</returns>
        public string GenerateHashedKey(string firstname, string lastname)
        {
            // Create source-string
            string source = firstname + lastname;

            // Used algorithm for hashing
            using SHA256 sha256Hash = SHA256.Create();

            // Hashes input
            return GetHash(sha256Hash, source);
        }

        /// <summary>
        /// Hashes a input with the given Hash-Algorithm
        /// </summary>
        /// <param name="hashAlgorithm">Used algorithm to hash</param>
        /// <param name="input">Source-Data to hash</param>
        /// <returns>Hashed string of the source-Data/returns>
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            var sBuilder = new StringBuilder();

            // Hash input and convert into byte-array
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convert hashed-input into Hex
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Returns Hashed-Hex-input as a string
            return sBuilder.ToString();
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
            var task = _userRepository.Add(user);
            return task.Result;
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
                var addr = new System.Net.Mail.MailAddress(email);

                // Check if email is already in use
                return _userRepository.FindByEmail(email) == null;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Accesses database and tries to find a user based on the hashed-key
        /// </summary>
        /// <param name="hashedKey">Key to search for</param>
        /// <returns>Indicates whether a user was successfully found our not</returns>
        public bool FindUserByHashedKey(string hashedKey)
        {
            if (string.IsNullOrEmpty(hashedKey)) return false;

            var task= _userRepository.FindByKey(hashedKey);

            return task.Result != null;
        }
    }
}
