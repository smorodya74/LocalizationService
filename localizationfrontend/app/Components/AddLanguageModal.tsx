'use client';

import { Modal, Input, message } from 'antd';
import axios from 'axios';
import { useState } from 'react';

interface Props {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
}

export default function AddLanguageModal({ open, onClose, onSuccess }: Props) {
  const [value, setValue] = useState('');

  const handleOk = async () => {
    try {
      await axios.post('http://localhost:5172/api/languages', {
        languageCode: value.trim().toLowerCase()
      });
      message.success('Язык добавлен');
      setValue('');
      onSuccess();
      onClose();
    } catch {
      message.error('Не удалось добавить язык');
    }
  };

  return (
    <Modal
      open={open}
      title="Новый язык"
      okText="Добавить"
      cancelText="Отменить"
      okButtonProps={{ disabled: value.trim().length !== 3 }}
      onOk={handleOk}
      onCancel={onClose}
    >
      <Input
        placeholder="Введите код языка..."
        value={value}
        onChange={e => setValue(e.target.value)}
        // minLength={2}
        // maxLength={3}
      />
    </Modal>
  );
}
