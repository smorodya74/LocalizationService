namespace LocalizationService.Domain.Models
{
    public class Translation
    {
        public Translation(Guid localizationId, Guid languageId, string translationText)
        {
            Id = Guid.NewGuid();
            LocalizationId = localizationId;
            LanguageId = languageId;
            TranslationText = translationText;
            // IsEditing - уточнить по необходимости
        }

        public Guid Id { get; }
        public Guid LocalizationId { get; }
        public Guid LanguageId { get; }
        public string? TranslationText { get; private set; }
        public bool IsEditing { get; private set; }

        public void EditOpen() =>
            IsEditing = true;

        public void EditClose() =>
            IsEditing = false;
    }
}
