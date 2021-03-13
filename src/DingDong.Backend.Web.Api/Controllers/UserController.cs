using DingDong.Backend.Common.Data;
using Microsoft.AspNetCore.Mvc;
using DingDong.Backend.Web.Api.Util;

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

            if (!userValide)
            {
                return HttpCode.Unauthorized.GetStatusCodeResult();
            }
            user.HashedKey = _userManager.GenerateHashedKey(user.Firstname, user.Lastname);

            if (_userManager.AddUser(user))
            {
                return HttpCode.OK.GetStatusCodeResult();
            }
            return HttpCode.InternalServerError.GetStatusCodeResult();
        }

        [HttpGet]
        public IActionResult Login(string hashedKey)
        {
            if (_userManager.ExistHashedKey(hashedKey))
            {
                return HttpCode.OK.GetStatusCodeResult();
            }
            return HttpCode.Unauthorized.GetStatusCodeResult();
        }
    }
}
