namespace LocalizationService.DAL.Entities
{
    public class LanguageEntity
    {
        public Guid Id { get; set; }
        public string LanguageKey { get; set; } = null!;
        public string Name { get; set; } = null!;

        public ICollection<TranslationEntity> Translations { get; set; } = [];
    }
}
