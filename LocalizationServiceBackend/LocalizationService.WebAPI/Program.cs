using FluentValidation;
using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.Application.Services;
using LocalizationService.Application.Validations;
using LocalizationService.DAL;
using LocalizationService.DAL.Repositories;
using LocalizationService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LocalizationServiceDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString(nameof(LocalizationServiceDbContext));
        options.UseNpgsql(connectionString);
    });

builder.Services.AddScoped<LanguagesService>();
builder.Services.AddScoped<ILanguagesRepository, LanguagesRepository>();
builder.Services.AddScoped<IValidator<Language>, LanguageCreateValidator>();

builder.Services.AddScoped<LocalizationKeysService>();
builder.Services.AddScoped<ILocalizationKeysRepository, LocalizationKeysRepository>();
builder.Services.AddScoped<IValidator<LocalizationKey>, LocalizationKeyCreateValidator>();

builder.Services.AddScoped<TranslationsService>();
builder.Services.AddScoped<ITranslationsRepository, TranslationsRepository>();
builder.Services.AddScoped<IValidator<Translation>, TranslationUpdateValidator>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocalizationServiceDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
