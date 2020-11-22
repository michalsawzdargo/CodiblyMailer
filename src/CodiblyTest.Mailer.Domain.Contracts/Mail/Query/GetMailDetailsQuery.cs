using CodiblyTest.Mailer.Core.Queries;
using CodiblyTest.Mailer.Domain.Contracts.Mail.ValueObjects;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.Query
{
    public class GetMailDetailsQuery : IQuery<MailDetailsModel>
    {
        public int Id { get; set; }
    }
}
