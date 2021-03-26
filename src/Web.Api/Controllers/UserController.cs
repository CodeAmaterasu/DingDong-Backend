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

            // Returns OK-Code when the user was successfully added
            if (_userManager.AddUser(user)) return HttpCode.OK.GetStatusCodeResult();

            // Returns a Internal-Server-Error-Code when the user was not added successfully
            else return HttpCode.InternalServerError.GetStatusCodeResult();
        }

        /// <summary>
        /// Tries to find a database-entry with the given guid
        /// </summary>
        /// <param name="guid">Guid to find</param>
        /// <returns>Corresponding HTTP-Code indicating whether the request was successful or not</returns>
        [HttpGet]
        public IActionResult Login(string guid)
        {
            // Returns OK-Code when guid was found in the database
            if (_userManager.ExistGuid(guid)) return HttpCode.OK.GetStatusCodeResult();

            // Returns a Unauthorized-Code when guid was not found
            else return HttpCode.Unauthorized.GetStatusCodeResult();
        }

        /// <summary>
        /// Tries to sign an existing user in the database
        /// </summary>
        /// <returns>Corresponding HTTP-Code indicating whether the request was successful or not</returns>
        [HttpPost]
        [Route("sign")]
        public IActionResult Sign(Badge badge)
        {
            if (string.IsNullOrEmpty(badge.BadgeGuid)) return HttpCode.BadRequest.GetStatusCodeResult();
            var isAssigned = _userManager.AssignBadgeToUser(badge.BadgeGuid);

            if (isAssigned) return HttpCode.OK.GetStatusCodeResult();
            else return HttpCode.InternalServerError.GetStatusCodeResult();
        }

        /// <summary>
        /// Tries to delete an existing user from the database
        /// </summary>
        /// <param name="input">input to search for. Can be an email adress or a guid</param>
        /// <returns>Corresponding HTTP-Code indicating whether the request was successful or not</returns>
        [HttpDelete]
        public IActionResult Delete(string input)
        {
            var isRemoved = _userManager.Delete(input);
            var returnValue = new { IsRemoved = isRemoved };

            if (isRemoved) return HttpCode.OK.GetObjectResult(returnValue);
            else return HttpCode.InternalServerError.GetObjectResult(returnValue);
        }
    }
}
