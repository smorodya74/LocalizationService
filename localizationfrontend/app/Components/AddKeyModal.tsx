'use client';

import { Modal, Input, message } from 'antd';
import axios from 'axios';
import { useState } from 'react';

interface Props {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
}

export default function AddKeyModal({ open, onClose, onSuccess }: Props) {
  const [value, setValue] = useState('');

  const handleOk = async () => {
    try {
      await axios.post('http://localhost:5172/api/localization-keys', {
        keyName: value.trim()
      });
      message.success('Ключ добавлен');
      setValue('');
      onSuccess();
      onClose();
    } catch {
      message.error('Не удалось добавить ключ');
    }
  };

  return (
    <Modal
      open={open}
      title="Новый ключ"
      okText="Добавить"
      cancelText="Отменить"
      okButtonProps={{ disabled: !value.trim() }}
      onOk={handleOk}
      onCancel={onClose}
    >
      <Input
        placeholder="Введите ключ..."
        value={value}
        onChange={e => setValue(e.target.value)}
        maxLength={256}
      />
    </Modal>
  );
}
