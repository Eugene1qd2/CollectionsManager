using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CollectionManager.Services
{

    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        private readonly SmtpClient _mySmtpClient;
        private const string HOST = "smtp.mail.ru";
        private const int PORT = 587;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            _mySmtpClient = new SmtpClient(HOST, PORT);
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SmtpKey) || string.IsNullOrEmpty(Options.SmtpMail))
            {
                throw new Exception("Null key");
            }
            _logger.LogInformation($"Trying to send message!");
            _logger.LogInformation($"Check key: {Options.SmtpKey}");
            await Execute(Options.SmtpKey, Options.SmtpMail, subject, message, toEmail);
        }

        private void SetupSmtpClient(string apiKey, string sender)
        {
            _mySmtpClient.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(sender, apiKey);
            _mySmtpClient.Credentials = basicAuthenticationInfo;
            _mySmtpClient.EnableSsl = true;
        }

        private MailMessage GetMailAddress(string sender, string subject, string message, string toEmail)
        {
            MailAddress from = new MailAddress(sender, "Collections Manager");
            MailAddress to = new MailAddress(toEmail, toEmail);
            MailMessage myMail = new MailMessage(from, to)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            return myMail;
        }

        public async Task Execute(string apiKey, string sender, string subject, string message, string toEmail)
        {
            try
            {
                SetupSmtpClient(apiKey, sender);
                var mail=GetMailAddress(sender, subject, message, toEmail); 
                await _mySmtpClient.SendMailAsync(mail);
                _logger.LogInformation($"Email to {toEmail} queued successfully!");
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
        }
    }
}
