namespace LocalizationService.DAL.Entities
{
    /// <summary>
    ///     Справочник всех ключей
    /// </summary>
    public class LocalizationKeyEntity
    {
        public string KeyName { get; set; } = null!;

        public ICollection<TranslationEntity> Translations { get; set; } = [];
    }
}