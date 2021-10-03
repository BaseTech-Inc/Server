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

        public string templateEmail(
            string Subject,
            string Username,
            string Body, 
            string Description,
            string UrlAction,
            string Action)
        {
            return $"" +
            "<!doctype html>" +
            "<html>" +
            "  <head>" +
            "    <meta name='viewport' content='width=device-width' />" +
            "    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" +
            $"    <title>{ Subject }</title>" +
            "    <style>" +
            "img{border:none;-ms-interpolation-mode:bicubic;max-width:100%}body{background - color:#f6f6f6;font-family:sans-serif;-webkit-font-smoothing:antialiased;font-size:14px;line-height:1.4;margin:0;padding:0;-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%}table{border - collapse:separate;width:100%}table td{font - family:sans-serif;font-size:14px;vertical-align:top}.body{background - color:#f6f6f6;width:100%}.container{display:block;margin:0 auto!important;max-width:580px;padding:10px;width:580px}.content{box - sizing:border-box;display:block;margin:0 auto;max-width:580px;padding:10px}.main{background:#fff;border-radius:3px;width:100%}.wrapper{box - sizing:border-box;padding:20px}.content-block{padding - bottom:10px;padding-top:10px}.footer{clear:both;margin-top:10px;text-align:center;width:100%}.footer a,.footer p,.footer span,.footer td{color:#999;font-size:12px;text-align:center}h1,h2,h3,h4{color:#000;font-family:sans-serif;font-weight:400;line-height:1.4;margin:0;margin-bottom:30px}h1{font - size:35px;font-weight:300;text-align:center;text-transform:capitalize}ol,p,ul{font - family:sans-serif;font-size:14px;font-weight:400;margin:0;margin-bottom:15px}ol li,p li,ul li{list - style - position:inside;margin-left:5px}a{color:#3498db;text-decoration:underline}.btn{box - sizing:border-box;width:100%}.btn>tbody>tr>td{padding - bottom:15px}.btn table{width:auto}.btn table td{background - color:#fff;border-radius:5px;text-align:center}.btn button{background - color:#fff;border:solid 1px #3498db;border-radius:5px;box-sizing:border-box;color:#3498db;cursor:pointer;display:inline-block;font-size:14px;font-weight:700;margin:0;padding:12px 25px;text-decoration:none;text-transform:capitalize}.btn-primary table td{background - color:#3498db}.btn-primary button{background - color:#3498db;border-color:#3498db;color:#fff}.last{margin - bottom:0}.first{margin - top:0}.align-center{text - align:center}.align-right{text - align:right}.align-left{text - align:left}.clear{clear:both}.mt0{margin - top:0}.mb0{margin - bottom:0}.preheader{color:transparent;display:none;height:0;max-height:0;max-width:0;opacity:0;overflow:hidden;visibility:hidden;width:0}.powered-by a{text - decoration:none}hr{border:0;border-bottom:1px solid #f6f6f6;margin:20px 0}@media only screen and (max-width:620px){table[class= body] h1{font-size:28px!important;margin-bottom:10px!important}table[class=body] button,table[class=body] a,table[class=body] ol,table[class=body] p,table[class=body] span,table[class=body] td,table[class=body] ul{font-size:16px!important}table[class=body] .article,table[class=body] .wrapper{padding:10px!important}table[class=body] .content{padding:0!important}table[class=body] .container{padding:0!important;width:100%!important}table[class=body] .main{border-left-width:0!important;border-radius:0!important;border-right-width:0!important}table[class=body] .btn table{width:100%!important}table[class=body] .btn button{width:100%!important}table[class=body] .img-responsive{height:auto!important;max-width:100%!important;width:auto!important}}@media all{.ExternalClass{width:100%}.ExternalClass,.ExternalClass div,.ExternalClass font,.ExternalClass p,.ExternalClass span,.ExternalClass td{line-height:100%}.apple-link button{color:inherit!important;font-family:inherit!important;font-size:inherit!important;font-weight:inherit!important;line-height:inherit!important;text-decoration:none!important}#MessageViewBody button{color:inherit;text-decoration:none;font-size:inherit;font-family:inherit;font-weight:inherit;line-height:inherit}.btn-primary table td:hover{background-color:#34495e!important}.btn-primary button:hover{background - color:#34495e!important;border-color:#34495e!important}.center{margin:0 auto;display:block;}}" +
            "    </style>" +
            "  </head>" +
            "  <body class='>" +
            "    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>" +
            "      <tr>" +
            "        <td>&nbsp;</td>" +
            "        <td class='container'>" +
            "          <div class='content'>" +
            "" +
            "            <!-- START CENTERED WHITE CONTAINER -->" +
            "            <table role='presentation' class='main'>" +
            "" +
            "              <!-- START MAIN CONTENT AREA -->" +
            "              <tr>" +
            "                <td class='wrapper'>" +
            "                  <table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
            "                    <tr>" +
            "                      <td align='center'>" +
            "                        <img class='center' src='https://tupaweb.azurewebsites.net/Content/Images/logo.png' alt='logo' width='100px'>" +
            "                      </td>" +
            "                    </tr>" +
            "                    <tr>" +
            "                      <td>" +
            $"                        <p>Olá { Username} ✌,</p>" +
            $"                        <p>{ Body }</p>" +
            "" +
            $"                        <form action='{ UrlAction }' method='post'>" +
            "                          <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='btn btn-primary'>" +
            "                            <tbody>" +
            "                              <tr>" +
            "                                <td align='left'>" +
            "                                  <table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
            "                                    <tbody>" +
            "                                      <tr>" +
            $"                                        <td> <button type='submit'>{ Action }</button> </td>" +
            "                                      </tr>" +
            "                                    </tbody>" +
            "                                  </table>" +
            "                                </td>" +
            "                              </tr>" +
            "                            </tbody>" +
            "                          </table>" +
            "                        </form>" +
            "" +
            $"                        <p>{ Description }</p>" +
            "                        <p>Obrigado.</p>" +
            "                      </td>" +
            "                    </tr>" +
            "                  </table>" +
            "                </td>" +
            "              </tr>" +
            "" +
            "            <!-- END MAIN CONTENT AREA -->" +
            "            </table>" +
            "            <!-- END CENTERED WHITE CONTAINER -->" +
            "" +
            "            <!-- START FOOTER -->" +
            "            <div class='footer'>" +
            "              <table role='presentation' border='0' cellpadding='0' cellspacing='0'>" +
            "                <tr>" +
            "                  <td class='content-block'>" +
            $"                    <span class='apple-link'>Copyrights ©{ DateTime.Now.Year } BaseTech Inc, Tupã</span>" +
            "                  </td>" +
            "                </tr>" +
            "              </table>" +
            "            </div>" +
            "            <!-- END FOOTER -->" +
            "" +
            "          </div>" +
            "        </td>" +
            "        <td>&nbsp;</td>" +
            "      </tr>" +
            "    </table>" +
            "  </body>" +
            "</html>" +
            "";
        }
    }
}
