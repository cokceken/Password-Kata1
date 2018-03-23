namespace Password.Domain.Contract
{
    public interface IHashService
    {
        string Hash(string value);
    }
}
