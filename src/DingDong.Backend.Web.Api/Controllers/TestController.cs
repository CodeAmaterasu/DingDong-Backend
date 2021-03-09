using Microsoft.AspNetCore.Mvc;

namespace DingDong.Backend.Web.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult TestConnection()
        {
            return Ok(new { message = "Ich esse gerne Pastelfarben!" });
        }
    }
}
