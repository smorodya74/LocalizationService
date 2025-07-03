namespace LocalizationService.DAL.Entities
{
    /// <summary>
    ///     Справочник стран (языков)
    ///     Например, "ru", "en"
    /// </summary>
    public class LanguageEntity
    {
        public string LanguageCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public ICollection<TranslationEntity> Translations { get; set; } = [];
    }
}
