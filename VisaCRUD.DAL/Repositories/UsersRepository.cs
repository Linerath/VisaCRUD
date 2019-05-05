using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
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

        public bool AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            String sql = "INSERT INTO AppUsers (Login, Password) Values (@Login, @Password); SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(connectionString))
            {
                int id = connection.Query<int>(sql, new { user.Login, user.Password }).Single();

                sql = "INSERT INTO AppUsersRoles (User_Id, Role_Id) Values (@id, @role)";

                connection.Execute(sql, new { id, role = 1 });

                return id >= 0;
            }
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

        public bool IsLoginUnique(String login)
        {
            if (String.IsNullOrEmpty(login))
                throw new ArgumentException(nameof(login));

            string sql = "SELECT id FROM AppUsers WHERE Login = @login";

            using (var connection = new SqlConnection(connectionString))
            {
                int result = connection.QueryFirstOrDefault<int>(sql, new { login });

                return result == 0;
            }
        }

        public bool TryDeleteUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
