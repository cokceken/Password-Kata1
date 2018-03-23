using Password.Domain.Contract;

namespace Password.Infrastructure.Services.Dummy
{
    public class DummyHashService : IHashService
    {
        public string Hash(string value)
        {
            return value;
        }
    }
}