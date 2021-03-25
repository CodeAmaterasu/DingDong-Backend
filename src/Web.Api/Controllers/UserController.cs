using DingDong.Backend.Common.Data;
using Microsoft.AspNetCore.Mvc;
using DingDong.Backend.Web.Api.Util;

namespace DingDong.Backend.Web.Api.Controllers
{
    /// <summary>
    /// Controller for <see cref="User"/> based API-calls
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        // Manager for the user, used for registering and finding a user
        private readonly UserManager _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserController()
        {
            _userManager = new UserManager();
        }

        /// <summary>
        /// Registers a new <see cref="User"/> to the database
        /// </summary>
        /// <param name="user"><see cref="User"/> to add</param>
        /// <returns>Corresponding HTTP-Code indicating whether the request was successful or not</returns>
        [HttpPost]
        public IActionResult Register(User user)
        {
            // Checks for empty string
            var userValide = _userManager.Validate(user);

            // Returns Bad-Request-Code when important data is null
            if (!userValide) return HttpCode.BadRequest.GetStatusCodeResult();

            // Generates a hashed string based on the firstname and lastname
            user.HashedKey = _userManager.GenerateHashedKey(user.Firstname, user.Lastname, user.Email);

            // Returns OK-Code when the user was successfully added
            if (_userManager.AddUser(user)) return HttpCode.OK.GetStatusCodeResult();

            // Returns a Internal-Server-Error-Code when the user was not added successfully
            else return HttpCode.InternalServerError.GetStatusCodeResult();
        }

        /// <summary>
        /// Tries to find a database-entry with the given hashed-key
        /// </summary>
        /// <param name="hashedKey">Key to find</param>
        /// <returns>Corresponding HTTP-Code indicating whether the request was successful or not</returns>
        [HttpGet]
        public IActionResult Login(string hashedKey)
        {
            // Returns OK-Code when a Hashed-Key was found in the database
            if (_userManager.ExistHashedKey(hashedKey)) return HttpCode.OK.GetStatusCodeResult();

            // Returns a Unauthorized-Code when the hashed string was not found
            else return HttpCode.Unauthorized.GetStatusCodeResult();
        }


        [HttpPost]
        [Route("sign")]
        public IActionResult Sign()
        {
            var hashedKey = _userManager.SignOldestUnsigned();
            var returnValue = new { hashedKey = hashedKey };

            if (!string.IsNullOrEmpty(hashedKey)) return HttpCode.OK.GetObjectResult(returnValue);
            else return HttpCode.InternalServerError.GetObjectResult(returnValue);
        }

        [HttpPost]
        [Route("unsign")]
        public IActionResult Unsign()
        {
            var isUnsigned = _userManager.UnsignNewsetSigned();
            var returnValue = new { isUnsigned = isUnsigned };

            if (isUnsigned) return HttpCode.OK.GetObjectResult(returnValue);
            else return HttpCode.InternalServerError.GetObjectResult(returnValue);
        }
    }
}
