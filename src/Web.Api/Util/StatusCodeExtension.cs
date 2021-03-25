using Microsoft.AspNetCore.Mvc;

namespace DingDong.Backend.Web.Api.Util
{
    /// <summary>
    /// HTTP-Codes for API-Endpoints
    /// </summary>
    public enum HttpCode : int
    {
        OK = 200,
        BadRequest = 400,
        Unauthorized = 401,
        InternalServerError = 500
    }

    /// <summary>
    /// Static-Extensions for HTTP-Codes
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets the corresponding <see cref="ObjectResult"/>
        /// </summary>
        /// <param name="statusCode">HTTP-Status-Code</param>
        /// <param name="value">The value to store in the result</param>
        /// <returns></returns>
        public static ObjectResult GetObjectResult(this HttpCode statusCode, object value)
        {
            return new ObjectResult(value)
            {
                 StatusCode = (int) statusCode
            };
        }

        /// <summary>
        /// Gets the corresponding <see cref="StatusCodeResult"/>
        /// </summary>
        /// <param name="statusCode">HTTP-Status-Code</param>
        /// <returns></returns>
        public static StatusCodeResult GetStatusCodeResult(this HttpCode statusCode)
        {
            return new StatusCodeResult((int)statusCode);
        }
    }
}
