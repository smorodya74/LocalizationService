import axios from 'axios';
import { Translation } from '@/app/Models/Types';
import { PageResult } from '@/app/Models/PageResult';


/** Пагинация переводов */
export async function getTranslationsPage(
  page: number,
  pageSize: number,
  search: string
): Promise<PageResult<Translation>> {
  const { data } = await axios.get<PageResult<Translation>>(
    'http://localhost:5172/api/translations/page',
    { params: { page, pageSize, search } }
  );
  return data;
}

/** Обновить одну ячейку перевода */
export async function updateTranslation(
  localizationKey: string,
  languageCode: string,
  translationText: string
): Promise<void> {
  await axios.put('http://localhost:5172/api/translations', {
    localizationKey,
    languageCode,
    translationText
  });
}
