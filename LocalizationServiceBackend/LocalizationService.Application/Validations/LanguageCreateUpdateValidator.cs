using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    public class LanguageCreateValidator : AbstractValidator<Language>
    {
        public LanguageCreateValidator(ILanguagesRepository repository) 
        {
            RuleFor(l => l.LanguageCode)
                .NotEmpty().WithMessage("Код страны не может быть пустым")
                .Length(2, 3).WithMessage("Код языка должен быть из 2-3 символов (ISO 3166-1 формат)")
                .MustAsync(async (code, _) => !await repository.ExistsAsync(code))
                .WithMessage("Язык с кодом '{PropertyValue}' уже существует");
        }
    }
}