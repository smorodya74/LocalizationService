namespace LocalizationService.Domain.Models
{
    public class LocalizationKey
    {
        public LocalizationKey(string keyName)
        {
            KeyName = keyName;
        }

        public string KeyName { get; }

        public static (LocalizationKey? Result, string? Error) CreateDB(string keyName)
        {
            return (new LocalizationKey(keyName), null);
        }
    }
}