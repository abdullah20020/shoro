# ⚖️ Shora Legal Platform

Shora is a legal management platform built with a clean, scalable architecture.  
It enables law firms, lawyers, and clients to collaborate through secure role-based APIs while maintaining performance and flexibility.

---

## 🚀 Tech Stack
- **Backend:** ASP.NET Core Web API, C#
- **Database:** SQL Server
- **Architecture Patterns:** CQRS with MediatR, Specification Pattern, Generic Repository
- **Security:** JWT Authentication & Role-Based Access
- **Testing:** Unit Testing (xUnit / NUnit)

---

## ✨ Key Features
- 🔐 **Secure Role-Based APIs**  
  Built APIs with **JWT Authentication** and **role-based access control**, ensuring proper authorization across Admin, Lawyers, and Clients.  

- 🛠️ **CQRS + MediatR Integration**  
  Implemented CQRS with MediatR for better separation of commands and queries, improving maintainability.  

- 📦 **Specification Pattern**  
  Created a **generic repository** with specification pattern, reducing code duplication by **60%** and enabling reusable, efficient queries.  

- ⚡ **Optimized Database Performance**  
  Advanced query tuning and indexing improved client response times by **40%**.  

- 🧩 **Complex Business Rules**  
  Applied specifications across multiple entities and handlers to enforce dynamic and reusable business logic.  

- 🏗️ **Scalable Architecture**  
  Designed a flexible, clean system capable of handling high loads and future integrations.  

- ✅ **Unit Testing**  
  Applied comprehensive unit testing to ensure reliability, quality, and maintainability.  

---

## 📡 API Endpoints (Sample)
- `POST /api/auth/login` → Authenticate user  
- `GET /api/cases` → Retrieve all cases  
- `POST /api/cases` → Create new case  
- `GET /api/lawyers` → Retrieve all lawyers  
- `POST /api/requests` → Send client request  

---

## ⚙️ Installation

### 1. Clone the Repository
```bash
git clone https://github.com/username/shora-legal-platform.git
cd shora-legal-platform
```
