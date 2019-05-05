using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;

namespace VisaCRUD.Infrastructure
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            if (String.IsNullOrWhiteSpace(Roles))
                return true;

            String login = httpContext.User.Identity.Name;

            if (String.IsNullOrWhiteSpace(login))
                return false;

            String[] roles = Roles.Split(',');
            User user = usersRepository.GetUserByLogin(login);

            if (user == null || user.Roles == null)
                return false;

            foreach (var role in roles)
            {
                if (user.Roles.Any(x => String.Equals(x.Name, role, StringComparison.OrdinalIgnoreCase)))
                    return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Error" },
                    { "action", "Forbidden" },
                });
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private IUsersRepository usersRepository
        {
            get
            {
                return DependencyResolver.Current.GetService<IUsersRepository>();
            }
        }
    }
}
