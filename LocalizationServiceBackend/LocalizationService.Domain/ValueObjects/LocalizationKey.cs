namespace LocalizationService.Domain.ValueObjects;

public readonly record struct LocalizationKey
{
    public LocalizationKey(string keyName)
    {
        KeyName = keyName;
    }

    public string KeyName { get; }
}