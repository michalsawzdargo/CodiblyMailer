using System.Collections.Generic;
using CodiblyTest.Mailer.Core.Queries;
using CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.Query
{
    public class GetMailListQuery : IQuery<IReadOnlyCollection<MailListItemModel>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}