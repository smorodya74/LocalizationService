'use client';
import dynamic from 'next/dynamic';
import { Color } from 'antd/es/color-picker';
import { AlignCenterOutlined } from '@ant-design/icons';
import TranslationTableContainer from './Components/TranslationsTable/TranslationTableContainer';

export default function Home() {
  return (
    <>
      <TranslationTableContainer pageSize={10} />
    </>
  );
}