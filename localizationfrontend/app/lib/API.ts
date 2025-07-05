import axios from 'axios';
import { PagedResult } from '@/app/Models/PageResult';
import { Translation } from '@/app/Models/Types';

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_BASE ?? 'http://localhost:5172/api',
});

export async function fetchTranslationsPage(
  page: number,
  pageSize = 10,
  query = ''
): Promise<PagedResult<Translation>> {
  const url = query
    ? `/translations/search?page=${page}&pageSize=${pageSize}&query=${encodeURIComponent(query)}`
    : `/translations/page?page=${page}&pageSize=${pageSize}`;
  const { data } = await api.get(url);
  return data;
}