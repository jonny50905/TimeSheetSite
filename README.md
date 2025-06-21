# TimeSheetSite

This repository contains a simple time tracking application with a .NET 8 backend and a Vue.js frontend.

## Backend

The API project is located in `TimeSheetApi` and uses:

- ASP.NET Core 8
- EF Core with SQL Server
- Controller based API returning JSON via `System.Text.Json`

To run the backend:

```bash
cd TimeSheetApi
dotnet run
```

## Frontend

The frontend is a minimal Vue 3 app powered by Vite. To start the development server:

```bash
cd frontend
npm install
npm run dev
```

## Database

The connection string is configured in `TimeSheetApi/appsettings.json`. By default it uses the local SQL Server instance `(localdb)`.
