using DingDong.Backend.Web.Api.Util;
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
        public IActionResult TestConnection()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var returnvalue = new { message = "Ich esse gerne Pastelfarben!" };
            return HttpCode.OK.GetObjectResult(returnvalue);
        }
    }
}
