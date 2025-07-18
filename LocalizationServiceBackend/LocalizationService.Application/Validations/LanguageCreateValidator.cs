﻿using FluentValidation;
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

            RuleFor(l => l.Name)
                .NotEmpty().WithMessage("Название языка не может быть пустым")
                .MaximumLength(64).WithMessage("Длина названия не должна быть не более 64 символов")
                .MustAsync(async (name, _) => !await repository.ExistsAsync(name))
                .WithMessage("Название '{PropertyValue}' уже существует");
        }
    }
}