# HotelZormat

Sistema de gestión hotelera (MVP) desarrollado como proyecto del curso
**ISW-123 Programación Media** · 6to semestre · Ingeniería de Software · UCE.

## Stack
- C# 7.3
- .NET Framework 4.7.2
- Windows Forms
- SQL Server Express
- ADO.NET (Repository pattern)

## Arquitectura

```
HotelZormat.UI         <- Windows Forms (presentación)
   ↓ usa
HotelZormat.Negocio    <- Reglas de negocio (modelos, servicios)
   ↓ usa
HotelZormat.Datos      <- Acceso a SQL Server (repositorios)
```

## Módulos planificados
- [x] Estructura por capas
- [ ] Login y usuarios
- [ ] Gestión de habitaciones
- [ ] Gestión de huéspedes
- [ ] Reservas
- [ ] Check-in / Check-out
- [ ] Facturación con NCF (ITBIS 18%, propina 10%)
- [ ] Reportes

## Autor
Angel Natera · Matrícula UCE 2023-3607

## Licencia
Uso académico.
