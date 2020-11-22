using System;
using CodiblyTest.Mailer.Core.Enums;

namespace CodiblyTest.Mailer.Domain.Mail.Validators
{
    public interface IMailDomainValidator
    {
        void CheckForPriorityChange(Storage.Entities.Mail @mail);
        void CheckForAttachmentAdd(Storage.Entities.Mail @mail);
    }

    public class MailDomainValidator : IMailDomainValidator
    {
        public void CheckForPriorityChange(Storage.Entities.Mail mail)
        {
            EnsureMailOperationIsOpen(mail);
        }

        public void CheckForAttachmentAdd(Storage.Entities.Mail mail)
        {
            EnsureMailOperationIsOpen(mail);
        }

        private void EnsureMailOperationIsOpen(Storage.Entities.Mail mail)
        {
            if (@mail.Status != MailStatus.Pending)
                throw new InvalidOperationException("Mail operation has finished.");
        }
    }
}