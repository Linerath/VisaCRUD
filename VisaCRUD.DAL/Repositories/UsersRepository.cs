using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisaCRUD.DAL.Entities;
using VisaCRUD.DAL.Interfaces;

namespace VisaCRUD.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private String connectionString;

        public UsersRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public User AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByLogin(String login)
        {
            string sql = "SELECT t1.*, t2.* FROM AppUsers t1 "
                + "LEFT JOIN AppUsersRoles t1t2 ON t1t2.User_Id=t1.Id "
                + "LEFT JOIN AppRoles t2 ON t2.Id=t1t2.Role_Id "
                ;

            if (!String.IsNullOrWhiteSpace(login))
                sql += "WHERE t1.Login LIKE @login";

            using (var connection = new SqlConnection(connectionString))
            {
                User result = null;

                connection
                    .Query<User, Role, User>(sql,
                    (_user, _role) =>
                    {
                        if (result == null || result.Id == _user?.Id)
                            result = _user;

                        if (_role != null)
                            _user.Roles.Add(_role);

                        return _user;
                    },
                    new { login });

                return result;
            }
        }

        public bool TryDeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
