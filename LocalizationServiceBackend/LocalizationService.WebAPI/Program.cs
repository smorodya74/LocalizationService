using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Application.Services;
using LocalizationService.Application.Validations;
using LocalizationService.DAL;
using LocalizationService.DAL.Repositories;
using LocalizationService.Domain.Models;
using LocalizationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LocalizationServiceDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString(nameof(LocalizationServiceDbContext));
        options.UseNpgsql(connectionString);
    });

builder.Services.AddScoped<LanguagesService>();
builder.Services.AddScoped<ILanguagesRepository, LanguagesRepository>();
builder.Services.AddScoped<IValidator<Language>, LanguageCreateUpdateValidator>();

builder.Services.AddScoped<LocalizationKeysService>();
builder.Services.AddScoped<ILocalizationKeysRepository, LocalizationKeysRepository>();
builder.Services.AddScoped<IValidator<LocalizationKey>, LocalizationKeyCreateUpdateValidator>();

builder.Services.AddScoped<TranslationsService>();
builder.Services.AddScoped<ITranslationsRepository, TranslationsRepository>();
builder.Services.AddScoped<IValidator<Translation>, TranslationCreateUpdateValidator>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
