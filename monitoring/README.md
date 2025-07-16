# Monitoring Setup

This directory contains the configuration for monitoring your calculation app with Grafana and Loki.

## Components

- **Loki**: Log aggregation system
- **Promtail**: Log shipper that sends logs to Loki
- **Grafana**: Visualization and dashboards

## Getting Started

1. Start the full stack:
   ```bash
   docker-compose up -d
   ```

2. Access Grafana:
   - URL: http://localhost:3001
   - Username: admin
   - Password: admin123

3. Access Loki directly:
   - URL: http://localhost:3100

## What You'll See

- **Calculation Requests Over Time**: Timeline of your API calls
- **Calculation Types**: Pie chart showing intersection vs union calculations
- **Recent Calculation Logs**: Live stream of your application logs
- **Error Rate**: Percentage of failed calculations
- **Container Logs**: Docker container logs for debugging

## Log Sources

- Application logs from `/logs/*.log` (your CalculationLogger output)
- Docker container logs from all services
- System logs (if needed)

## Queries

Example Loki queries you can use:

```logql
# All application logs
{job="app-calculations"}

# Only error logs
{job="app-calculations", level="ERROR"}

# Container logs for backend
{job="containerlogs", container_name=~".*backend.*"}

# Count calculations in last hour
count_over_time({job="app-calculations"}[1h])
```

## Customization

- Edit dashboards in Grafana UI or modify JSON files in `dashboards/`
- Update Promtail config in `promtail-config.yaml` to add new log sources
- Modify Loki config in `loki-config.yaml` for retention policies
