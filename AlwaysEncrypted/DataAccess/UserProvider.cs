using Dapper;
using System.Data;

namespace AlwaysEncrypted.DataAccess.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IDbConnection db;

        public UserProvider(IDbConnection db) => this.db = db;

        public IEnumerable<UserDTO> GetUsers() => db.Query<UserDTO>(Queries.GetAllUsers);
    }

    file static class Queries
    {
        public static string GetAllUsers = @"select * from Users";
    }
}
