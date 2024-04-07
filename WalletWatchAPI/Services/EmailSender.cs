using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;
using WalletWatchAPI.Services.Settings;

namespace WalletWatchAPI.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SMTPSettings> _smtpSettings;
        public EmailSender(IOptions<SMTPSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var smtp = new SmtpClient(_smtpSettings.Value.Host, _smtpSettings.Value.Port))
            {
                smtp.Credentials = new NetworkCredential(_smtpSettings.Value.User, _smtpSettings.Value.Password);

                await smtp.SendMailAsync(_smtpSettings.Value.User, email, subject, htmlMessage);
            }
        }
    }
}
