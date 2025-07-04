namespace LocalizationService.DAL.Entities
{
    /// <summary>
    ///     Перевод по ключу-языку (key-code)
    /// </summary>
    public class TranslationEntity
    {
        // PK
        public string LocalizationKey { get; set; } = null!;
        public string LanguageCode { get; set; } = null!;
        
        // FK
        public LocalizationKeyEntity Localization { get; set; } = null!;
        public LanguageEntity Language { get; set; } = null!;

        // Data
        public string TranslationText { get; set; } = string.Empty;
    }
}