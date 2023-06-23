namespace AlwaysEncrypted.DataAccess
{
    public interface IUserProvider
    {
        IEnumerable<UserDTO> GetUsers();

        void AddUser(UserDTO user);
    }
}
