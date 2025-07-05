'use client';
import dynamic from 'next/dynamic';
import type { TranslationTableProps } from '@/app/Components/Table';
import { Color } from 'antd/es/color-picker';
import { AlignCenterOutlined } from '@ant-design/icons';

const TranslationTable = dynamic<TranslationTableProps>(
  () => import('@/app/Components/Table').then(m => m.default),
  { ssr: false }
);

export default function Home() {
  return (
    <>
      <p>HOME PAGE</p>
      <TranslationTable pageSize={10} />
    </>
  );
}