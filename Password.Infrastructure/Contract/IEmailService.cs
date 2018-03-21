namespace Password.Infrastructure.Contract
{
    public interface IEmailService
    {
        bool Send(string text, string email);
    }
}
