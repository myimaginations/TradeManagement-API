# Trade Management System API

## Project Overview
The **Trade Management System API** is a secure backend system for managing trades, orders, portfolios, and instruments with **role-based authentication**. The API is built using **ASP.NET Core 8**, **MongoDB**, and **JWT authentication**.  

It supports multiple user roles (Admin, Trader, User) and implements **full CRUD operations** with proper authorization.

---

## Features

### 1. User Management
- Users can register with **username**, **password**, and **roles**.
- Supported roles:
  - **Admin** → Full CRUD access
  - **Trader** → Trade-related operations
  - **User** → Read-only access
- Only **one Admin** allowed.
- Users can have **multiple roles simultaneously**.
- JWT tokens include:
  - UserId
  - Username
  - Roles (array)

### 2. Authentication & Authorization
- **Login endpoint** returns JWT token.
- Authorization enforced using `[Authorize(Roles="...")]`.
- Swagger UI supports JWT authentication after entering the token.

### 3. CRUD Operations
All entities have full CRUD operations with role-based access:

- **Users**
  - Admin: full CRUD
  - Others: read-only

- **Trades**
  - Admin & Trader: full CRUD

- **Orders**
  - Admin & Trader: full CRUD

- **Instruments**
  - Admin & Trader: full CRUD

- **Portfolio**
  - Admin & Trader: full CRUD

### 4. Security
- **JWT-based authentication** (stateless)
- **Password hashing** for secure storage
- Role claims embedded in JWT for access control

### 5. Technical Stack
- **Backend:** ASP.NET Core 8 Web API
- **Database:** MongoDB Atlas
- **Authentication:** JWT
- **Authorization:** Role-based `[Authorize]`
- **API Testing:** Swagger UI / Postman
- **Design Pattern:** Repository Pattern + Service Layer + Controller Layer

---

## Getting Started

### Prerequisites
- .NET 8 SDK
- MongoDB (Atlas or local)
- Visual Studio / VS Code / Rider
- Postman or Swagger UI

### Configuration
1. Update `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "<YOUR_MONGODB_CONNECTION_STRING>",
    "DatabaseName": "TradeManagementDB",
    "UsersCollectionName": "Users",
    "TradesCollectionName": "Trades",
    "OrdersCollectionName": "Orders",
    "InstrumentsCollectionName": "Instruments",
    "PortfoliosCollectionName": "Portfolios"
  },
  "Jwt": {
    "Key": "<YOUR_SECRET_KEY>",
    "Issuer": "TradeManagementAPI",
    "Audience": "TradeManagementClient",
    "ExpireMinutes": 60
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:5173"]
  }
}

Running the API
# Restore packages
dotnet restore
# Build the project
dotnet build
# Run the API
dotnet run

Open Swagger UI at:
http://localhost:<PORT>/swagger/index.html

API Endpoints
Auth
POST /api/Auth/register → Register a new user
POST /api/Auth/login → Login and get JWT token

Users
GET /api/Users → Get all users (Admin full, others read-only)
GET /api/Users/{id} → Get user by ID
PUT /api/Users/{id} → Update user (Admin only)
DELETE /api/Users/{id} → Delete user (Admin only)
Trades / Orders / Instruments / Portfolio
Standard CRUD endpoints available

Role-based access using [Authorize(Roles="...")]

Notes
Admin has full control over all entities.
Traders can manage trade-related data.
Users have read-only access.
JWT tokens must be included in the Authorization header for protected routes:
Authorization: Bearer <JWT_TOKEN>

Future Improvements
Integration with real trading APIs
Frontend (React/Vue/Blazor) dashboards
Advanced reporting for trades and portfolio

License

This project is free to use educational purpose.
