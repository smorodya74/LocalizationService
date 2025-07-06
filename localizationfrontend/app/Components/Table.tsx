'use client';

import React, { useEffect, useState, useCallback } from 'react';
import { Table, Pagination, Spin, message, Button } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import Search from 'antd/es/input/Search';
import axios from 'axios';

import AddKeyModal       from '@/app/Components/AddKeyModal';
import AddLanguageModal  from '@/app/Components/AddLanguageModal';
import { fetchTranslationsPage } from '@/app/lib/API';

export interface TranslationTableProps {
  pageSize?: number;
}

interface Row {
  keyName: string;
  [langCode: string]: string | undefined;
}

export default function TranslationTable({ pageSize = 10 }: TranslationTableProps) {
  const [rows, setRows]         = useState<Row[]>([]);
  const [total, setTotal]       = useState(0);
  const [page, setPage]         = useState(1);
  const [search, setSearch]     = useState('');
  const [loading, setLoading]   = useState(false);
  const [columns, setColumns]   = useState<any[]>([]);

  const [isAddKeyOpen,  setAddKeyOpen]  = useState(false);
  const [isAddLangOpen, setAddLangOpen] = useState(false);

  const loadLanguages = useCallback(async () => {
    try {
      const { data } = await axios.get('http://localhost:5172/api/languages');

      const langCols = data.map((l: any) => ({
        title: l.name,
        dataIndex: l.languageCode,
        width: 150,
        render: (text: string) => text ?? ''
      }));

      setColumns([
        {
          title: 'Ключ',
          dataIndex: 'keyName',
          width: 200,
          fixed: 'left'
        },
        ...langCols,
        {
          title: (
            <Button
              type="link"
              icon={<PlusOutlined />}
              onClick={() => setAddLangOpen(true)}
              style={{ padding: 0 }}
            >
              Add&nbsp;Language
            </Button>
          ),
          dataIndex: '__addLang',
          width: 160,
          fixed: 'right',
          render: () => '-'
        }
      ]);
    } catch {
      message.error('Не удалось получить список языков');
    }
  }, []);

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

      setRows([...map.values()]);
      setTotal(res.totalCount);
    } catch {
      message.error('Ошибка загрузки переводов');
    } finally {
      setLoading(false);
    }
  }, [page, pageSize, search]);

  useEffect(() => { loadLanguages(); }, [loadLanguages]);
  useEffect(() => { loadPage(); }, [loadPage]);

  return (
    <div style={{ padding: 24 }}>
      <Search
        placeholder="Поиск по ключу…"
        allowClear
        enterButton
        style={{ maxWidth: 400, marginBottom: 16 }}
        onSearch={val => { setPage(1); setSearch(val); }}
      />

      <Spin spinning={loading}>
        <Table
          rowKey="keyName"
          columns={columns}
          dataSource={rows}
          pagination={false}
          scroll={{ x: 'max-content' }}
          size="middle"
          footer={() => (
            <Button
              type="link"
              icon={<PlusOutlined />}
              onClick={() => setAddKeyOpen(true)}
            >
              Add Key
            </Button>
          )}
        />
      </Spin>

      <Pagination
        style={{ marginTop: 16, textAlign: 'right' }}
        current={page}
        pageSize={pageSize}
        total={total}
        onChange={setPage}
        showSizeChanger={true}
        align="end"
      />

      <AddKeyModal
        open={isAddKeyOpen}
        onClose={() => setAddKeyOpen(false)}
        onSuccess={() => { setPage(1); loadPage(); }}
      />

      <AddLanguageModal
        open={isAddLangOpen}
        onClose={() => setAddLangOpen(false)}
        onSuccess={() => { loadLanguages(); setPage(1); loadPage(); }}
      />
    </div>
  );
}