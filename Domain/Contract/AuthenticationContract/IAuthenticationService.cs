namespace Password.Domain.Contract.AuthenticationContract
{
    public interface IAuthenticationService
    {
        bool AreValidUserCredentials(string username, string password);
        void SendResetEmail(string email);
        void ChangePassword(int userId, string token, string newPassword);
    }
}
