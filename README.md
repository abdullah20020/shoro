Shora - Legal Platform API
Overview

Shora is a comprehensive legal platform designed to connect clients with lawyers and law firms. The system provides full management of legal cases, comments, and subscriptions.

Key Features
🔐 Authentication & Authorization

Secure login using JWT

Advanced role system (Client, Lawyer, Law Firm, Admin)

API endpoints protection

📋 Legal Case Management

Create, update, and delete cases

Categorize cases by type and status

Track the status of each case

👥 User Management

Clients: Can create and track their cases

Lawyers: Can manage assigned cases

Law Firms: Full team and case management

Admins: Full system privileges

💬 Comment System

Add comments on cases

Track comment history

📊 Subscription System

Manage subscriptions

Track subscription status

Technical Architecture
🏗️ Clean Architecture

The project follows Clean Architecture principles with separate layers:

├── DomainLayer/          # Domain Layer (Entities, Contracts)
├── ApplicationLayer/     # Application Layer (Commands, Queries, Services)
├── InfrastructureLayer/  # Infrastructure Layer (Database, Repositories)
└── shora/               # Presentation Layer (Controllers, DTOs)

🛠️ Technologies Used

.NET 9.0 - Core framework

ASP.NET Core Web API - API development

Entity Framework Core - ORM for database operations

SQL Server - Database

MediatR - CQRS pattern

AutoMapper - Data mapping

JWT Authentication - Authentication

ASP.NET Core Identity - User management

Swagger/OpenAPI - API documentation

📦 CQRS Pattern

Commands: Operations that modify data (Create, Update, Delete)

Queries: Data retrieval (GetAll, GetById)

Handlers: Process commands and queries

🔄 Generic Repository Pattern

Generic interfaces for data access

Automatic registration of handlers for each entity

Installation & Running
Requirements

.NET 9.0 SDK

SQL Server

Visual Studio 2022 or VS Code

Installation Steps

Clone the project

git clone https://github.com/abdullah20020/shoro.git
cd shoro


Restore packages

dotnet restore


Setup database

Update the connection string in appsettings.json

Run the migrations:

dotnet ef database update


Run the project

dotnet run


Access the API

Swagger UI: https://localhost:7000/swagger

API Base URL: https://localhost:7000/api

Project Structure
DomainLayer
├── Entities/           # Core entities
│   ├── BaseClass.cs
│   ├── BaseUser.cs
│   ├── Case.cs
│   ├── Comment.cs
│   ├── Post.cs
│   └── Users/         # User types
├── Enums/             # Enumerations
├── ValueObjects/      # Value objects
└── Contract/          # Interfaces

ApplicationLayer
├── Features/          # Features
│   ├── Auth/         # Authentication
│   ├── Command/      # Commands
│   └── Query/        # Queries
├── Services/         # Services
├── DTOs/             # Data Transfer Objects
└── Extensions/       # Extensions

InfrastructureLayer
├── DbContext/        # Database context
├── GenericRepository/ # Generic repository
└── Migrations/       # Database migrations

API Endpoints
Authentication

POST /api/auth/register - Register a new user

POST /api/auth/login - Login

Cases

GET /api/cases - Get all cases

GET /api/cases/{id} - Get a specific case

POST /api/cases - Create a new case

PUT /api/cases/{id} - Update a case

DELETE /api/cases/{id} - Delete a case

Security

JWT Authentication: Secure authentication using JWT tokens

Role-based Authorization: Role-based access control

Password Hashing: Password encryption

HTTPS: Encrypted communication

Contribution

Fork the project

Create a new branch (git checkout -b feature/AmazingFeature)

Commit your changes (git commit -m 'Add some AmazingFeature')

Push to the branch (git push origin feature/AmazingFeature)

Open a Pull Request

License

This project is licensed under the MIT License. See LICENSE
 for details.

Contact

Developer: Abdullah

GitHub: @abdullah20020

Project: Shora Repository

Future Development

[❌] Add notifications system

[❌] Electronic payment system <Payment_getway>

[❌] Ratings & reviews system

[❌] File and document support

[❌] OAuth2.0

[❌] Hangfire

[❌] chat <SignalR> 



Note: This project is under active development. Some features may contain bugs or need improvements.
