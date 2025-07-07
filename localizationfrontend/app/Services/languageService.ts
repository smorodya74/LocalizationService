import axios from 'axios';
import { Language } from '@/app/Models/Types'

export async function getLanguages(): Promise<Language[]> {
  const { data } = await axios.get<Language[]>('http://localhost:5172/api/languages');
  return data;
}
