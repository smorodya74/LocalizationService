namespace LocalizationService.WebAPI.DTOs.TranslationDTO
{
    public record UpdateTranslationDto(
    string LocalizationKey,
    string LanguageCode,
    string LanguageName,
    string? TranslationText);
}
