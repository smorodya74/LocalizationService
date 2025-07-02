namespace LocalizationService.Domain.Models
{
    public class Language
    {
        public Language(string languageKey, string name)
        {
            Id = Guid.NewGuid();
            LanguageKey = languageKey;
            Name = name;
        }

        public Guid Id { get; }
        public string LanguageKey { get; }
        public string Name { get; }
    }
}
