using CodiblyTest.Mailer.Domain.Contracts.Mail;
using CodiblyTest.Mailer.Domain.Contracts.Mail.Query;
using FluentValidation;

namespace CodiblyTest.Mailer.Domain.Mail.Validators
{
    public class GetMailListQueryValidator : AbstractValidator<GetMailListQuery>
    {
        public GetMailListQueryValidator()
        {
            RuleFor(p => p.PageNumber).Must(v => v > 0);
            RuleFor(p => p.PageSize).InclusiveBetween(1, 50);
        }
    }
}