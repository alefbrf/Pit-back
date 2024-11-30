using System.Net.Mail;

namespace CoffeeBreak.Application.Common.Interfaces.Email
{
    public interface IEmailService
    {
        MailMessage CreateMessage(CoffeeBreak.Application.DTOs.Request.Email.Email dto);
        void SendEmail(CoffeeBreak.Application.DTOs.Request.Email.Email dto, MailMessage? message = null);
    }
}
