using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(
            string emailTo,
            string body,
            string subject);

        string templateBodyVerifyEmail(
            string username,
            string url);

        string templateBodyChangePassoword(
            string username,
            string url);
    }
}
