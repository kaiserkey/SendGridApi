using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using SendGridApi.Interfaces;
using SendGridApi.Models;

namespace SendGridApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridSettings _sendGridSettings;
        private readonly SendGridClient _client;

        public EmailService(IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridSettings = sendGridSettings.Value;
            _client = new SendGridClient(_sendGridSettings.ApiKey);
        }

        public async Task<bool> EnviarEmailAsync(string destinatario, string nombre)
        {
            var from = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.FromName);
            var to = new EmailAddress(destinatario);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, _sendGridSettings.TemplateId, new { name = nombre });

            var response = await _client.SendEmailAsync(msg);
            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
