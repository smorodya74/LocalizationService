namespace LocalizationService.Domain.Models
{
    public class LocalizationKey
    {
        public LocalizationKey(string keyName)
        {
            Id = Guid.NewGuid();
            KeyName = keyName;
        }

        public Guid Id { get; }
        public string KeyName { get; }
    }
}
