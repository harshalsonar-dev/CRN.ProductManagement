# CRN Product Management API

A production-ready **ASP.NET Core 8 Web API** built using **Clean Architecture**, **Repository Pattern**, **Unit of Work**, **JWT Authentication**, **Role-Based Authorization**, **Refresh Token Authentication**, **Entity Framework Core**, **SQL Server**, and **xUnit Testing**.

This project was developed as part of the **CRN Technosoft .NET Technical Assessment** following industry best practices for scalability, maintainability, and clean code.

---

# Features

- ASP.NET Core 8 Web API
- Clean Architecture
- Entity Framework Core
- SQL Server
- Repository Pattern
- Unit of Work Pattern
- JWT Authentication
- Refresh Token Authentication
- Role-Based Authorization
- ASP.NET Identity
- AutoMapper
- FluentValidation
- Global Exception Middleware
- Serilog Logging
- Swagger/OpenAPI Documentation
- CRUD Operations
- Search
- Sorting
- Pagination
- API Versioning
- Unit Testing (xUnit + Moq)

---

# Technology Stack

| Technology | Version |
|------------|---------|
| .NET | 8 |
| ASP.NET Core Web API | 8 |
| Entity Framework Core | 8 |
| SQL Server | Latest |
| ASP.NET Identity | ✔ |
| JWT | ✔ |
| AutoMapper | ✔ |
| FluentValidation | ✔ |
| Serilog | ✔ |
| Swagger | ✔ |
| xUnit | ✔ |
| Moq | ✔ |

---

# Project Structure

```
CRN.ProductManagement

├── CRN.ProductManagement.API
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   └── appsettings.json
│
├── CRN.ProductManagement.Application
│   ├── Common
│   ├── DTOs
│   ├── Interfaces
│   ├── Mappings
│   ├── Services
│   ├── Validators
│   └── Extensions
│
├── CRN.ProductManagement.Domain
│   ├── Common
│   └── Entities
│
├── CRN.ProductManagement.Infrastructure
│   ├── Data
│   ├── Identity
│   ├── Services
│   ├── Extensions
│   └── Configurations
│
├── CRN.ProductManagement.Application.Tests
│
└── CRN.ProductManagement.API.Tests
```

---

# Architecture

```
Controller
      │
      ▼
Application Service
      │
      ▼
Unit Of Work
      │
      ▼
Repository
      │
      ▼
Entity Framework Core
      │
      ▼
SQL Server
```

---

# Authentication Flow

```
User Register
        │
        ▼
ASP.NET Identity

        │

User Login
        │
        ▼
Generate JWT Token
        │
        ▼
Generate Refresh Token
        │
        ▼
Save Refresh Token in Database

        │

Client Request
        │
        ▼
JWT Validation

        │

Expired Token
        │
        ▼
Refresh Token Endpoint
        │
        ▼
Generate New JWT
```

---

# Authorization

Two Roles are implemented.

### Admin

- Create Product
- Update Product
- Delete Product
- View Products

### User

- View Products Only

---

# API Endpoints

## Authentication

| Method | Endpoint |
|----------|---------------------------|
| POST | /api/Auth/register |
| POST | /api/Auth/login |
| POST | /api/Auth/refresh-token |
| POST | /api/Auth/logout |

---

## Product

| Method | Endpoint |
|----------|------------------------------|
| GET | /api/v1/Product |
| GET | /api/v1/Product/{id} |
| POST | /api/v1/Product |
| PUT | /api/v1/Product/{id} |
| DELETE | /api/v1/Product/{id} |

---

# Product Search

Example

```
GET

/api/v1/Product?Search=Laptop
```

---

# Pagination

Example

```
GET

/api/v1/Product?PageNumber=1&PageSize=10
```

---

# Sorting

Example

```
GET

/api/v1/Product?SortBy=Name&Descending=true
```

---

# Database

Tables

- AspNetUsers
- AspNetRoles
- AspNetUserRoles
- RefreshTokens
- Product
- Item

---

# Logging

Serilog is configured.

Logs are automatically generated inside

```
Logs/
```

Example

```
log-2026xxxx.txt
```

---

# Validation

Implemented using FluentValidation.

Example validations

- Product Name Required
- Email Required
- Password Required

---

# Exception Handling

Global Exception Middleware handles

- Validation Errors
- Server Errors
- Unhandled Exceptions

---

# Unit Testing

Frameworks Used

- xUnit
- Moq

Covered Tests

- Get Product By Id
- Create Product
- Delete Product
- Product Service Tests

---

# Swagger

Swagger UI is enabled.

```
https://localhost:{PORT}/swagger
```

---

# Setup Instructions

## Clone Repository

```bash
git clone https://github.com/YOUR_USERNAME/CRN.ProductManagement.git
```

---

## Navigate

```bash
cd CRN.ProductManagement
```

---

## Update Connection String

Edit

```
appsettings.json
```

Example

```json
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server Connection String"
}
```

---

## Apply Migration

```bash
Update-Database
```

or

```bash
dotnet ef database update
```

---

## Run

```bash
dotnet run
```

---

Open

```
https://localhost:xxxx/swagger
```

---

# Security

- JWT Authentication
- Refresh Token Rotation
- Role-Based Authorization
- ASP.NET Identity
- HTTPS
- Password Hashing

---

# Future Improvements

- Docker Support
- Docker Compose
- Redis Caching
- API Rate Limiting
- Response Compression
- Health Checks
- CI/CD Pipeline
- Azure Deployment

---

# Screenshots

Add the following screenshots before submission.

- Swagger Home
- Register API
- Login API
- JWT Authentication
- Product CRUD
- SQL Server Database
- Unit Test Results
- Logs Folder

---

# Author

**Harshal Sonar**

LinkedIn

https://www.linkedin.com/in/harshalsonar07

GitHub

https://github.com/sonarharshal0609-cyber

Portfolio

https://harshal-sonar-portfolio.netlify.app/

---

# License

This project was developed for the **CRN Technosoft Technical Assessment** and is intended for educational and evaluation purposes.
