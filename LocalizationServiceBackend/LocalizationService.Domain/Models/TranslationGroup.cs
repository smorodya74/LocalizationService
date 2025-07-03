using LocalizationService.Domain.ValueObjects;

namespace LocalizationService.Domain.Models
{
    public class TranslationGroup
    {
        private List<Translation> _translations { get; init; }

        public LocalizationKey Key { get; set; }
        public IReadOnlyCollection<Translation> Translations => _translations.AsReadOnly();

        public TranslationGroup(LocalizationKey key)
        {
            Key = key;
            _translations = new List<Translation>();
        }

        public TranslationGroup(LocalizationKey key, IEnumerable<Translation> translations)
        {
            Key = key;
            _translations = new List<Translation>();

            foreach (var tr in translations) AddOrUpdate(tr);
        }

        public static (TranslationGroup group, string? error) CreateDB(
            LocalizationKey key,
            IEnumerable<Translation> translations)
        {
            try
            {
                var group = new TranslationGroup(key, translations);
                return (group, null);
            }
            catch (Exception ex)
            {
                return (null!, ex.Message);
            }
        }

        /// <summary>
        ///     Добавить новый перевод или заменить текст существующего.
        /// </summary>
        public void AddOrUpdate(Translation translation)
        {
            for (int i = 0; i < _translations.Count; i++)
            {
                if (_translations[i].LanguageCode == translation.LanguageCode)
                {
                    _translations[i].ChangeText(translation.TranslationText);
                    return;
                }
            }

            _translations.Add(translation);
        }

        /// <summary>
        ///     Вернуть перевод по коду языка, 
        ///     либо null, если его нет.
        /// </summary>
        public Translation? GetByCode(string languageCode)
        {
            foreach (var tr in _translations)
                if (tr.LanguageCode == languageCode)
                    return tr;

            return null;
        }

        /// <summary>
        ///     Удалить перевод языка; 
        ///     вернёт true, если был удалён.
        /// </summary>
        public bool Remove(string languageCode)
        {
            for (int i = 0; i < _translations.Count; i++)
            {
                if (_translations[i].LanguageCode == languageCode)
                {
                    _translations.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}