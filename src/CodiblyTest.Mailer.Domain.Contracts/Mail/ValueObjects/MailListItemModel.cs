using CodiblyTest.Mailer.Core.Enums;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects
{
    public class MailListItemModel
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public MailStatus Status { get; set; }
    }
}