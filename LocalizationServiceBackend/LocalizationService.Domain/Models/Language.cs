namespace LocalizationService.Domain.Models
{
    public class Language
    {
        public Language(string languageCode, string name)
        {
            LanguageCode = languageCode;
            Name = name;
        }

        public string LanguageCode { get; }
        public string Name { get; private set; }

        public void UpdateName(string newName) => Name = newName;

        public static (Language? Result, string? Error) CreateDB(string languageCode, string name)
        {
            return (new Language(languageCode, name), null);
        }
    }
}