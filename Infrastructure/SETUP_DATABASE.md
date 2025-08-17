# Setting Up the Database Outside the Application

This guide explains how to set up and update the database for the PeopleSearch solution using Entity Framework Core migrations, **outside of the application**. Reference this document from your main README for onboarding or deployment instructions.

---

## Prerequisites

- .NET 8 SDK installed
- Access to the solution source code
- Database permissions as specified in `PeopleSearch/appsettings.json`

---

## Step-by-Step Instructions

### 1. Restore NuGet Packages
**Directory:** Solution root  
**Command:** dotnet restore

### 2. Build the Solution
**Directory:** Solution root  
**Command:** dotnet build

### 3. Apply Migrations to Create/Update the Database
**Directory:** Solution root  
**Command:**  dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project PeopleSearch/PeopleSearch.csproj


- This will create the database (if it does not exist) and apply all migrations.

### 4. (Optional) Add a Migration if Models Have Changed
**Directory:** Solution root  
**Command:**  dotnet ef migrations add MigrationName --project Infrastructure/Infrastructure.csproj --startup-project PeopleSearch/PeopleSearch.csproj

- Replace `MigrationName` with a descriptive name for your migration.

### 5. Verify the Database
**Directory:** N/A (use a database tool)  
- Use SQL Server Management Studio, Azure Data Studio, or another tool to connect to the database specified in `appsettings.json` and verify tables and seed data.

## Summary Table

| Step                | Directory to Run Command | Command/Action                                                                 |
|---------------------|-------------------------|-------------------------------------------------------------------------------|
| Restore packages    | Solution root           | `dotnet restore`                                                              |
| Build solution      | Solution root           | `dotnet build`                                                                |
| Apply migrations    | Solution root           | `dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project PeopleSearch/PeopleSearch.csproj` |
| Add migration (opt) | Solution root           | `dotnet ef migrations add MigrationName --project Infrastructure/Infrastructure.csproj --startup-project PeopleSearch/PeopleSearch.csproj` |
| Verify DB           | N/A                     | Use a database tool                                                           |

---

## Back to Solution Overview

Return to the main solution overview: [PeopleSearch/README.md](../README.md)
