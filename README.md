# Sistema de Gestión de Garantías - API

API REST desarrollada con ASP.NET Core 8 para la gestión de garantías de equipos Lenovo. Permite administrar tickets, componentes dañados, seguimiento de incidencias y exponer información para dashboards analíticos y generación de reportes ejecutivos.

---

## Descripción

El sistema centraliza el ciclo completo de atención de incidencias:

- Registro de tickets
- Seguimiento de garantías
- Gestión de componentes reemplazados
- Control de estados
- Consulta de historial
- Exposición de datos para dashboards
- Integración con reportes automatizados

La API es consumida por una aplicación frontend desarrollada en React.

---

## Arquitectura

```text
Frontend (React)
        |
        v
API REST (.NET 8)
        |
        v
SQL Server
```

---

## Funcionalidades

### Gestión de Tickets

- Crear tickets
- Consultar tickets
- Actualizar tickets
- Eliminar tickets
- Buscar por serie
- Buscar por inventario

### Gestión de Garantías

- Registro de casos Lenovo
- Control de procedencia de garantía
- Seguimiento de estado

### Gestión de Componentes

- Agregar componentes reemplazados
- Consultar historial de componentes
- Relación Ticket-Componente

### Dashboard

La API suministra información para:

- Total de tickets
- Garantías procedentes y no procedentes
- Casos abiertos y cerrados
- Casos por mes
- Casos por trimestre
- Ranking de componentes
- Tipos de daño

---

## Endpoints Principales

### Tickets

```http
GET    /api/tickets
POST   /api/tickets
GET    /api/tickets/{id}
PUT    /api/tickets/{id}
DELETE /api/tickets/{id}
```

### Búsquedas

```http
GET /api/tickets/buscar
```

### Componentes

```http
POST /api/tickets/{id}/componentes
```

---

## Tecnologías

### Backend

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- REST API

### DevOps

- Docker
- Git
- GitHub

### Testing

- Playwright
- Postman
- Newman

---

## Base de Datos

Modelo principal:

```text
Tickets
│
├── ComponentesDanados
```

Características:

- Relación uno a muchos
- Persistencia en SQL Server
- Entity Framework Core
- Migraciones controladas

---

## Ejecución Local

### Restaurar dependencias

```bash
dotnet restore
```

### Ejecutar API

```bash
dotnet run
```

### Aplicar migraciones

```bash
dotnet ef database update
```

---

## Testing Automatizado

El sistema cuenta con un repositorio independiente de QA Automation:

### UI Testing

- Playwright
- Page Object Model

### API Testing

- Postman
- Newman

Casos cubiertos:

- Crear Ticket
- Buscar Ticket
- Actualizar Ticket
- Agregar Componente
- Dashboard
- Exportación de Reportes
- CRUD API completo

Repositorio QA:

🔗 Garantias-QA

---

## Capturas

### 📊 Dashboard
![Dashboard](./screenshots/dashboard.png)



### 📈 Gráficos
![Charts](./screenshots/charts.png)



### 📄 Reporte generado
![Reporte](./screenshots/report.png)

---

## Valor del Proyecto

Este proyecto permitió aplicar conocimientos de:

- Desarrollo Backend
- Arquitectura REST
- Modelado de Base de Datos
- Entity Framework Core
- SQL Server
- Docker
- Testing Automatizado
- API Testing
- Business Intelligence
- Reportería Ejecutiva

---

## Autor

**Henry Gabriel Gómez Gerónimo**

Ingeniero Electrónico | Soporte TI N2 | Backend Developer | QA Automation | DevOps Enthusiast