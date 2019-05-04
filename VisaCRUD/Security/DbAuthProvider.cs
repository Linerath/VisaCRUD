using System;
using System.Web.Security;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;
using VisaCRUD.Infrastructure.Interfaces;
using VisaCRUD.Models.ViewModels;

namespace VisaCRUD.Security
{
    public class DbAuthProvider : IAuthProvider
    {
        private IUsersRepository usersRepository;

        public DbAuthProvider(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public UserDto Authenticate(String login, String password)
        {
            if (String.IsNullOrWhiteSpace(login) || String.IsNullOrWhiteSpace(password))
                return null;

            User userDb = usersRepository.GetUserByLogin(login);

            if (userDb == null)
                return null;

            UserDto user = new UserDto
            {
                Login = userDb.Login,
                Roles = userDb.Roles,
            };

            String hash = CryptoService.GetHashCode(password);

            if (String.Equals(hash, userDb.Password, StringComparison.Ordinal))
            {
                FormsAuthentication.SetAuthCookie(login, false);
                return user;
            }

            return null;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}