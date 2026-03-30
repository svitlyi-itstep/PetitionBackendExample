# PetitionBackendExample

Цей проєкт є прикладом backend-додатку, реалізованого на ASP.NET Core Web API. На поточному етапі він не містить повноцінної логіки роботи з петиціями, а слугує шаблоном для побудови серверної частини застосунку.

Реалізовано базову інфраструктуру для:

* організації REST API
* роботи з базою даних
* розділення логіки на шари (контролери, сервіси, моделі)
* подальшого масштабування (додавання бізнес-логіки, авторизації тощо)

Проєкт може використовуватись як стартова точка для розробки більш складних систем.

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
