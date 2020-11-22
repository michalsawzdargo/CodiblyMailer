using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Commands;
using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Core.Exceptions;
using CodiblyTest.Mailer.Core.Services;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using CodiblyTest.Mailer.Domain.Mail.Validators;
using CodiblyTest.Mailer.Storage;
using CodiblyTest.Mailer.Storage.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodiblyTest.Mailer.Domain.Mail.Commands
{
    public class MailCommandHandlers : 
        ICommandHandler<CreateMailCommand, int>,
        ICommandHandler<SetMailPriorityCommand>,
        ICommandHandler<SendMailsCommand>
    {
        private readonly MailerDbContext _dbContext;
        private readonly IMailClient _mailClient;
        private readonly IMailDomainValidator _domainValidator;

        public MailCommandHandlers(
            MailerDbContext dbContext, 
            IMailClient mailClient,
            IMailDomainValidator domainValidator)
        {
            _dbContext = dbContext;
            _mailClient = mailClient;
            _domainValidator = domainValidator;
        }

        public async Task<int> Handle(CreateMailCommand request, CancellationToken cancellationToken)
        {
            var mail = new Storage.Entities.Mail
            {
                Sender = request.Sender ?? "no-reply@test.com", // TODO get from config
                Recipients = request.Recipients,
                Subject = request.Subject,
                Body = request.Body,
                Status = MailStatus.Pending,
                Priority = Enum.Parse<MailPriority>(request.Priority)
            };

            _dbContext.Mails.Add(mail);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return mail.Id;
        }

        public async Task<Unit> Handle(SendMailsCommand request, CancellationToken cancellationToken)
        {
            // TODO add batching
            var pendingMails = await _dbContext.Mails
                .Where(m => m.Status == MailStatus.Pending)
                .ToListAsync(cancellationToken: cancellationToken);

            foreach (var mail in pendingMails)
            {
                try
                {
                    await _mailClient.Send(mail.Sender, mail.Recipients, mail.Subject, mail.Body);
                    mail.Status = MailStatus.Sent;
                }
                catch (Exception)
                {
                    mail.Status = MailStatus.Failed;
                }
            }

            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(SetMailPriorityCommand request, CancellationToken cancellationToken)
        {
            var priority = Enum.Parse<MailPriority>(request.Priority);
            var mail = await _dbContext.Mails.SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
            if (mail == null)
                throw new EntityNotFoundException(request.Id);

            _domainValidator.CheckForPriorityChange(mail);

            mail.Priority = priority;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
