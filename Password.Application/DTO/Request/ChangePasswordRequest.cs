namespace Password.Application.DTO.Request
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}