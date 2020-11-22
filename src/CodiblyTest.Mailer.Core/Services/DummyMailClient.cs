using System;
using System.Threading.Tasks;

namespace CodiblyTest.Mailer.Core.Services
{
    public class DummyMailClient : IMailClient
    {
        public Task Send(string sender, string[] recipients, string subject, string body)
        {
            // TODO replace with real implementation

            var message =
                $"Mail ${subject} sent from ${sender} to ${recipients.Length} recipient(s) at ${DateTime.UtcNow}";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;

            return Task.CompletedTask;
        }
    }
}