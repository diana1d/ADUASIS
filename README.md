# ADUASIS

**Sistema para la gestión, monitoreo y visualización de activos tecnológicos**  
**Caso: Aduana Nacional – Gerencia Regional La Paz**

> Proyecto de grado — Ingeniería de Sistemas

---

## Descripción

ADUASIS es una plataforma web para la gestión, monitoreo, soporte y visualización de activos tecnológicos de la Aduana Nacional – Gerencia Regional La Paz.

Integra un inventario digital de activos tecnológicos con visualización 3D interactiva (Three.js + Blender) y conecta con el sistema de tickets existente (osTicket) para facilitar el seguimiento de incidencias.

---

## Stack tecnológico

| Capa | Tecnología |
|---|---|
| Backend | C#, ASP.NET Core Web API, Entity Framework Core |
| Frontend | React, TypeScript, Vite |
| Base de datos | PostgreSQL 17 |
| Visualización 3D | Blender, Three.js, GLB/glTF |
| Control de versiones | Git, GitHub |

---

## Estructura del proyecto

```
ADUASIS/
├── backend/
│   └── Aduasis.Api/          # ASP.NET Core Web API
├── frontend/
│   └── aduasis-web/          # React + TypeScript (Vite)
├── basedatos/
│   ├── migraciones/          # Scripts SQL de referencia
│   └── scripts/              # Seeds y scripts de mantenimiento
├── modelos3d/
│   ├── blender/              # Archivos fuente .blend
│   └── exportaciones/        # GLB/glTF exportados
├── documentacion/
│   ├── arquitectura/
│   ├── basedatos/
│   ├── api/
│   └── proyecto/
└── pruebas/
```

---

## Configuración local

### Requisitos previos

- .NET 10 SDK
- Node.js 22+
- PostgreSQL 17

### Backend

1. Copiar la configuración:
   ```
   # Editar backend/Aduasis.Api/appsettings.Development.json
   # con tus credenciales de PostgreSQL local
   ```

2. Crear la base de datos:
   ```sql
   CREATE DATABASE aduasis_db;
   ```

3. Ejecutar migraciones:
   ```bash
   dotnet ef database update
   ```

4. Iniciar el servidor:
   ```bash
   dotnet run
   ```
   
   API disponible en: `http://localhost:5020`  
   Swagger UI: `http://localhost:5020/swagger`

### Frontend

1. Instalar dependencias:
   ```bash
   npm install
   ```

2. Copiar variables de entorno:
   ```bash
   cp .env.example .env.local
   ```

3. Iniciar el servidor de desarrollo:
   ```bash
   npm run dev
   ```
   
   Aplicación disponible en: `http://localhost:5173`

---

## Estado del proyecto

| Sprint | Descripción | Estado |
|---|---|---|
| Sprint 0 | Fundaciones, estructura base | ✅ Completado |
| Sprint 1 | Autenticación y usuarios | 🔜 Pendiente |
| Sprint 2 | Catálogos y activos | 🔜 Pendiente |
| Sprint 3 | Historial y asignaciones | 🔜 Pendiente |
| Sprint 4 | Visualización 3D básica | 🔜 Pendiente |
| Sprint 5 | Integración con osTicket | 🔜 Pendiente |
| Sprint 6 | Monitoreo y reportes | 🔜 Pendiente |

---

## Documentación

- [Arquitectura del sistema](documentacion/arquitectura/)
- [Modelo de base de datos](documentacion/basedatos/)
- [Documentación de API](documentacion/api/)
- [Documentación del proyecto](documentacion/proyecto/)
