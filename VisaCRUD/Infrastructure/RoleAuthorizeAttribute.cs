using System;
using System.Web;
using System.Web.Mvc;

namespace VisaCRUD.Infrastructure
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            String login = httpContext.User.Identity.Name;



            return base.AuthorizeCore(httpContext);
        }
    }
}
