using System.Threading;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Commands;
using CodiblyTest.Mailer.Core.Exceptions;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using CodiblyTest.Mailer.Domain.Mail.Validators;
using CodiblyTest.Mailer.Storage;
using CodiblyTest.Mailer.Storage.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodiblyTest.Mailer.Domain.Mail.Commands
{
    public class AttachmentCommandHandler :
        ICommandHandler<CreateAttachment>
    {
        private readonly MailerDbContext _dbContext;
        private readonly IMailDomainValidator _domainValidator;

        public AttachmentCommandHandler(MailerDbContext dbContext, IMailDomainValidator domainValidator)
        {
            _dbContext = dbContext;
            _domainValidator = domainValidator;
        }

        public async Task<Unit> Handle(CreateAttachment request, CancellationToken cancellationToken)
        {
            var mail = await _dbContext.Mails.SingleOrDefaultAsync(m => m.Id == request.MailId, cancellationToken: cancellationToken);
            if (mail == null)
                throw new EntityNotFoundException(request.MailId);

            _domainValidator.CheckForAttachmentAdd(mail);

            var attachment = new Attachment
            {
                FileName = request.FileName,
                Data = new AttachmentData
                {
                    Data = request.File
                }
            };
            mail.Attachments.Add(attachment);

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
