using LocalizationService.Application.Abstractions.Repositories;
using LocalizationService.DAL;
//using LocalizationService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LocalizationServiceDbContext>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString(nameof(LocalizationServiceDbContext));
        options.UseNpgsql(connectionString);
    });

//builder.Services.AddScoped<ILanguagesRepository, LanguagesRepository>();
//builder.Services.AddScoped<ILocalizationKeysRepository, LocalizationKeysRepository>();
//builder.Services.AddScoped<ITranslationsRepository, TranslationsRepository>();

// Контроллеры
builder.Services.AddControllers();

// Swagger
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
