using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Domain.Contracts.Mail;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using FluentValidation;

namespace CodiblyTest.Mailer.Domain.Mail.Validators
{
    public class CreateMailCommandValidator : AbstractValidator<CreateMailCommand>
    {
        public CreateMailCommandValidator()
        {
            RuleFor(p => p.Subject)
                .NotEmpty();
            RuleFor(p => p.Body)
                .NotEmpty();
            RuleFor(p => p.Sender)
                .EmailAddress()
                .When(p => !string.IsNullOrEmpty(p.Sender));
            RuleForEach(p => p.Recipients).EmailAddress();
            RuleFor(p => p.Priority)
                .NotEmpty()
                .IsEnumName(typeof(MailPriority));
        }
    }
}