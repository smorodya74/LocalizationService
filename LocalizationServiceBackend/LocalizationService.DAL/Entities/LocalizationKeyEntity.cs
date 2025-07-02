namespace LocalizationService.DAL.Entities
{
    public class LocalizationKeyEntity
    {
        public Guid Id { get; set; }
        public string KeyName { get; set; } = null!;

        public ICollection<TranslationEntity> Translations { get; set; } = [];
    }
}
