using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Application.Common.Interfaces;

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
                catch (Exception)
                {
                }
            }
        }

        public string templateBodyVerifyEmail(
            string username,
            string url)
        {
            var body = "";

            body = "" +
                "<!DOCTYPE html>" +
                "<html lang='pt-br'>" +
                "<head>" +
                    "<meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
                    "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                    "<title>Email</title>" +
                    "<link rel='preconnect' href='https://fonts.googleapis.com'>" +
                    "<link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>" +
                    "<link href='https://fonts.googleapis.com/css2?family=Open+Sans&display=swap' rel='stylesheet'>" +
                    "<style>" +
                        "body {font-family: 'Open Sans', sans-serif;font-size: 1rem;background: #F7F9FB;}" +
                        ".container {min-width: 320px;max-width: 600px;width: 600px;margin: 0 auto;}" +
                        ".card {background-color: #FFFFFF;border: 1px solid #D9DBDD;box-shadow: 0 3px 6px #EDEFF1;border-radius: 4px;}" +
                        "table tr td {padding: 12px 32px;}" +
                        "button {background: #3B91F3;border: 1px solid #1F72D1;color: #ffffff;padding: 10px 12px;border-radius: 6px;font-size: 1rem;}" +
                    "</style>" +
                "</head>" +
                "<body>" +
                    "<table class='container'>" +
                        "<tr>" +
                            "<td align='center'>" +
                                "<img src='https://tupaweb.azurewebsites.net/Content/Images/logo.png' alt='logo' width='100px'>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                    "<table class='container card'>" +
                        "<tr>" +
                            "<td align='center' style='background-color: #2485F3; color: #ffffff;'>" +
                                "<h1>Bem-Vindo ao Tupã, Bro!</h1>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                $"<h3>Olá, ✌ { username }</h3>" +
                                "<p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Placeat aspernatur cumque sunt sapiente ipsa ullam architecto adipisci dicta nulla dolorum, laborum sint ad veritatis dignissimos totam deserunt sit obcaecati amet.</p>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td align='center'>" +
                                $"<form action='{ url }' method='post'>" +
                                    "<button type='submit'>Verificar E-mail!</button>" +
                                "</form>" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<p>" +
                                    "Obrigado," +
                                    "<br>" +
                                    "A Equipe da Empresa" +
                                "</p>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                    "<table class='container'>" +
                        "<tr>" +
                            "<td align='center'>" +
                                "<p style='font-size: .8rem;'>Copyrights © BaseTech Inc.</p>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                "</body>" +
                "</html>";

            return body;
        }
    }
}
