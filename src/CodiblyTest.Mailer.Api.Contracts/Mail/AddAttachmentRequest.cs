using Microsoft.AspNetCore.Http;

namespace CodiblyTest.Mailer.Api.Contracts.Mail
{
    public class AddAttachmentRequest
    {
        public IFormFile File { get; set; }
    }
}