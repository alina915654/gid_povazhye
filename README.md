# Гид Поважья

Веб-приложение — туристический гид по региону **Поважье**.  
Позволяет находить интересные места (еда, ночлег, достопримечательности), читать отзывы и оставлять заявки на персональный маршрут.

Разработано на **ASP.NET Core MVC** (.NET 10) с использованием Entity Framework Core и SQL Server.

## Возможности

- Каталог мест (еда, ночлег, достопримечательности)
- Поиск по названию и фильтрация по типу
- Подробная информация о месте (адрес, координаты, часы работы, контакты)
- Удобства: Wi-Fi, парковка, оплата картой, гид/экскурсия, эко-объект
- Загрузка фотографий мест
- Отзывы пользователей
- Заявки на индивидуальный маршрут / услугу
- Простая админ-панель (просмотр и удаление заявок и отзывов)

## Скриншоты

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (рекомендуется LocalDB или SQL Server Express)
- Visual Studio 2022 / VS Code / Rider (по желанию)

## Установка и запуск

### 1. Клонирование репозитория

```bash
git clone https://github.com/alina915654/gid_povazhye.git
cd gid_povazhye
```
2. Настройка строки подключения
Откройте файл appsettings.json и при необходимости измените строку подключения:
```JSON
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TourGuideDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;"}
```
Примеры других строк подключения:

```SQL Server Express:
Server=localhost\\SQLEXPRESS;Database=TourGuideDb;Trusted_Connection=True;TrustServerCertificate=True;
```
```Обычный SQL Server:
Server=ИМЯ_СЕРВЕРА;Database=TourGuideDb;Trusted_Connection=True;TrustServerCertificate=True;
```

При первом запуске приложение автоматически создаст базу данных и применит миграции (context.Database.Migrate()).

3. Запуск
```Bash
dotnet restore
dotnet run
```
После запуска откройте в браузере адрес, который появится в консоли
(обычно https://localhost:7xxx или http://localhost:5xxx).
По умолчанию открывается страница со списком мест (/Places).

## Использование
# Для посетителей
- Просмотр и поиск мест
- Фильтрация по типу (Еда / Ночлег / Достопримечательность)
- Просмотр подробной информации о месте
- Оставление отзыва
- Оформление заявки на маршрут

# Для администратора
Админ-панель доступна по адресу:
text/Places/AdminPanel
Доступ защищён cookie IsAdmin.
После входа в админку можно просматривать и удалять заявки и отзывы.
Структура проекта
textgid_povazhye/
├── Controllers/          # PlacesController, HomeController
├── Data/                 # AppDbContext
├── Migrations/           # Миграции Entity Framework
├── Models/               # Place, ServiceOrder, SiteReview, AdminViewModel
├── Views/                # Razor-представления
├── wwwroot/              # CSS, JS, изображения
│   └── images/           # Загруженные фото мест
├── Program.cs            # Точка входа и настройка
└── appsettings.json      # Строка подключения к БД

## Используемые технологии
- ASP.NET Core MVC (.NET 10)
- Entity Framework Core + SQL Server
- Bootstrap / HTML / CSS / JavaScript
- LocalDB / SQL Server

## Автор
alina915654
