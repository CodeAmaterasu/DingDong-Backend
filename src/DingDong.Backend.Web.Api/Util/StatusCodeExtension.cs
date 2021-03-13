using Microsoft.AspNetCore.Mvc;

namespace DingDong.Backend.Web.Api.Util
{
    public enum HttpCode : int
    {
        OK = 200,
        BadRequest = 400,
        Unauthorized = 401,
        InternalServerError = 502
    }

    public static class Extensions
    {
        public static StatusCodeResult GetStatusCodeResult(this HttpCode statusCode)
        {
            return new StatusCodeResult((int)statusCode);
        }
    }
}
