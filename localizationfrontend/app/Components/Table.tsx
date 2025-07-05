'use client';

import React, { useEffect, useState, useCallback } from 'react';
import { Table, Input, Pagination, Spin, message } from 'antd';
import { fetchTranslationsPage } from '@/app/lib/API';
import axios from 'axios';

// 👇 описываем пропы, которые действительно нужны компоненту
export interface TranslationTableProps {
  /** Кол‑во строк на странице (по умолчанию 10) */
  pageSize?: number;
}

interface Row {
  keyName: string;
  [langCode: string]: string | undefined;   // eng, ru …
}

export default function TranslationTable({ pageSize = 10 }: TranslationTableProps) {
  const [rows, setRows] = useState<Row[]>([]);
  const [total, setTotal] = useState(0);
  const [page, setPage]   = useState(1);
  const [search, setSearch] = useState('');
  const [loading, setLoading] = useState(false);
  const [columns, setColumns] = useState<any[]>([
    { title: 'Ключ', dataIndex: 'keyName', fixed: 'left', width: 200 }
  ]);

  /** Загружаем список языков один раз */
  const loadLanguages = useCallback(async () => {
    try {
      const { data } = await axios.get('http://localhost:5172/api/languages');
      const langCols = data.map((l: any) => ({
        title: l.name,
        dataIndex: l.languageCode,
        width: 150,
      }));
      setColumns(prev => [...prev, ...langCols]);
    } catch {
      message.error('Не удалось получить список языков');
    }
  }, []);

  /** Загружаем одну страницу переводов */
  const loadPage = useCallback(async () => {
    setLoading(true);
    try {
      const res = await fetchTranslationsPage(page, pageSize, search.trim());
      const map = new Map<string, Row>();

      res.items.forEach((t: any) => {
        const k = t.localizationKey.keyName;
        if (!map.has(k)) map.set(k, { keyName: k });
        map.get(k)![t.language.languageCode] = t.translationText ?? '';
      });

      setRows(Array.from(map.values()));
      setTotal(res.totalCount);
    } catch {
      message.error('Ошибка загрузки переводов');
    } finally {
      setLoading(false);
    }
  }, [page, pageSize, search]);

  /* Первичная загрузка */
  useEffect(() => { loadLanguages(); }, [loadLanguages]);
  useEffect(() => { loadPage(); }, [loadPage]);

  return (
    <div style={{ padding: 24 }}>
      <Input.Search
        placeholder="Поиск по ключу…"
        allowClear
        enterButton
        style={{ maxWidth: 400, marginBottom: 16 }}
        onSearch={(val) => { setPage(1); setSearch(val); }}
      />

      <Spin spinning={loading}>
        <Table
          rowKey="keyName"
          columns={columns}
          dataSource={rows}
          pagination={false}
          scroll={{ x: 'max-content' }}
          size="middle"
        />
      </Spin>

      <Pagination
        style={{ marginTop: 16, textAlign: 'right' }}
        current={page}
        pageSize={pageSize}
        total={total}
        onChange={setPage}
        showSizeChanger={false}
      />
    </div>
  );
}
