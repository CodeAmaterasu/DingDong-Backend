using System.ComponentModel.DataAnnotations;

namespace DingDong.Backend.Common.Data
{
    /// <summary>
    /// Stores information about a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID of the user
        /// </summary>
        [Key]
        public int Id { get; set; }

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
        /// Used for telling if a user is assigned to a user not
        /// </summary>
        public bool IsSigned { get; set; }

        /// <summary>
        /// GUID of the assigned Badge
        /// </summary>
        public string Guid { get; set; }
    }
}
