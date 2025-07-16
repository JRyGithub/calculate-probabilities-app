# Coming Soon

## Setup - Docker

### Requirements

- docker

1 - `docker compose up -d`

You should be able to access:

Frontend: <http://localhost:3000/>
Backend: <http://localhost:5000/swagger>
Logs: <http://localhost:3001/>

## Setup - Local

### Requirements

- npm
- bun
- dotnet 9.0
- node.js ~22 (LTS)

#### Frontend

1- `cd frontend-calc-prob-app`
2 - `Bun install`
3 - `Bun run dev`

##### Frontend Tests

1- `cd frontend-calc-prob-app`
2 - `Bun run test`

#### Backend

1- `cd backend-calc-prob-app`
2 - `dotnet build`
3 - `dotnet run`

##### Backend Tests

1 - `cd backend-calc-prob-app.Tests`
2 - `dotnet test`

### Future

=> database
=> auth
