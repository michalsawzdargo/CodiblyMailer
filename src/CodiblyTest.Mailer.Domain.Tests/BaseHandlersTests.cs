using System;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Storage;
using Microsoft.EntityFrameworkCore;

namespace CodiblyTest.Mailer.Domain.Tests
{
    public abstract class BaseHandlersTests
    {
        protected readonly MailerDbContext DbContext;

        protected BaseHandlersTests()
        {
            DbContext = SetupDb();
        }

        private MailerDbContext SetupDb()
        {
            var options = new DbContextOptionsBuilder<MailerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new MailerDbContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }

        protected Storage.Entities.Mail SetupMail(MailStatus status = MailStatus.Pending)
        {
            // TODO move to factory
            var mailRecord = new Storage.Entities.Mail
            {
                Sender = Faker.Internet.Email(),
                Recipients = new[] {Faker.Internet.Email(), Faker.Internet.Email()},
                Subject = Faker.Company.Name(),
                Body = Faker.Lorem.Paragraph(),
                Priority = MailPriority.Normal,
                Status = status
            };
            DbContext.Mails.Add(mailRecord);
            DbContext.SaveChanges();
            return mailRecord;
        }
    }
}