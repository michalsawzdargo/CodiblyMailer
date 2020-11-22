using System.Collections.Generic;

namespace CodiblyTest.Mailer.Api.Contracts.Mail.DTO
{
    public class MailDTO
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public IReadOnlyCollection<MailAttachmentDTO> Attachments { get; set; }
    }
}
