![Build](https://github.com/alexfitzkane-02/dotnet-api-starter/actions/workflows/ci.yml/badge.svg)

# dotnet-api-starter

A clean, production-ready .NET 10 Web API template with JWT authentication, Azure Key Vault, rate limiting, EF Core, and Swagger pre-wired. Clone it and start building your own API without the boilerplate setup.

---

## 🚀 What's Included

- **JWT Authentication** — login, register, logout, and a `/me` endpoint with HTTP-only cookie token storage
- **Role-based Authorization** — Reader role assigned on registration, extendable to any roles you need
- **Azure Key Vault** — secrets (connection string, JWT key) loaded securely at runtime, nothing hardcoded
- **Entity Framework Core** — two DbContexts (Application + Auth) with SQL Server and migrations ready to go
- **Rate Limiting** — per-IP fixed window rate limiting on auth endpoints (60 requests/min, returns 429)
- **Swagger / OpenAPI** — interactive API docs available at the root URL in development
- **CORS** — pre-configured for a frontend running on `https://localhost:4200`
- **GitHub Actions CI/CD** — build and test pipeline runs automatically on every push

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core (.NET 10)
- **Database:** SQL Server with Entity Framework Core
- **Authentication:** JWT Bearer tokens via HTTP-only cookies
- **Identity:** ASP.NET Core Identity
- **Secrets:** Azure Key Vault
- **API Docs:** Swagger / Swashbuckle
- **CI/CD:** GitHub Actions

---

## 📁 Project Structure

```
dotnet-api-starter/
├── Controllers/
│   └── AuthenticationController.cs   # Login, Register, Logout, Me
├── Data/
│   ├── ApplicationDbContext.cs        # Main DB context for your entities
│   ├── AuthDbContext.cs               # Identity DB context
│   └── Migrations/                   # EF Core migrations
├── Models/
│   ├── Domain/                        # Add your domain models here
│   └── Dto/                           # Request/Response DTOs
│       ├── LoginRequestDto.cs
│       ├── LoginResponseDto.cs
│       └── RegisterRequestDto.cs
├── Services/
│   ├── Interfaces/
│   │   └── ITokenInterface.cs
│   └── TokenService.cs               # JWT token generation
├── appsettings.json
└── Program.cs
```

---

## ⚙️ Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or Azure)
- Azure Key Vault (or use dotnet user-secrets for local dev)

### 1. Clone the repo
```bash
git clone https://github.com/alexfitzkane-02/dotnet-api-starter.git
cd dotnet-api-starter
```

### 2. Set up Azure Key Vault

Add your Key Vault name to `appsettings.json`:
```json
{
  "KeyVault": {
    "KeyVaultName": "your-key-vault-name"
  },
  "JwtSettings": {
    "Issuer": "https://localhost:7000",
    "Audience": "https://localhost:4200"
  }
}
```

Add these secrets to your Key Vault:

| Secret Name | Description |
|-------------|-------------|
| `YourSQLConnecitonFromAzureVault` | Your SQL Server connection string |
| `YourJwtKeyFromAzureVault` | A secure random string for signing JWT tokens |

### 3. For local development (without Key Vault)

Use dotnet user-secrets instead:
```bash
dotnet user-secrets init
dotnet user-secrets set "YourSQLConnecitonFromAzureVault" "your-connection-string"
dotnet user-secrets set "YourJwtKeyFromAzureVault" "your-jwt-secret-key"
```

### 4. Apply migrations
```bash
dotnet ef database update
```

### 5. Run the API
```bash
dotnet run
```

Navigate to `https://localhost:{port}` to open Swagger and explore the endpoints.

---

## 🔗 API Endpoints

### `POST /api/authentication/register`
Register a new user. Assigned the **Reader** role by default.

**Request body:**
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```
**Response `200 OK`**

---

### `POST /api/authentication/login`
Authenticate and receive a JWT stored in an HTTP-only cookie.

**Request body:**
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```
**Response `200 OK`:**
```json
{
  "email": "user@example.com",
  "roles": ["Reader"]
}
```

---

### `GET /api/authentication/me` 🔒
Returns the currently authenticated user's details.

**Response `200 OK`:**
```json
{
  "email": "user@example.com",
  "roles": ["Reader"]
}
```

---

### `POST /api/authentication/logout`
Clears the JWT cookie and logs the user out.

**Response `200 OK`**

---

## 🔒 Adding Your Own Endpoints

1. Add your domain models to `Models/Domain/`
2. Add your DTOs to `Models/Dto/`
3. Add your DbSets to `ApplicationDbContext.cs`
4. Create a repository interface in `Services/Interfaces/`
5. Implement the repository in `Services/`
6. Register it in `Program.cs` with `builder.Services.AddScoped<>`
7. Create your controller in `Controllers/`

To protect an endpoint with JWT auth add `[Authorize]` to the controller or method. To restrict by role use `[Authorize(Roles = "YourRole")]`.

---

## 🔐 Security Notes

- JWT tokens are stored in **HTTP-only cookies** — not accessible via JavaScript
- Rate limiting is set to **60 requests per minute per IP** on auth endpoints — adjust in `Program.cs`
- Secrets are loaded from **Azure Key Vault** at runtime — nothing sensitive in source control
- CORS is configured for `https://localhost:4200` — update this in `Program.cs` for your frontend URL

---

## Contributing
Pull requests are welcome. For major changes please open an issue first.
