# PeopleSearch Application

## Overview

PeopleSearch is a WinForms application that exemplifies clean architecture principles. It leverages .NET 8, Entity Framework Core, and dependency injection to ensure maintainable and testable code. Configuration and user messages are managed via messagesconfig.json and connection strings managed via appsettings.json, with database schema changes handled by EF Core migrations. This project was primarily developed with AI, under my guidance, which managed documentation, analysis, troubleshooting, and code generation.

With that said, I have not implemented Add/Save/Delete feature, Async methods, or implemented the 3NF person, address, city, state, zip tables but are included in the design.

I asked AI, once I was satisfied with the database schema, to generate Entities based off of that schema.

---

## Architectural Layout

- **Domain Layer**  
  Contains core business entities (e.g., `Person`) and interfaces.  
  Path: `Domain\Entities\Person.cs`

- **Application Layer**  
  Contains service interfaces and implementations (e.g., `IPeopleService`, `PeopleService`).  
  Path: `Application\Interfaces\IPeopleService.cs`, `Application\Services\PeopleService.cs`

- **Infrastructure Layer**  
  Contains data access logic, including the EF Core `AppDbContext` and design-time factory for migrations.  
  Path: `Infrastructure\Data\AppDbContext.cs`, `Infrastructure\Data\DesignTimeDbContextFactory.cs`

- **UI Layer (WinForms)**  
  Contains the main form (`Form1`) and UI logic.  
  Path: `PeopleSearch\Form1.cs`

- Notes:  It is important to note that while the UI layer references the layers, the Form does not interact
  directly with the `AppDbContext` or any repositories. Instead, it uses the `IPeopleService` interface to perform operations. This ensures that the UI remains decoupled from the data access layer and adheres to the principles of clean architecture. 
- The services in the `Application` layer handle the business logic and data access, while the UI layer focuses on presentation and user interaction.
, they interact with the `Application` layer services.

---

## Dependency Injection

- **Service Registration**  
  All services, including `IPeopleService` and `AppDbContext`, are registered in the DI container in `Program.cs`:

  
 
- 
- **Service Resolution**  
  The main form (`Form1`) receives its dependencies via constructor injection:

- **Scope Management**  
  Services are resolved within a DI scope, ensuring proper lifetime management:

---

## Configuration via appsettings.json

- **Location:**  
`PeopleSearch\appsettings.json`

- **Purpose:**  
Stores connection strings and user/validation messages. 
Example:

---

## Database Migrations

- **Purpose:**  
EF Core migrations manage schema changes over time, allowing you to update the database structure as your models evolve.

- **Design-Time Factory:**  
The `DesignTimeDbContextFactory` enables EF Core tools to create your `AppDbContext` at design time, using the connection string from `appsettings.json`.

- **Usage:**  
- Add a migration:
  ```
  dotnet ef migrations add MigrationName
  ```
- Apply migrations to the database:
  ```
  dotnet ef database update
  ```

- **Configuration:**  
Ensure your connection string in `appsettings.json` is correct and accessible to both the application and EF Core tools.

---

## Summary

- **Clean architecture** separates concerns for maintainability.
- **Dependency injection** provides flexible, testable service management.
- **appsettings.json** centralizes configuration and user messages.
- **EF Core migrations** keep your database schema in sync with your code.

For further details, see the code comments and referenced files in each layer.

## Database Setup

For instructions on setting up and updating the database outside the application, see [Infrastructure/SETUP_DATABASE.md](Infrastructure/SETUP_DATABASE.md).

## Analysis Overview
For anlaysis overview, see [Solution Architecture & Layer Dependency Analysis](PeopleSearch/AnalysisReadMe.md)



