using FluentValidation;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;
using System.Diagnostics.CodeAnalysis;

namespace SiteConfiguration.API.TransactionPriority.ModelValidator
{
    [ExcludeFromCodeCoverage]
    public class TransactionPriorityValidator : AbstractValidator<TransactionPriorityPost>
    {
        public TransactionPriorityValidator()
        {
            RuleFor(m => m.PriorityName).NotEmpty().Matches(@"^[^<>%@#$&]*$").WithMessage("'Description' should not contain special characters").Length(1,50).WithMessage("'Description' can not be more than 50 characters.");
            RuleFor(m => m.PriorityCode).NotEmpty().Matches(@"^[^<>%@#$&]*$").WithMessage("'Code' should not contain special characters").Length(1, 25).WithMessage("'Code' can not be more than 25 characters.");
        }
    }
}
