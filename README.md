# ProductApi

Minimal ASP.NET Core Web API for product details with SQL Server, Swagger, health checks, and AKS manifests.

## Endpoints

- `GET /api/products/{id}` - returns product detail by ID
- `GET /health` - health endpoint for liveness/readiness
- `GET /swagger` - Swagger UI

## Configuration

Connection string is configurable via:

- `ConnectionStrings:DefaultConnection` in `appsettings*.json`
- `ConnectionStrings__DefaultConnection` environment variable (used in AKS)

## Run locally

```bash
dotnet run --project ProductApi.csproj
```

## Build Docker image

From the `DummyDataApi` folder:

```bash
docker build -t your-dockerhub-username/productapi:latest .
docker push your-dockerhub-username/productapi:latest
```

## Deploy to AKS

1. Update image and SQL connection string in `k8s/deployment.yaml`.
2. Apply manifests:

```bash
kubectl apply -f k8s/deployment.yaml
kubectl apply -f k8s/service.yaml
```

3. Check status:

```bash
kubectl get pods
kubectl get svc productapi-service
```