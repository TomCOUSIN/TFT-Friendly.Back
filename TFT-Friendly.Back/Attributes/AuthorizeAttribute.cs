using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TFT_Friendly.Back.Attributes
{
    /// <summary>
    /// AuthorizeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Check if the user as the right authorization to perform the request
        /// </summary>
        /// <param name="context">The context of the request</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var userId = (string) context.HttpContext.Items["UserId"];

                if (userId.Length <= 0)
                {
                    context.Result = new JsonResult(new {message = "Unauthorized"})
                        {StatusCode = StatusCodes.Status401Unauthorized};
                }
            }
            catch
            {
                context.Result = new JsonResult(new {message = "Unauthorized"})
                    {StatusCode = StatusCodes.Status401Unauthorized};
            }
        }
    }
}