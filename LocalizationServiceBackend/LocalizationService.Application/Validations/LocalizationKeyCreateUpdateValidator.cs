using FluentValidation;
using LocalizationService.Domain.ValueObjects;

namespace LocalizationService.Application.Validations
{
    public class LocalizationKeyCreateUpdateValidator : AbstractValidator<LocalizationKey>
    {
        public LocalizationKeyCreateUpdateValidator()
        {
            RuleFor(k => k.KeyName)
                .NotEmpty().WithMessage("Ключ не может быть пустым")
                .MaximumLength(255).WithMessage("Ключ не может быть длиннее 255 символов");
        }
    }
}