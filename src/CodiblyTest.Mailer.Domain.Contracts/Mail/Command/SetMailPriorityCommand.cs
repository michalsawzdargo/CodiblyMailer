using CodiblyTest.Mailer.Core.Commands;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.Command
{
    public class SetMailPriorityCommand : ICommand
    {
        public int Id { get; set; }
        public string Priority { get; set; }
    }
}