namespace LocalizationService.Domain.ValueObjects;

public readonly record struct LocalizationKey
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