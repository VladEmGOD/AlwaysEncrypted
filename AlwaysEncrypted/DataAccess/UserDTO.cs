namespace AlwaysEncrypted.DataAccess
{
    public record UserDTO(int Id, string Email, string Name)
    {
        public override string ToString() => $"{Id} {Email} {Name}";
    };
}
