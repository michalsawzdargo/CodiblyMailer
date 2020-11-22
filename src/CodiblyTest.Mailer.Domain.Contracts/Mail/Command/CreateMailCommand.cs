using CodiblyTest.Mailer.Core.Commands;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.Command
{
    public class CreateMailCommand : ICommand<int>
    {
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Priority { get; set; }
    }
}
