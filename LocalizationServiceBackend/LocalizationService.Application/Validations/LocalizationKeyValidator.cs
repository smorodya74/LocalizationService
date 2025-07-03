using FluentValidation;
using LocalizationService.Domain.ValueObjects;

namespace LocalizationService.Application.Validations
{
    public class LocalizationKeyValidator : AbstractValidator<LocalizationKey>
    {
        public LocalizationKeyValidator()
        {
            RuleFor(k => k.KeyName)
                .NotEmpty().WithMessage("Ключ не может быть пустым")
                .MaximumLength(255).WithMessage("Ключ не может быть длиннее 255 символов");
        }
    }
}
