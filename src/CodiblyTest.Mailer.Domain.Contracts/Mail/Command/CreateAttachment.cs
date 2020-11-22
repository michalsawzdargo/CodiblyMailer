using CodiblyTest.Mailer.Core.Commands;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.Command
{
    public class CreateAttachment : ICommand
    {
        public int MailId { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}