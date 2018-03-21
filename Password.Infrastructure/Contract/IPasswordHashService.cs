namespace Password.Infrastructure.Contract
{
    public interface IPasswordHashService
    {
        string HashPassword(string password, string salt);
    }
}
