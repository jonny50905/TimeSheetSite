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

The frontend is a Vue 3 app powered by Vite with a simple UI for entering time entries
and viewing project reports. To start the development server:


```bash
cd frontend
npm install
npm run dev
```

Bootstrap is installed locally via npm and will be bundled automatically when running the dev or build commands.

## Database

The connection string is configured in `TimeSheetApi/appsettings.json`. By default it uses the local SQL Server instance `(localdb)`.


## Reports API

The backend exposes `/api/reports/projects` which returns aggregated hours,
cost, revenue and profit for each project based on submitted time entries.

### Customers API

Basic CRUD operations for customers are available at `/api/customers`.

