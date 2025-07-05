using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Domain.Models;

namespace LocalizationService.Application.Validations
{
    public class LocalizationKeyCreateValidator : AbstractValidator<LocalizationKey>
    {
        public LocalizationKeyCreateValidator(ILocalizationKeysRepository repository)
        {
            RuleFor(k => k.KeyName)
                .NotEmpty().WithMessage("Ключ не может быть пустым")
                .MaximumLength(255).WithMessage("Ключ не может быть длиннее 255 символов")
                .MustAsync(async (key, _) => !await repository.ExistsAsync(key))
                .WithMessage("Ключ '{PropertyValue}' уже существует"); ;
        }
    }
}