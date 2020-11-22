using System.Collections.Generic;
using CodiblyTest.Mailer.Api.Contracts.Mail.DTO;

namespace CodiblyTest.Mailer.Api.Contracts.Mail
{
    public class MailListResponse
    {
        public ICollection<MailListItemDTO> Items { get; set; }
    }
}