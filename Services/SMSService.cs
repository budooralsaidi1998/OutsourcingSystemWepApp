using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace OutsourcingSystemWepApp.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string toPhoneNumber, string message);
    }
    public class SMSService : ISmsService
    {
        private readonly string accountSid;
        private readonly string authToken;
        private readonly string fromPhoneNumber; 

        public SMSService(string accountSid, string authToken, string fromPhoneNumber)
        {
            this.accountSid = accountSid;
            this.authToken = authToken;
            this.fromPhoneNumber = fromPhoneNumber;
        }

        public Task SendSmsAsync(string toPhoneNumber, string message)
        {
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new Twilio.Types.PhoneNumber(fromPhoneNumber),
                Body = message,
            };

            var msg = MessageResource.Create(messageOptions);
            return Task.FromResult(msg);
        }
    }
}
