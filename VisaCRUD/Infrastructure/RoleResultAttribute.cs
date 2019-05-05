using System;
using System.Web.Mvc;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.Models.ViewModels;

namespace VisaCRUD.Infrastructure
{
    public class RoleResultAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                String username = filterContext.HttpContext.User.Identity.Name;
                User userDb = usersRepository.GetUserByLogin(username);

                if (userDb != null)
                {
                    UserDto user = new UserDto
                    {
                        Login = username,
                        Roles = userDb.Roles,
                    };

                    filterContext.Controller.ViewData.Add("User", user);
                    filterContext.Controller.ViewBag.User = user;
                }
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