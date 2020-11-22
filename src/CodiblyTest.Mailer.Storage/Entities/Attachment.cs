using CodiblyTest.Mailer.Core;

namespace CodiblyTest.Mailer.Storage.Entities
{
    public class Attachment : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public AttachmentData Data { get; set; }
    }
}
