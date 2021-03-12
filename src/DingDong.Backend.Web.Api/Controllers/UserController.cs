using DingDong.Backend.Common.Data;
using Microsoft.AspNetCore.Mvc;

namespace DingDong.Backend.Web.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;

        public UserController()
        {
            _userManager = new UserManager();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var userValide = _userManager.Validate(user);

            if (!userValide) return StatusCode(403);

            user.HashedKey = _userManager.GenerateHashedKey(user.Firstname, user.Lastname);

            if (_userManager.AddUser(user))
            {
                return StatusCode(200);
            }
            return StatusCode(500);
        }

        [HttpGet]
        public IActionResult Login(string hashedKey)
        {
            if (_userManager.FindUserByHashedKey(hashedKey))
            {
                return StatusCode(200);
            }
            return StatusCode(403);
        }
    }
}
