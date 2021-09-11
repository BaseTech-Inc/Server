using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(
            string emailTo,
            string body,
            string subject);

        string templateBody(
            string username,
            string url);
    }
}
