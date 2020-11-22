using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Core.Exceptions;
using CodiblyTest.Mailer.Core.Queries;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Query;
using CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects;
using CodiblyTest.Mailer.Storage;
using Microsoft.EntityFrameworkCore;

namespace CodiblyTest.Mailer.Domain.Mail.Queries
{
    public class MailQueryHandlers :
        IQueryHandler<GetMailDetailsQuery, MailDetailsModel>,
        IQueryHandler<GetMailListQuery, IReadOnlyCollection<MailListItemModel>>,
        IQueryHandler<GetMailStatusQuery, MailStatus>
    {
        private readonly MailerDbContext _dbContext;

        public MailQueryHandlers(MailerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MailDetailsModel> Handle(GetMailDetailsQuery request, CancellationToken cancellationToken)
        {
            var model = await _dbContext.Mails
                .Include(m => m.Attachments)
                .SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
            if (model == null)
                throw new EntityNotFoundException(request.Id);

            return new MailDetailsModel
            {
                Id = model.Id,
                Sender = model.Sender,
                Recipients = model.Recipients,
                Subject = model.Subject,
                Body = model.Body,
                Status = model.Status,
                Priority = model.Priority,
                Attachments = model.Attachments.Select(a => new AttachmentModel { FileName = a.FileName }).ToList()
            };
        }

        public async Task<IReadOnlyCollection<MailListItemModel>> Handle(GetMailListQuery request, CancellationToken cancellationToken)
            => await _dbContext.Mails
                .Take(request.PageSize)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Select(m => new MailListItemModel
                {
                    Id = m.Id,
                    Sender = m.Sender,
                    Recipients = m.Recipients,
                    Subject = m.Subject,
                    Status = m.Status
                })
                .ToListAsync(cancellationToken);

        public async Task<MailStatus> Handle(GetMailStatusQuery request, CancellationToken cancellationToken)
        {
            var model = await _dbContext.Mails
                .Select(m => new
                {
                    Id = m.Id,
                    Status = m.Status
                })
                .SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);
            if (model == null)
                throw new EntityNotFoundException(request.Id);

            return model.Status;
        }
    }
}
