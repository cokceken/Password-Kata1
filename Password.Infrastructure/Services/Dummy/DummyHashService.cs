using Password.Infrastructure.Contract;

namespace Password.Infrastructure.Services.Dummy
{
    public class DummyHashService : IHashService
    {
        public string HashPassword(string password)
        {
            return password;
        }
    }
}
