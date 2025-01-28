
namespace OutsourcingSystemWepApp.Services
{
    public interface ILoginLogService
    {
        Task<int> GetFailedAttemptsAsync(string email);
        Task IncrementFailedAttemptsAsync(string email);
        Task LogLoginAsync(string email, string role, bool isSuccess);
        Task ResetFailedAttemptsAsync(string email);
    }
}