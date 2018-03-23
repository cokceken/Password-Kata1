using System.IO;
using Password.Domain.Contract;

namespace Password.Infrastructure.Services.Dummy
{
    public class DummyEmailService : IEmailService
    {
        public bool Send(string text, string email)
        {
            using (var file = new StreamWriter(@"C:\Users\s.cokceken\Desktop\mail.txt"))
            {
                file.WriteLine($"Email: {email}\tText: {text}");
            }

            return true;
        }
    }
}
