## Page 1

Microservices Test  Тестовое   задание   —   миĸросервисная   система   на   .NET 8   с   Clean Architecture   и   CQRS.  Стеĸ  Языĸ :   C# / .NET 8  Архитеĸтура :   Clean Architecture + CQRS (MediatR)  БД :   PostgreSQL + Entity Framework Core  Аутентифиĸация :   JWT (Access + Refresh   тоĸены )  Gateway:   YARP  Доĸументация :   Swagger / OpenAPI  Струĸтура   проеĸта  Microservices_Test/  ├──   UserService/   #   Микросервис   пользователей  │   ├──   UserService.Api/   #   Контроллеры , Program.cs  │   ├──   UserService.Application/   #   Команды ,   запросы ,   хендлеры , DTO  │   ├──   UserService.Domain/   #   Сущности  │   └──   UserService.Infrastructure/ # DbContext, JWT,   миграции  │  ├──   FinanceService/   #   Микросервис   финансов  │   ├──   FinanceService.Api/   #   Контроллеры , Program.cs  │   ├──   FinanceService.Application/ #   Команды ,   запросы ,   хендлеры , DTO  │   ├──   FinanceService.Domain/   #   Сущности  │   └──   FinanceService.Infrastructure/ # DbContext, Refit   клиент ,   миграции  │  ├──   BackgroundRateService/   #   Фоновый   сервис   курсов   валют   ( ЦБ   РФ )  │   ├──   BackgroundRateService.Background/  │   ├──   BackgroundRateService.Application/  │   ├──   BackgroundRateService.Domain/  │   └──   BackgroundRateService.Infrastructure/  │  ├──   MigrationService.Api/   #   Сервис   применения   миграций  ├──   ApiGateway/   # API Gateway (YARP)

---

## Page 2

│  ├──   UserService.Tests/   # Unit   тесты   UserService  ├──   FinancialService.Tests/   # Unit   тесты   FinanceService  │  └──   docker-compose.yml   #   Запуск   всего   одной   командой  Быстрый   старт   (Docker)  Требования  Docker Desktop  Запусĸ  #   Клонировать   репозиторий  git clone <repository-url>  cd Microservices_Test  #   Запустить   все   сервисы  docker-compose up --build  После   запусĸа   автоматичесĸи :  1 .   Поднимается   PostgreSQL  2 .   Применяются   миграции   ( создаются   таблицы )  3 .   Фоновый   сервис   загружает   ĸурсы   валют   с   ЦБ   РФ  4 .   Запусĸаются   все   миĸросервисы   и   Gateway  Адреса   сервисов  Сервис   URL  API Gateway   http://localhost:5000  UserService Swagger   http://localhost:5001/swagger  FinanceService Swagger   http://localhost:5002/swagger

---

## Page 3

Запусĸ   без   Docker ( лоĸально )  Требования  .NET 8 SDK  PostgreSQL ( лоĸально   или   через   Docker)  Шаги  1.   Поднять   PostgreSQL:  docker run -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:16  2.   Создать   базы   данных :  CREATE DATABASE userservice;  CREATE DATABASE financeservice;  3.   Применить   миграции   ( запустить   MigrationService):  cd MigrationService.Api  dotnet run  4.   Запустить   сервисы   в   порядĸе :  #   Терминал   1  cd BackgroundRateService.Background && dotnet run  #   Терминал   2  cd UserService.Api && dotnet run  #   Терминал   3  cd FinanceService.Api && dotnet run  #   Терминал   4  cd ApiGateway && dotnet run  API Endpoints  UserService ( через   Gateway: http://localhost:5000)

---

## Page 4

Метод   URL   Описание   Auth  POST   /api/users/register   Регистрация  POST   /api/users/auth   Вход  POST   /api/users/refresh   Обновление   тоĸена  POST   /api/users/logout   Выход  GET   /api/users/{id}   Получить   пользователя  POST   /api/users/update   Обновить   пользователя  FinanceService ( через   Gateway: http://localhost:5000)  Метод   URL   Описание   Auth  GET   /api/v1/financial/favourite-rates/{userId}   Курсы   валют   пользователя  Сценарий   использования  1.   Зарегистрироваться :  POST /api/users/register  { "name": "Pavel", "password": "secret123", "favourites": "USD, EUR, CNY" }  2.   Войти :  POST /api/users/auth  { "name": "Pavel", "password": "secret123" }  →   Получить   AccessToken   в   body, RefreshToken   в   httpOnly cookie  3.   Получить   курсы   валют :  GET /api/v1/financial/favourite-rates/{userId}  Authorization: Bearer <AccessToken>  →   Вернёт   курсы   избранных   валют   пользователя   из   ЦБ   РФ  4.   Обновить   токен :  POST /api/users/refresh  →   Cookie   с   RefreshToken   передаётся   автоматически  5.   Выйти :  POST /api/users/logout  Authorization: Bearer <AccessToken>

---

## Page 5

Тесты  dotnet test  Поĸрыты   unit- тестами :  UserService   ĸонтроллер   ( регистрация ,   вход ,   выход , refresh, getById)  FinanceService   ĸонтроллер   ( получение   ĸурсов )  Архитеĸтурные   решения  Clean Architecture   —   ĸаждый   миĸросервис   разделён   на   слои :  Domain   —   сущности ,   без   зависимостей  Application   —   бизнес - логиĸа , CQRS   хендлеры ,   интерфейсы  Infrastructure   —   реализации   ( БД , JWT,   внешние   сервисы )  Api   —   ĸонтроллеры ,   точĸа   входа  CQRS   —   ĸоманды   ( изменение   состояния )   и   запросы   ( чтение )   разделены .   Реализовано  через   MediatR   без   отдельных   баз   для   чтения / записи .  JWT Auth   — Access   тоĸен   (15   мин )   передаётся   в   заголовĸе , Refresh   тоĸен   (7   дней )  хранится   в   httpOnly cookie   и   в   БД .   При   logout Refresh   отзывается   из   БД .  API Gateway   —   единая   точĸа   входа ,   валидирует   JWT   и   проĸсирует   запросы   ĸ  сервисам .

