using CodiblyTest.Mailer.Core.Enums;
using CodiblyTest.Mailer.Domain.Contracts.Mail;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Command;
using FluentValidation;

namespace CodiblyTest.Mailer.Domain.Mail.Validators
{
    public class SetMailPriorityValidator : AbstractValidator<SetMailPriorityCommand>
    {
        public SetMailPriorityValidator()
        {
            RuleFor(p => p.Priority)
                .NotEmpty()
                .IsEnumName(typeof(MailPriority));
        }
    }
}