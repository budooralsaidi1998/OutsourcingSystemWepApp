using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
namespace OutsourcingSystemWepApp.Services
{


    public class LoginLogService : ILoginLogService
    {
        private readonly string _logFilePath;
        private readonly ConcurrentDictionary<string, int> _failedAttempts;

        public LoginLogService()
        {
            _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "login_logs.txt");
            _failedAttempts = new ConcurrentDictionary<string, int>();
        }

        public async Task LogLoginAsync(string email, string role, bool isSuccess)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {email} | {role} | Login {(isSuccess ? "Success" : "Failed")}{Environment.NewLine}";

            await File.AppendAllTextAsync(_logFilePath, logMessage);
        }

        public async Task<int> GetFailedAttemptsAsync(string email)
        {
            _failedAttempts.TryGetValue(email, out var failedAttempts);
            return failedAttempts;
        }

        public async Task IncrementFailedAttemptsAsync(string email)
        {
            _failedAttempts.AddOrUpdate(email, 1, (key, value) => value + 1);
        }

        public async Task ResetFailedAttemptsAsync(string email)
        {
            _failedAttempts.TryRemove(email, out _);
        }
    }

}
