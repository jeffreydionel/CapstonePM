# CapstonePM

CapstonePM is the evolving COMP 3402 capstone application.

## Prerequisites

- .NET SDK selected for the official course repository
- Node.js and npm
- Git
- EF Core CLI tool compatible with the repository dependencies

## Architecture baseline

- `src/CapstonePM.Web` — React + TypeScript presentation and browser routing.
- `src/CapstonePM.Api` — ASP.NET Core HTTP/API boundary and composition root.
- `src/CapstonePM.Application` — application behavior and abstractions.
- `src/CapstonePM.Infrastructure` — EF Core persistence and infrastructure.

Dependency direction:

`Web --HTTP/JSON--> Api -> Application`

`Api -> Infrastructure -> Application`

`Application` does not reference EF Core or `Infrastructure`.

The server remains authoritative for validation, authorization, business rules,
state transitions, concurrency, and persistence as those concerns are introduced.

## API convention

Application HTTP endpoints use the `/api` base path.

Lesson 01 provides:

`GET /api/status`

This is a walking-skeleton endpoint, not the final production health subsystem.

## Configuration

Local development uses `src/CapstonePM.Api/appsettings.Development.json`.

Production-style configuration must be supplied externally. Do not commit
secrets. The database setting key is:

`ConnectionStrings__CapstonePm`

## Database setup

From the repository root:

    dotnet ef database update --project src/CapstonePM.Infrastructure --startup-project src/CapstonePM.Api

The first migration is `InitialCreate`.

## Build

From the repository root:

    dotnet restore
    dotnet build

Then build the React client:

    cd src/CapstonePM.Web
    npm ci
    npm run build
    cd ../..

## Test

From the repository root:

    dotnet test

## Run locally

Terminal 1:

    dotnet run --project src/CapstonePM.Api

Terminal 2:

    cd src/CapstonePM.Web
    npm run dev

Open the Vite development URL shown in Terminal 2.
The client calls the API through the `/api` development proxy.