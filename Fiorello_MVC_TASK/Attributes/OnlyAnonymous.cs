using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fiorello_MVC_TASK.Attributes
{
    public class OnlyAnonymous : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
