# PetitionBackendExample

![Static Badge](https://img.shields.io/badge/Language-C%23-purple?style=for-the-badge)
![Static Badge](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge)
![Static Badge](https://img.shields.io/badge/Database-SQLite-003B57?style=for-the-badge)
![Static Badge](https://img.shields.io/badge/License-MIT-brightgreen?style=for-the-badge)

---

Цей проєкт є прикладом backend-додатку, реалізованого на ASP.NET Core Web API. На поточному етапі він не містить повноцінної логіки роботи з петиціями, а слугує шаблоном для побудови серверної частини застосунку.

Реалізовано базову інфраструктуру для:

* організації REST API
* роботи з базою даних
* розділення логіки на шари (контролери, сервіси, моделі)
* подальшого масштабування (додавання бізнес-логіки, авторизації тощо)

---

<p align="center"><i>Проєкт може використовуватись як стартова точка для розробки більш складних систем.</i></p>

---

## Використані технології

* C#
* ASP.NET Core Web API
* Entity Framework Core
* JWT Bearer
* Swagger (Swashbuckle)

---

## Структура проєкту

Основні частини проєкту:

* `/Controllers` – API контролери (обробка HTTP-запитів)
* `/Models` – моделі даних
* `/Services` – бізнес-логіка
* `/DbContexts` – контекст бази даних (DbContext) та робота з БД

---

## API

| Метод | Endpoint                | Авторизація                | Опис |
|------|------------------------|----------------------------|------|
| ![GET][get-shield]  | /api/user              | 🔒 Будь-який авторизований | Отримати дані поточного користувача |
| ![GET][get-shield]  | /api/user/me           | 🔒 Будь-який авторизований | Те ж саме, що і /api/user |
| ![GET][get-shield]  | /api/user/all          | 🔒 Admin, Moderator        | Отримати список всіх користувачів |
| ![GET][get-shield]  | /api/user/paged/{page} | 🔒 Admin, Moderator        | Отримати список користувачів з пагінацією |
| ![POST][post-shield] | /api/user              | 🔒 Admin                   | Створити нового користувача |
| ![POST][post-shield] | /api/user/register     | ❌ Без авторизації         | Реєстрація нового користувача (тільки роль User) |
| ![POST][post-shield] | /api/user/login        | ❌ Без авторизації         | Авторизація користувача (отримання JWT токенів) |
| ![POST][post-shield] | /api/user/refresh      | ❌ Без авторизації         | Оновлення AccessToken за допомогою RefreshToken |

---

## Розгортання додатку

### 1. Клонування репозиторію

```bash
git clone https://github.com/svitlyi-itstep/PetitionBackendExample.git
cd PetitionBackendExample
```

### 2. Встановлення залежностей

```bash
dotnet restore
```

### 3. Налаштування бази даних

Відредагуйте файл `appsettings.json`, вказавши коректний рядок підключення:

```json
"ConnectionStrings": {
  "DefaultConnection": "your_connection_string"
}
```

### 4. Міграції (якщо використовуються)

```bash
dotnet ef database update
```

### 5. Запуск

```bash
dotnet run
```

---

## Ліцензія

Проєкт розповсюджується під [ліцензією MIT](https://github.com/svitlyi-itstep/PetitionBackendExample/LICENSE.txt).


[get-shield]:https://img.shields.io/badge/GET-green?style=flat
[post-shield]:https://img.shields.io/badge/POST-blue?style=flat
