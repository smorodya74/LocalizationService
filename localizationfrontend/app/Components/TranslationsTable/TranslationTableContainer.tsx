'use client';

import React, { useEffect, useMemo, useState, useCallback } from 'react';
import { message, Input } from 'antd';
import type { ColumnsType, ColumnType } from 'antd/es/table';
import { getTranslationsPage, updateTranslation } from '@/app/Services/translationsService';
import TranslationTableView from './TranslationTableView';
import { useLanguages } from '@/app/Hooks/useLanguage';

interface Row {
    keyName: string;
    [langCode: string]: string | undefined;
}

export default function TranslationTableContainer({ pageSize = 10 }: { pageSize?: number }) {
    const [isFirstLoad, setIsFirstLoad] = useState(true);
    const [rows, setRows] = useState<Row[]>([]);
    const [total, setTotal] = useState(0);
    const [page, setPage] = useState(1);
    const [search, setSearch] = useState('');
    const [loading, setLoading] = useState(false);

    const [isAddKeyOpen, setAddKeyOpen] = useState(false);
    const [isAddLangOpen, setAddLangOpen] = useState(false);

    const [editing, setEditing] = useState<{ key: string; lang: string } | null>(null);
    const [editingValue, setEditingValue] = useState('');

    const { languages, reload: reloadLanguages } = useLanguages();

    const loadPage = useCallback(async () => {
        setLoading(true);
        try {
            const res = await getTranslationsPage(page, pageSize, search.trim());
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
            setIsFirstLoad(false);
        }
    }, [page, pageSize, search]);

    useEffect(() => { loadPage(); }, [loadPage]);

    const saveCell = async () => {
        if (!editing) return;
        const { key, lang } = editing;

        try {
            await updateTranslation(key, lang, editingValue);
            setRows(prev => prev.map(r => r.keyName === key ? { ...r, [lang]: editingValue } : r));
        } catch {
            message.error('Не удалось сохранить перевод');
        }
        setEditing(null);
    };

    const columns: ColumnsType<Row> = useMemo(() => {
        const langCols: ColumnType<Row>[] = languages.map((l: any) => {
            const lang = l.languageCode as string;

            return {
                title: l.name,
                dataIndex: lang,
                width: 150,
                onCell: (record: Row) => ({
                    onClick: () => {
                        setEditing({ key: record.keyName, lang });
                        setEditingValue(record[lang] ?? '');
                    },
                    style: { cursor: 'pointer' }
                }),
                render: (_: string, record: Row) => {
                    const isEditing = editing?.key === record.keyName && editing?.lang === lang;

                    if (isEditing) {
                        return (
                            <Input
                                value={editingValue}
                                onChange={e => setEditingValue(e.target.value)}
                                onPressEnter={saveCell}
                                onBlur={saveCell}
                                size="small"
                                autoFocus
                            />
                        );
                    }
                    return record[lang] ?? '';
                }
            };
        });

        return [
            {
                title: 'Ключ',
                dataIndex: 'keyName',
                width: 200,
                fixed: 'left' as const
            },
            ...langCols,
            {
                title: (
                    <button
                        type="button"
                        onClick={() => setAddLangOpen(true)}
                        style={{ padding: 0, background: 'none', border: 'none', color: '#1677ff', cursor: 'pointer' }}
                    >
                        + Add Language
                    </button>
                ),
                dataIndex: '__addLang',
                width: 160,
                fixed: 'right' as const,
                render: () => '-'
            }
        ];
    }, [languages, editing, editingValue]);

    return (
        <TranslationTableView
            isFirstLoad={isFirstLoad}
            rows={rows}
            columns={columns}
            loading={loading}
            page={page}
            pageSize={pageSize}
            total={total}
            onPageChange={setPage}
            searchValue={search}
            onSearch={val => { setPage(1); setSearch(val); }}
            isAddKeyOpen={isAddKeyOpen}
            isAddLangOpen={isAddLangOpen}
            openAddKey={() => setAddKeyOpen(true)}
            closeAddKey={() => setAddKeyOpen(false)}
            openAddLang={() => setAddLangOpen(true)}
            closeAddLang={() => setAddLangOpen(false)}
            onSuccess={() => { reloadLanguages(); setPage(1); loadPage(); }}
        />
    );
}
