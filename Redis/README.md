# ApiRedisSample (.NET 8 + SQLite + Redis)

API de exemplo em C# com cache Redis e banco local SQLite.

## Requisitos
- .NET 8 SDK
- Docker (para subir um Redis rapidamente)

## Rodando o Redis
```bash
docker run -d --name redis -p 6379:6379 redis
```

## Rodando a API
```bash
dotnet restore
dotnet run
```
Acesse: `https://localhost:7165/swagger` (ou `http://localhost:5165/swagger`).

## Teste
1. `GET /api/product/1` → primeira chamada busca no banco e inclui no Redis.
2. `GET /api/product/1` → segunda chamada retorna do Redis (cache hit).
