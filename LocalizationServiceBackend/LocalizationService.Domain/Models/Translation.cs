namespace LocalizationService.Domain.Models
{
    public class Translation
    {
        public Translation(LocalizationKey key, Language language, string? translationText)
        {
            LocalizationKey = key;
            Language = language;
            TranslationText = translationText;
        }

        public LocalizationKey LocalizationKey { get; }
        public Language Language { get; }
        public string? TranslationText { get; private set; }

        public void UpdateTranslationText(string? newText)
        {
            TranslationText = newText;
        }
    }
}