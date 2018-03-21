using Password.Application.DTO.Request;
using Password.Application.DTO.Response;

namespace Password.Application
{
    public interface IPasswordService
    {
        AreValidUserCredentialsResponse AreValidUserCredentials(AreValidUserCredentialsRequest request);
        SendResetEmailResponse SendResetEmail(SendResetEmailRequest request);
        ChangePasswordResponse ChangePassword(ChangePasswordRequest request);
    }
}
