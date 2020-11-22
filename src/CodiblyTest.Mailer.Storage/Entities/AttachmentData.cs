using CodiblyTest.Mailer.Core;

namespace CodiblyTest.Mailer.Storage.Entities
{
    public class AttachmentData : IEntity
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }

        public int AttachmentId { get; }
        public Attachment Attachment { get; set; }
    }
}