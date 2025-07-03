using System.ComponentModel.DataAnnotations;

namespace LocalizationService.DAL.Entities
{
    /// <summary>
    ///     Справочник стран (языков)
    ///     Например, "ru - Русский", "en - English"
    /// </summary>
    public class LanguageEntity
    {
        public string LanguageCode { get; set; }

        public string Name { get; set; }

        public ICollection<TranslationEntity> Translations { get; set; }
    }
}