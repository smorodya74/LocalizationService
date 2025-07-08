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
  const [code, setCode] = useState('');
  const [name, setName] = useState('');

  const handleOk = async () => {
    try {
      await axios.post('http://localhost:5172/api/languages', {
        languageCode: code.trim().toLowerCase(),
        name: name.trim()
      });
      message.success('Язык добавлен');
      setCode('');
      setName('');
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
      okButtonProps={{ disabled: code.trim().length !== 2 && name.trim().length == 0 }}
      onOk={handleOk}
      onCancel={onClose}
    >
      <Input
        placeholder="Код языка 2 буквы (ISO 3166-1)"
        maxLength={3}
        value={code}
        onChange={e => setCode(e.target.value)}
      />
      <Input
        placeholder="Название языка"
        maxLength={64}
        value={name}
        onChange={e => setName(e.target.value)}
        style={{ marginTop: 12 }}
      />
    </Modal>
  );
}
