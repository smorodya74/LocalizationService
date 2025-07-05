export interface Language {
  languageCode: string;
  name: string;
}

export interface LocalizationKey {
  keyName: string;
}

export interface Translation {
  localizationKey: LocalizationKey;
  language: Language;
  translationText: string | null;
}