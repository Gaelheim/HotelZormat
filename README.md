# HotelZormat

Sistema de gestión hotelera (MVP) desarrollado como proyecto del curso
**ISW-123 Programación Media** · 6to semestre · Ingeniería de Software · UCE.

## Información del Estudiante
- **Nombre Completo:** Angel Francisco Natera Daniel
- **Matrícula:** 2023-3607

  
## Stack Tecnológico
- **Lenguaje:** C# 7.3
- **Framework:** .NET Framework 4.7.2
- **Interfaz Gráfica:** Windows Forms (WinForms)
- **Base de Datos:** SQL Server Express / LocalDB
- **Patrón de Arquitectura:** Layered Architecture + Repository Pattern

---

## Arquitectura del Proyecto

```text
HotelZormat (UI)          <- Presentación (Formularios Windows Forms)
   │
   ├─► HotelZormat.Negocio <- Lógica de negocio y reglas del sistema
   │      │
   │      └─► HotelZormatDatos <- Acceso a datos con SQL Server & Repositorios ADO.NET
   │
   └─► HotelZormat.Modelo  <- Entidades de dominio y DTOs compartidos
```

---
## Credenciales de Prueba por Defecto

| Usuario | Contraseña | Rol | Permisos Especiales |
|---|---|---|---|
| `admin` | `admin123` | Administrador | Eliminar habitaciones, Consultar bitácora |
| `recepcion` | `recep123` | Recepcionista | Gestión operativa estándar |

## Módulos del Sistema
- [x] **Estructura por capas:** UI, Negocio, Datos, Modelo.
- [x] **Acceso y Seguridad:** Login con hash SHA-256 y roles diferenciados.
- [x] **Dashboard de Habitaciones:** Tablero interactivo con mapa de colores por estado (`switch`).
- [x] **CRUD Habitaciones:** Listar con `foreach`, Crear, Actualizar y Eliminar (con confirmación).
- [x] **CRUD Huéspedes:** Validación de cédula de 11 dígitos, Pasaporte, filtro de búsqueda e historial.
- [x] **CRUD Reservas:** Creación con fechas de Check-In / Check-Out, temporadas y cálculo de estancia.
- [x] **Facturación & Flujo Hotelero:** Check-In, Check-Out, generación de factura con NCF secuencial (Consumo Final), ITBIS 18% y propina legal 10%.
- [x] **Reportes & Bitácora:** Ocupación del día, ingresos por rango de fechas y auditoría de acciones.

---

## Licencia & Uso
Uso académico y demostrativo.
