using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(
            string emailTo,
            string body,
            string subject);

        string templateEmail(
            string Subject,
            string Username,
            string Body,
            string Description,
            string UrlAction,
            string Action);
    }
}
