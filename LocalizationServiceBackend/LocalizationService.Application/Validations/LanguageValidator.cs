using FluentValidation;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    public class LanguageValidator : AbstractValidator<Language>
    {
        public LanguageValidator() 
        {
            RuleFor(l => l.LanguageCode)
                .NotEmpty().WithMessage("Код страны не может быть пустым")
                .Length(2, 3).WithMessage("Код языка не может соответствовать ISO 3166-1 (из букв)");
        }
    }
}
