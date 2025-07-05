using FluentValidation;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    public class TranslationUpdateValidator : AbstractValidator<Translation>
    {
        public TranslationUpdateValidator() 
        {
            RuleFor(t => t.TranslationText)
                .MaximumLength(512).WithMessage("Текст перевода слишком длинный");
        }
    }
}