# Bank Loan Management - Backend 🏦

API REST robusta desarrollada para la gestión de préstamos bancarios, centrada en la seguridad, escalabilidad y una alta mantenibilidad mediante principios de diseño modernos.

## 🏗️ Arquitectura
El sistema sigue una **Arquitectura Hexagonal (Clean Architecture)** dividida en 4 capas:
- **Domain:** Entidades de negocio (`User`, `Loan`) y reglas núcleo.
- **Application:** Casos de uso, servicios y DTOs.
- **Infrastructure:** Persistencia (SQLite) y repositorios.
- **Api:** Controladores REST, autenticación JWT y configuración de arranque.

## 🚀 Tecnologías
- **.NET 10**: Última tecnología de Microsoft para APIs de alto rendimiento.
- **Entity Framework Core 10**: ORM para gestión de datos.
- **SQLite**: Motor de base de datos embebido para una ejecución local sin dependencias.
- **JWT (JSON Web Tokens)**: Seguridad basada en tokens para autenticacion stateless.
- **Auto-Seeding**: Inicialización automática de la base de datos y usuarios de prueba.

## 🛠️ Cómo levantar la API
1. Asegúrate de tener el SDK de .NET instalado.
2. Navega al directorio del proyecto:
   ```bash
   cd src/Api
   ```
3. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```
   *La API se levantará por defecto en `http://localhost:5000`.*

---

## 🔗 Comunicación Full Stack
Para que el frontend se comunique correctamente, esta API habilita **CORS** para el puerto `3000` y expone endpoints bajo el prefijo `/api`.

## 🧪 Usuarios de Prueba (Pre-cargados)
- **Admin:** `admin@test.com` / `123`
- **Usuario:** `usuario@test.com` / `123`
