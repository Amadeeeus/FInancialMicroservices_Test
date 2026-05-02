# Microservices Test

Microservices system on .NET 8 with Clean Architecture and CQRS.

## Stack

- C# / .NET 8
- Clean Architecture + CQRS (MediatR)
- PostgreSQL + Entity Framework Core
- JWT Authentication (Access + Refresh tokens)
- YARP API Gateway
- Swagger / OpenAPI

## Quick Start (Docker)

```bash
git clone <repository-url>
cd Microservices_Test
docker-compose up --build
```

On startup: PostgreSQL starts, migrations apply, exchange rates load from CBR, all services start.

| Service | URL |
|---------|-----|
| API Gateway | http://localhost:5000 |
| UserService Swagger | http://localhost:5001/swagger |
| FinanceService Swagger | http://localhost:5002/swagger |

## Manual Start

**1. Start PostgreSQL:**
```bash
docker run -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:16
```

**2. Create databases:**
```sql
CREATE DATABASE userservice;
CREATE DATABASE financeservice;
```

**3. Run in order:**
```
MigrationService -> BackgroundRateService -> UserService -> FinanceService -> ApiGateway
```

## API Endpoints

### UserService

| Method | URL | Auth |
|--------|-----|------|
| POST | /api/users/register | No |
| POST | /api/users/auth | No |
| POST | /api/users/refresh | No |
| POST | /api/users/logout | Yes |
| GET | /api/users/{id} | Yes |
| POST | /api/users/update | Yes |

### FinanceService

| Method | URL | Auth |
|--------|-----|------|
| GET | /api/v1/financial/favourite-rates/{userId} | Yes |

## Usage Scenario

```
1. Register:
POST /api/users/register
{ "name": "Pavel", "password": "secret123", "favourites": "USD, EUR, CNY" }

2. Login:
POST /api/users/auth
{ "name": "Pavel", "password": "secret123" }
-> AccessToken in body, RefreshToken in httpOnly cookie

3. Get rates:
GET /api/v1/financial/favourite-rates/{userId}
Authorization: Bearer <AccessToken>

4. Refresh: POST /api/users/refresh

5. Logout: POST /api/users/logout
```

## Tests

```bash
dotnet test
```

## Architecture

- Clean Architecture: Domain -> Application -> Infrastructure -> Api
- CQRS: Commands and queries separated via MediatR
- JWT: Access token (15 min) in header, Refresh token (7 days) in httpOnly cookie + DB
- API Gateway: Single entry point, validates JWT, proxies requests
