using System.Threading.Tasks;

namespace CodiblyTest.Mailer.Core.Services
{
    public interface IMailClient
    {
        Task Send(
            string sender,
            string[] recipients,
            string subject,
            string body);
    }
}
