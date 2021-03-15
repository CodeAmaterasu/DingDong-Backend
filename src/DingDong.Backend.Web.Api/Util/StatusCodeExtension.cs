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
        InternalServerError = 502
    }

    /// <summary>
    /// Static-Extensions for HTTP-Codes
    /// </summary>
    public static class Extensions
    {
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
