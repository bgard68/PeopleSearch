# PeopleSearch Application

## Solution Architecture & Layer Dependency Analysis

PeopleSearch is a WinForms application built with .NET 8, following clean architecture principles. The solution is organized into four main layers, each with clear responsibilities and dependencies:

---

### Layer Overview

| Layer           | References Domain | References Application | References Infrastructure | Status      |
|-----------------|------------------|-----------------------|--------------------------|-------------|
| **Domain**      | ✅                | ❌                    | ❌                       | Correct     |
| **Application** | ✅                | ✅                    | ❌                       | Correct     |
| **Infrastructure** | ✅             | ❌                    | ✅ (itself)               | Correct     |
| **UI (WinForms)** | ✅ (via App)    | ✅                    | ❌                       | Correct     |

---

### Layer Details

- **Domain Layer**
  - Contains core business entities (`Person`) and interfaces (`IPerson`, `IPeopleRepository`).
  - Only references other domain types.
  - No improper references.

- **Application Layer**
  - Contains service interfaces (`IPeopleService`) and implementations (`PeopleService`).
  - Depends on domain abstractions (`IPeopleRepository`), not on infrastructure or EF Core directly.
  - No improper references.

- **Infrastructure Layer**
  - Contains data access logic (`AppDbContext`, `PersonRepository`).
  - Implements domain interfaces and uses domain entities.
  - No improper references.

- **UI Layer (WinForms)**
  - Uses dependency injection to resolve `IPeopleService` and passes it to the form.
  - Does not directly interact with `AppDbContext` or repositories.
  - No improper references.

---

### Dependency Injection

- All services, including `IPeopleService`, `IPeopleRepository`, and `AppDbContext`, are registered in the DI container in `Program.cs`.
- The main form (`Form1`) receives its dependencies via constructor injection, ensuring decoupling from data access and infrastructure.

---

### Configuration

- Application configuration (connection strings, messages) is managed via `appsettings.json`.

---

### Database Migrations

- EF Core migrations are used to manage schema changes.
- The database is created and updated at startup using migrations.

Back to Solution Overview
Return to the main solution overview: PeopleSearch/README.md

---

## Summary

- **No layer references something it shouldn’t.**
- The solution adheres to clean architecture principles, ensuring maintainability, testability, and separation of concerns.

For further details, see the code comments and referenced files in each layer.

---

## Back to Solution Overview

Return to the main solution overview: [PeopleSearch/README.md](../README.md)
