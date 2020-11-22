using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Core.Queries;

namespace CodiblyTest.Mailer.Domain.Contracts.Mail.Query
{
    public class GetMailStatusQuery : IQuery<MailStatus>
    {
        public int Id { get; set; }
    }
}