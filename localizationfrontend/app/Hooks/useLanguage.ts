'use client';

import { useState, useEffect } from 'react';
import { getLanguages } from '@/app/Services/languageService';
import { message } from 'antd';

export function useLanguages() {
    const [languages, setLanguages] = useState<any[]>([]);
    const [loading, setLoading] = useState(false);

    const loadLanguages = async () => {
        setLoading(true);
        try {
            const langs = await getLanguages();
            setLanguages(langs);
        } catch {
            message.error('Не удалось получить список языков');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadLanguages();
    }, []);

    return { languages, loading, reload: loadLanguages };
}
