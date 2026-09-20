# Mini Social App Backend

A simple C# console application backend for a mini social app, using raw ADO.NET (`Npgsql`) against a PostgreSQL database. Built as a learning project focused on ADO.NET fundamentals, relational database design, and transaction handling.

## Features

- **User registration** — creates a new user with a hashed password
- **User login** — verifies credentials against the stored password hash
- **List users** — view all registered users except the currently logged-in one
- **Friends system** — add a friend directly (no request/approval flow); friendships are symmetric (if A adds B, B automatically has A as a friend too)

## Tech Stack

- **C# / .NET** — application logic
- **ADO.NET** (`System.Data`, `Npgsql`) — direct database access, no ORM
- **PostgreSQL 16** — relational database, run via Docker
- **Docker Compose** — reproducible local database environment

## Project Structure

```
├── docker-compose.yml       # Local Postgres environment definition
├── Program.cs                # Entry point / console interaction
├── Data/
│   └── Database.cs           # Connection factory (Database.Open())
├── Repositories/
│   ├── UserRepository.cs     # Register, Login, GetAllUsersExcept
│   └── FriendRepository.cs   # AddFriend
```

## Database Schema

**Users**
- `UserId` (serial, primary key)
- `UserName`
- `PasswordHash`
- `FirstName`
- `LastName`
- `DateOfBirth`

**Friends**
- `UserId`, `FriendUserId` — composite primary key
- `CreatedAt` — timestamp, defaults to current time
- Foreign keys to `Users(UserId)` on both columns, `ON DELETE CASCADE`
- Each friendship is stored as **two rows** (one per direction) to keep the relationship symmetric

## Getting Started

### 1. Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (with Docker Compose)

### 2. Start the database

From the repository root (same folder as `docker-compose.yml`):

```bash
docker-compose up -d
```

This starts a PostgreSQL 16 container named `app-pg`, listening on `localhost:5433`, with a persistent volume (`apppg-pgdata`) so data survives container restarts.

Confirm it's running:

```bash
docker ps
```

### 3. Run the application

```bash
dotnet run
```

### 4. Connect a database client (optional)

To inspect the database directly (e.g. via DataGrip, DBeaver, pgAdmin), create a new PostgreSQL connection with:

| Setting  | Value       |
|----------|-------------|
| Host     | localhost   |
| Port     | 5433        |
| Username | admin       |
| Password | admin1234   |
| Database | AppPgDb     |

Or connect via `psql` inside the container:

```bash
docker exec -it app-pg psql -U admin -d AppPgDb
```

## Notes

- The container is disposable — deleting and recreating `app-pg` (e.g. via `docker-compose up -d` after a `docker rm`) does not affect stored data, since data lives in the `apppg-pgdata` volume, not the container itself.
- This project is for learning purposes; password hashing and other security practices are simplified and **not** production-ready.
