namespace Password.Infrastructure.Contract
{
    public interface IHashService
    {
        string HashPassword(string password);
    }
}
