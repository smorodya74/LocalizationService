namespace LocalizationService.DAL.Entities
{
    /// <summary>
    ///     Перевод по ключу-языку (key-code)
    /// </summary>
    public class TranslationEntity
    {
        public string LocalizationKey { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;
        public LanguageEntity Language { get; set; } = null!;

        public string TranslationText { get; set; }
    }
}
