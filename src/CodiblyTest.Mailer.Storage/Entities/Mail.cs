using System.Collections.Generic;
using CodiblyTest.Mailer.Core;
using CodiblyTest.Mailer.Core.Enums;

namespace CodiblyTest.Mailer.Storage.Entities
{
    public class Mail : IEntity
    {
        public int Id { get; set; }

        public string Sender { get; set; }

        public string[] Recipients { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public MailPriority Priority { get; set; }

        public MailStatus Status { get; set; }

        public ICollection<Attachment> Attachments {get; set; }
    }
}