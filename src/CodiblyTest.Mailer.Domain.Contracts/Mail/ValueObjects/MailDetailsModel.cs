using System.Collections.Generic;
using CodiblyTest.Mailer.Core.Enums;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects
{
    public class MailDetailsModel
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailStatus Status { get; set; }
        public MailPriority Priority { get; set; }
        public ICollection<AttachmentModel> Attachments { get; set; }
    }
}
