namespace LocalizationService.DAL.DTO.TranslationDTO
{
    public record UpdateTranslationDto(
    string LocalizationKey,
    string LanguageCode,
    string LanguageName,
    string? TranslationText);
}
