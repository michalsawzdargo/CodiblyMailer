using System;
using CodiblyTest.Mailer.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodiblyTest.Mailer.Storage
{
    public class MailerDbContext : DbContext
    {
        private const char CollectionDelimiter = ';';

        public MailerDbContext(DbContextOptions<MailerDbContext> options)
            : base(options) { }

        public DbSet<Mail> Mails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mail>()
                .Property(p => p.Recipients)
                .HasConversion(
                    v => string.Join(CollectionDelimiter, v),
                    v => v.Split(CollectionDelimiter, StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Attachment>()
                .HasOne(p => p.Data)
                .WithOne(p => p.Attachment)
                .HasForeignKey<AttachmentData>(p => p.AttachmentId);
        }
    }
}
