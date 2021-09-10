using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(
            string emailTo, 
            string body, 
            string subject)
        {
            // Replace sender@example.com with your "From" address. 
            String FROM = _configuration["smtp:fromAdress"];

            // Replace recipient@example.com with a "To" address. If your account 
            // is still in the sandbox, this address must be verified.
            String TO = emailTo;

            // Replace smtp_username with your SMTP user name.
            String SMTP_USERNAME = _configuration["smtp:username"];

            // Replace smtp_password with your SMTP password.
            String SMTP_PASSWORD = _configuration["smtp:password"];

            String HOST = "smtp.gmail.com";

            // The port you will connect to on the SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            int PORT = 587;

            // The subject line of the email
            String SUBJECT = subject;

            // The body of the email
            String BODY = body;

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;

            using (var client = new SmtpClient(HOST, PORT))
            {
                // Pass SMTP credentials
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

                // Enable SSL encryption
                client.EnableSsl = true;

                // Try to send the message. Show status in console.
                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
