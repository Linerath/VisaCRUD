using System;
using VisaCRUD.DAL.Entities;

namespace VisaCRUD.DAL.Interfaces
{
    public interface IUsersRepository
    {
        User AddUser(User user);
        bool TryDeleteUser(User user);
        User GetUserByLogin(String login);
    }
}
