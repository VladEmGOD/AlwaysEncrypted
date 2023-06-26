using Dapper;
using System.Data;

namespace AlwaysEncrypted.DataAccess.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IDbConnection db;

        public UserProvider(IDbConnection db) { this.db = db; }

        public void AddUser(UserDTO user)
        {
            db.Query(Queries.AddUser, new { user.Email, user.Name });
        }

        public IEnumerable<UserDTO> GetUsers()
        {


            return db.Query<UserDTO>(Queries.GetAllUsers, new { Email = "Aboba" });
        }
    }

    file static class Queries
    {
        public static string GetAllUsers = @"select * from Users";

        public static string AddUser = @"INSERT INTO [dbo].[Users] ([Email], [Name]) values (@email, @name)";

    }
}
