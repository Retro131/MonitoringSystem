Веб-приложение для мониторинга активности пользователей и управления сессиями устройств.
Система позволяет просматривать список устройств, детализацию по сессиям, фильтровать данные и удалять устаревшие или выбранные записи.

Технологический стек

### Backend
* **Platform:** .NET 8 (ASP.NET Core Web API)
* **Database ORM:** Entity Framework Core
* **Architecture:** Repository Pattern
* **Features:** Async/Await, DTO Mapping, оптимизированные SQL-запросы via LINQ.

### Frontend
* **Framework:** Angular 19+ (Standalone Components)

### DevOps
* **Containerization:** Docker, Docker Compose

---

## Docker Compose

1.  **Сборка и запуск:**
    Выполните команду в корне проекта:
    ```bash
    docker-compose up -d --build
    ```

2.  **Доступ к приложению:**
    * **Frontend (UI):** [http://localhost:4200](http://localhost:4200)
    * **Backend (Swagger API):** [http://localhost:5236/swagger](http://localhost:5236/swagger)

3.  **Остановка:**
    ```bash
    docker-compose down
    ```

Строка подключения к бд конфигурируется в DockerCompose.

---

## Локальный запуск

Если нужно запустить части приложения по отдельности без контейнеров.

### 1. Запуск Backend
Необходимо наличие .NET SDK.

```bash
cd InfotecsBackend
dotnet restore
dotnet run
```
### 2. Запуск Frontend

Необходимо наличие Node.js.

```bash
cd InfotecsFrontend
npm install
npm start
```