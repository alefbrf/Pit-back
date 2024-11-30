using CoffeeBreak.Application.Common.Interfaces.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CoffeeBreak.Infrastructure.Email
{
    public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
    {
        private EmailSettings _emailSettings = emailSettings.Value;

        public MailMessage CreateMessage(CoffeeBreak.Application.DTOs.Request.Email.Email dto)
        {
            string sender = _emailSettings.User;
            string recipient = dto.RecipientEmail;
            string subject = dto.Subject;
            string body = dto.Message;

            return new MailMessage(sender, recipient, subject, body);
        }

        public void SendEmail(CoffeeBreak.Application.DTOs.Request.Email.Email dto, MailMessage? message = null)
        {
            if (message is null)
            {
                message = CreateMessage(dto);
            }

            try
            {
                using (SmtpClient smtpClient = new SmtpClient(_emailSettings.Server, _emailSettings.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.User, _emailSettings.Password);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
