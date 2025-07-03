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
        public string Name { get; }

        public static (Language language, string? error) CreateDB(
                string languageCode,
                string name)
        {
            try
            {
                var language = new Language(languageCode, name);

                return (language, null);
            }
            catch (Exception ex)
            {
                return (null!, ex.Message);
            }
        }
    }
}