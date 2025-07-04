using FluentValidation;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    public class TranslationCreateUpdateValidator : AbstractValidator<Translation>
    {
        public TranslationCreateUpdateValidator() 
        {
            RuleFor(t => t.TranslationText)
                .MaximumLength(512).WithMessage("Текст перевода слишком длинный");
        }
    }
}