using FluentValidation;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    internal class TranslationValidator : AbstractValidator<Translation>
    {
        public TranslationValidator() 
        {
            RuleFor(t => t.TranslationText)
                .MaximumLength(512).WithMessage("Текст перевода слишком длинный");
        }
    }
}
