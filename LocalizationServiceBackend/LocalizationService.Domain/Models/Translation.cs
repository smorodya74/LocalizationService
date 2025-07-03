namespace LocalizationService.Domain.Models
{
    public class Translation
    {
        public Translation(string languageCode, string translationText)
        {
            LanguageCode = languageCode;
            TranslationText = translationText;
        }

        public string LanguageCode { get; }
        public string TranslationText { get; private set; }

        public void ChangeText(string newTranslation) => TranslationText = newTranslation;

        public static (Translation? translation, string? Error) CreateDB(string languageCode, string translationText)
            => (new Translation(languageCode, translationText), null);
    }
}
