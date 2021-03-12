using System.Security.Cryptography;
using System.Text;

namespace DingDong.Backend.Business.Hash
{
    /// <summary>
    /// Used for implementing any type of hashing
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Hasher()
        {
        }

        /// <summary>
        /// Hashes a input with the given Hash-Algorithm
        /// </summary>
        /// <param name="hashAlgorithm">Used algorithm to hash</param>
        /// <param name="input">Source-Data to hash</param>
        /// <returns>Hashed string of the source-Data/returns>
        public string GetHash(HashAlgorithm hashAlgorithm, string input)
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
    }
}
