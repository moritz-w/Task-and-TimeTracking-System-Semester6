using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskAndTimeTracking.Controller
{
    public class JwtAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine(context.HttpContext.User.ToString());
        }
    }
}