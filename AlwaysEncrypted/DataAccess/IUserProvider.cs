namespace AlwaysEncrypted.DataAccess
{
    public interface IUserProvider
    {
        IEnumerable<UserDTO> GetUsers();
    }
}
