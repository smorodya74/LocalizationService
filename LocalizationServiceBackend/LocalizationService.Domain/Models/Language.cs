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

        public void UpdateName(string newName)
        {
            Name = newName;
        }
    }
}