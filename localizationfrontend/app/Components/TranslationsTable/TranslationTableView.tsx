'use client';

import React from 'react';
import { Table, Pagination, Spin, Button, Skeleton, Input } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { PlusOutlined, LoadingOutlined } from '@ant-design/icons';

import AddKeyModal from '@/app/Components/Modals/AddKeyModal';
import AddLanguageModal from '@/app/Components/Modals/AddLanguageModal';

interface Row {
    keyName: string;
    [langCode: string]: string | undefined;
}

export interface TranslationTableViewProps {
    isFirstLoad: boolean;
    rows: Row[];
    columns: ColumnsType<Row>;
    loading: boolean;
    page: number;
    total: number;
    pageSize: number;
    onPageChange: (page: number) => void;
    searchValue: string;
    onSearch: (val: string) => void;
    isAddKeyOpen: boolean;
    isAddLangOpen: boolean;
    openAddKey: () => void;
    closeAddKey: () => void;
    openAddLang: () => void;
    closeAddLang: () => void;
    onSuccess: () => void;
}

export default function TranslationTableView({
    isFirstLoad,
    rows,
    columns,
    loading,
    page,
    total,
    pageSize,
    onPageChange,
    searchValue,
    onSearch,
    isAddKeyOpen,
    isAddLangOpen,
    openAddKey,
    closeAddKey,
    closeAddLang,
    onSuccess
}: TranslationTableViewProps) {
    return (
        <div style={{ padding: 24 }}>
            <Input
                placeholder="Поиск по ключу…"
                allowClear
                value={searchValue}
                style={{ maxWidth: 400, marginBottom: 16, minHeight: 40 }}
                onChange={e => onSearch(e.target.value)}
            />
            <Skeleton
                active
                loading={isFirstLoad}
                paragraph={{ rows: 6 }}
                title={false}
            >
                <Spin
                    spinning={loading}
                    indicator={<LoadingOutlined style={{ fontSize: 48 }} spin />}
                >
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
                                onClick={openAddKey}
                            >
                                Add Key
                            </Button>
                        )}
                    />

                    <Pagination
                        style={{ marginTop: 15, textAlign: 'right' }}
                        current={page}
                        pageSize={pageSize}
                        total={total}
                        onChange={onPageChange}
                        showSizeChanger
                        align="end"
                    />
                </Spin>
            </Skeleton>

            {/* модалки */}
            <AddKeyModal
                open={isAddKeyOpen}
                onClose={closeAddKey}
                onSuccess={onSuccess}
            />
            <AddLanguageModal
                open={isAddLangOpen}
                onClose={closeAddLang}
                onSuccess={onSuccess}
            />
        </div>
    );
}
