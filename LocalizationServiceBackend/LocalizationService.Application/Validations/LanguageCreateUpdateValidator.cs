using FluentValidation;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    public class LanguageCreateUpdateValidator : AbstractValidator<Language>
    {
        public LanguageCreateUpdateValidator() 
        {
            RuleFor(l => l.LanguageCode)
                .NotEmpty().WithMessage("Код страны не может быть пустым")
                .Length(2, 3).WithMessage("Код языка должен быть из 2-3 символов (ISO 3166-1 формат)");
        }
    }
}