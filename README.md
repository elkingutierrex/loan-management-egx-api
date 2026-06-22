# Loan Management System - Backend (API)

API REST para la gestión de préstamos bancarios desarrollada con **.NET 10** bajo **Arquitectura Hexagonal**.

## 🚀 Tecnologías
- **.NET 10** (Preview 5)
- **C# 13**
- **Entity Framework Core**: ORM para persistencia.
- **PostgreSQL**: Base de datos relacional.
- **JWT (Bearer)**: Autenticación segura.
- **xUnit + Moq**: Pruebas unitarias e integración.

## 🏗️ Arquitectura Hexagonal
El proyecto está dividido en 4 capas para garantizar el desacoplamiento:
1. **Domain:** Entidades de negocio y contratos de repositorio.
2. **Application:** Casos de uso y lógica de orquestación.
3. **Infrastructure:** Implementación de persistencia y servicios externos.
4. **Api:** Controladores REST y configuración de seguridad.

## ⚙️ Configuración
Actualiza las cadenas de conexión en `src/Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=LoanDb;Username=postgres;Password=postgres"
  }
}
```

## 🧪 Pruebas
```bash
dotnet test
```

## 🏃 Ejecución Local
```bash
dotnet run --project src/Api/LoanManagement.Api.csproj
```

## 🏗️ Docker
```bash
docker build -t loan-api .
```
