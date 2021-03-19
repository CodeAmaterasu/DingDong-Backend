using Microsoft.AspNetCore.Mvc;

namespace DingDong.Backend.Web.Api.Controllers
{
    /// <summary>
    /// Controller for testing the API
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// API-Endpoint for testing Get-Requests
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestConnection()
        {
            return Ok(new { message = "Ich esse gerne Pastelfarben!" });
        }
    }
}
