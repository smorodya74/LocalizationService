namespace LocalizationService.Domain.ValueObjects;

public readonly record struct LocalizationKey
{
    public LocalizationKey(string keyName)
    {
        if (string.IsNullOrWhiteSpace(keyName))
            throw new ArgumentException("Ключ не может быть пустым", nameof(keyName));

        KeyName = keyName;
    }

    public string KeyName { get; }

    public override string ToString() => KeyName;

    public static implicit operator string(LocalizationKey key) => key.KeyName;
    public static explicit operator LocalizationKey(string key) => new(key);
}