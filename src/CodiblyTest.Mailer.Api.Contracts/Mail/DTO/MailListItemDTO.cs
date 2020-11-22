namespace CodiblyTest.Mailer.Api.Contracts.Mail.DTO
{
    public class MailListItemDTO
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string[] Recipients { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
    }
}