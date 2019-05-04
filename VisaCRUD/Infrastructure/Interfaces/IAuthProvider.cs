using System;
using VisaCRUD.Models.ViewModels;

namespace VisaCRUD.Infrastructure.Interfaces
{
    public interface IAuthProvider
    {
        UserDto Authenticate(String login, String password);
        void Logout();
    }
}
