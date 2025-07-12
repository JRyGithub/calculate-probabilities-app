# Calculate Probabilities App

A full-stack application for calculating probabilities with a React frontend and .NET backend.

## Project Structure

```
calculate-probabilties-app/
├── frontend-calc-prob-app/     # React + Vite frontend
├── backend-calc-prob-app/      # .NET Web API backend
├── docker-compose.yml          # Docker orchestration
└── README.md
```

## Running with Docker

### Prerequisites
- Docker
- Docker Compose

### Quick Start

1. **Build and run all services:**
   ```bash
   docker-compose up --build
   ```

2. **Run in detached mode:**
   ```bash
   docker-compose up -d --build
   ```

3. **Stop all services:**
   ```bash
   docker-compose down
   ```

### Access the Application

- **Frontend:** http://localhost:3000
- **Backend API:** http://localhost:5000

### Development

**Build individual services:**
```bash
# Build frontend only
docker-compose build frontend

# Build backend only
docker-compose build backend
```

**View logs:**
```bash
# All services
docker-compose logs

# Specific service
docker-compose logs frontend
docker-compose logs backend
```

**Restart a service:**
```bash
docker-compose restart frontend
docker-compose restart backend
```

## Local Development (without Docker)

### Frontend
```bash
cd frontend-calc-prob-app
npm install  # or bun install
npm run dev  # or bun dev
```

### Backend
```bash
cd backend-calc-prob-app
dotnet restore
dotnet run
```

## Environment Variables

### Frontend
- `VITE_API_URL`: Backend API URL (default: http://localhost:5000)

### Backend
- `ASPNETCORE_ENVIRONMENT`: Environment (Development/Production)
- `ASPNETCORE_URLS`: Binding URLs
