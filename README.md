# Loan Management System - Backend (API)

API REST desarrollada con **.NET 10** bajo **Arquitectura Hexagonal**, integrada con persistencia local facilitada.

## 🚀 Tecnologías y Arreglos Críticos
- **.NET 10** (Sincronizado para máxima compatibilidad).
- **SQLite**: Implementado por defecto (`LoanManagement.db`) para ejecución inmediata sin dependencias externas.
- **JWT (Bearer)**: Sistema de autenticación real con roles (ADMIN/USER).
- **OpenApi (Swagger)**: Configuración optimizada para evitar conflictos con el SDK 11.

## 🏗️ Cambios Recientes
- **AuthController**: Endpoint `/api/auth/login` real integrado.
- **Data Seeding**: Carga automática de usuarios de prueba al iniciar.
- **Port Matching**: Backend configurado en el puerto **5000** fijo.

## 🏃 Ejecución Local
```bash
dotnet run --project src/Api/LoanManagement.Api.csproj
```

## 🧪 Usuarios de Prueba (Auto-Seed)
- **Admin:** `admin@test.com` / `123`
- **User:** `usuario@test.com` / `123`

## 🏗️ Docker
```bash
docker build -t loan-api .
```
