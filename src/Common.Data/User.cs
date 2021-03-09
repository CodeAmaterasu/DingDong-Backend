using System.ComponentModel.DataAnnotations;

namespace DingDong.Backend.Common.Data
{
    /// <summary>
    /// Stores information about a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Firstname of the user
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Lastname of the user
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Secret hashed key, used for authenticating the user in the login-terminal
        /// </summary>
        [Key]
        public string HashedKey { get; set; }
    }
}
