# <img width="64" height="64" alt="hieroglyph" src="https://github.com/user-attachments/assets/b2325aca-214a-4ac1-8fa6-ddaeb81cd6c7" /> Localization Service
Сервис-локализатор для других проектов

## Технологический стек
- C# / .NET Core 8.0
- Entity Framework (PostgreSQL)
- Fluent Validation
- TypeScript / React + Next.js
- Ant Design (библиотека компонентов)

## Установка и запуск
```
# Клонирование репозитория
git clone https://github.com/smorodya74/LocalizationService/

# Переход в репозиторий
cd LocalizationService

# Сборка и запуск проекта в Docker (предварительно запустите Docker Desktop)
docker-compose up --build

# Откройте страницу http://localhost:3000
```

## Основные функции приложения
Ниже перечислены основные функции системы 

### 1. Добавление языка для перевода
Валидация:
- Код языка (ISO 3166-1) 2 символа, например: ru, en, cn;
- Название языка на латинице максимум 64 символа;
<img width="500" height="200" alt="AddLanguage" src="https://github.com/user-attachments/assets/94127d27-edb9-472d-9ea0-d3ca448ea2fb" />

### 2. Добавление ключа перевода
Валидация:
- Длинна ключа на латинице максимум 255 символов
<img width="500" height="200" alt="AddKey" src="https://github.com/user-attachments/assets/bb9eddea-5ba1-492a-ac6b-68095dae8140" />

### 3. Редактирование переводов в таблице
- При добавлении языка или ключа автоматически создаются пустые переводы;
<img width="1280" height="720" alt="LocalizationTable" src="https://github.com/user-attachments/assets/053a7c23-7e57-4a00-b81c-36e6b6da82f5" />

### 4. Поиск по ключу
- Поиск происходит по вхождению подстроки в строку с ключом (оператор ILike)
<img width="1280" height="720" alt="LocalizationTableSearch" src="https://github.com/user-attachments/assets/9d3bffb8-92ed-4650-ae33-cdcc1defb00a" />



Контакты: <br>
[![VK](https://img.shields.io/badge/VK-0077FF.svg?style=for-the-badge&logo=VK&logoColor=white)](https://vk.com/smorodya74)
[![Telegram](https://img.shields.io/badge/Telegram-2CA5E0?style=for-the-badge&logo=telegram&logoColor=white)](https://t.me/smorodya74)
[![Gmail](https://img.shields.io/badge/Gmail-EA4335.svg?style=for-the-badge&logo=Gmail&logoColor=white)](positivepel@gmail.com)
