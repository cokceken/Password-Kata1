namespace Password.Domain.Contract
{
    public interface IEmailService
    {
        bool Send(string text, string email);
    }
}
