using System;
using System.Threading;
using System.Threading.Tasks;
using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Core.Services;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using CodiblyTest.Mailer.Domain.Mail.Commands;
using CodiblyTest.Mailer.Domain.Mail.Validators;
using FluentAssertions;
using Moq;
using Xunit;

namespace CodiblyTest.Mailer.Domain.Tests
{
    public class MailCommandHandlersTests : BaseHandlersTests
    {   
        private readonly Mock<IMailClient> _mailClient;
        private readonly Mock<IMailDomainValidator> _domainValidator;

        private readonly MailCommandHandlers Sut;

        public MailCommandHandlersTests()
        {
            _mailClient = new Mock<IMailClient>();
            _domainValidator = new Mock<IMailDomainValidator>();

            Sut = new MailCommandHandlers(DbContext, _mailClient.Object, _domainValidator.Object);
        }

        [Fact]
        public async Task OnSendMailsCommand_WhenMailSentSuccessfully_SetProperMailStatus()
        {
            // arrange
            var pendingMail = SetupMail(MailStatus.Pending);
            _mailClient
                .Setup(m => m.Send(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            
            // act
            await Sut.Handle(new SendMailsCommand(), CancellationToken.None);

            // assert
            var mailSent = DbContext.Mails.Find(pendingMail.Id);
            mailSent.Status.Should().Be(MailStatus.Sent);
        }

        [Fact]
        public async Task OnSendMailsCommand_WhenMailSentFailed_SetProperMailStatus()
        {
            // arrange
            var pendingMail = SetupMail(MailStatus.Pending);
            _mailClient
                .Setup(m => m.Send(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException());

            // act
            await Sut.Handle(new SendMailsCommand(), CancellationToken.None);

            // assert
            var mailSent = DbContext.Mails.Find(pendingMail.Id);
            mailSent.Status.Should().Be(MailStatus.Failed);
        }

        [Fact]
        public async Task OnSendMailsCommand_SendsOnlyPendingMails()
        {
            // arrange
            var pendingMail = SetupMail(MailStatus.Pending);
            var completedMail1 = SetupMail(MailStatus.Sent);
            var completedMail2 = SetupMail(MailStatus.Failed);
            _mailClient
                .Setup(m => m.Send(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            
            // act
            await Sut.Handle(new SendMailsCommand(), CancellationToken.None);

            // assert
            _mailClient
                .Verify(m => m.Send(
                        It.Is<string>(p => p == pendingMail.Sender),
                        It.Is<string[]>(p => p == pendingMail.Recipients),
                        It.Is<string>(p => p == pendingMail.Subject),
                        It.Is<string>(p => p == pendingMail.Body)),
                    Times.Once);
            _mailClient.VerifyNoOtherCalls();
        }
    }
}
