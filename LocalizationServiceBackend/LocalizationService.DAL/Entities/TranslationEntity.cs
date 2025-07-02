namespace LocalizationService.DAL.Entities
{
    public class TranslationEntity
    {
        public Guid Id { get; set; }
        
        public Guid LocalizationId { get; set; }
        public LocalizationKeyEntity LocalizationKey { get; set; } = null!;

        public Guid LanguageId { get; set; }
        public LanguageEntity Language { get; set; } = null!;

        public string? TranslationText { get; set; }
        public bool IsEditing { get; set; }
    }
}
