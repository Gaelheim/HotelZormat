CREATE DATABASE HotelZormatDB;
GO
 
USE HotelZormatDB;
GO


-- Almacena los roles disponibles dentro del sistema HotelZormat.
-- Cada rol define los permisos generales que tendrá un usuario al iniciar sesión en la aplicación.
CREATE TABLE Roles (
    Id  INT IDENTITY(1,1) PRIMARY KEY,
    Nombre  VARCHAR(30) NOT NULL,
    PuedeEliminarHabitaciones BIT NOT NULL DEFAULT (0),
    PuedeVerBitacora BIT  NOT NULL DEFAULT (0),
    CONSTRAINT UQ_Roles_Nombre UNIQUE (Nombre)
);
GO

-- Usuarios del sistema
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario   VARCHAR(50)     NOT NULL,
    PasswordHash    VARBINARY(64)   NOT NULL,   -- SHA2_256 = 32 bytes; se deja 64 por si se usa SHA2_512
    RolId  INT NOT NULL,
    NombreCompleto  VARCHAR(100)    NOT NULL,
    Activo  BIT  NOT NULL DEFAULT (1),
    FechaCreacion DATETIME  NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario),
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (RolId) REFERENCES Roles(Id)
);
GO

-- Tipos de habitación. Tarifa base vive aquí, no en cada habitación.
CREATE TABLE TiposHabitacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(30) NOT NULL,   -- Sencilla, Doble, Suite
    TarifaBase DECIMAL(10,2) NOT NULL,
    CapacidadMax INT NOT NULL,
    CONSTRAINT UQ_TiposHabitacion_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_TiposHabitacion_TarifaBase CHECK (TarifaBase > 0),
    CONSTRAINT CK_TiposHabitacion_Capacidad CHECK (CapacidadMax > 0)
);
GO

-- Temporadas y su factor de descuento
CREATE TABLE Temporadas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(20) NOT NULL,   -- Alta, Media, Baja
    FactorDescuento DECIMAL(4,2) NOT NULL, -- 0.00 = Alta, 0.10 = Media, 0.20 = Baja
    CONSTRAINT UQ_Temporadas_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_Temporadas_Factor CHECK (FactorDescuento BETWEEN 0 AND 1)
);
GO

CREATE TABLE Habitaciones (
    Id  INT IDENTITY(1,1) PRIMARY KEY,
    Numero  VARCHAR(10) NOT NULL,
    TipoHabitacionId INT NOT NULL,
    Piso  INT  NOT NULL,
    Capacidad INT  NOT NULL,
    Estado VARCHAR(15) NOT NULL DEFAULT ('Disponible'),
    CONSTRAINT UQ_Habitaciones_Numero UNIQUE (Numero),
    CONSTRAINT FK_Habitaciones_Tipo FOREIGN KEY (TipoHabitacionId) REFERENCES TiposHabitacion(Id),
    CONSTRAINT CK_Habitaciones_Estado CHECK (Estado IN ('Disponible','Ocupada','Reservada','Limpieza')),
    CONSTRAINT CK_Habitaciones_Piso CHECK (Piso >= 0),
    CONSTRAINT CK_Habitaciones_Capacidad CHECK (Capacidad > 0)
);
GO

CREATE TABLE Huespedes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre  VARCHAR(60) NOT NULL,
    Apellido VARCHAR(60) NOT NULL,
    TipoDocumento VARCHAR(10) NOT NULL,   -- Cedula | Pasaporte
    NumeroDocumento VARCHAR(20) NOT NULL,
    Nacionalidad  VARCHAR(40) NOT NULL,
    Telefono VARCHAR(20) NULL,
    Email VARCHAR(100) NULL,
    FechaRegistro   DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT UQ_Huespedes_Documento UNIQUE (TipoDocumento, NumeroDocumento),
    CONSTRAINT CK_Huespedes_TipoDocumento CHECK (TipoDocumento IN ('Cedula','Pasaporte')),
    -- Cédula dominicana = exactamente 11 dígitos numéricos; pasaporte no se restringe al mismo patrón
    CONSTRAINT CK_Huespedes_CedulaFormato CHECK (
        (TipoDocumento = 'Cedula' AND NumeroDocumento LIKE REPLICATE('[0-9]', 11) AND LEN(NumeroDocumento) = 11)
        OR TipoDocumento = 'Pasaporte'
    )
);
GO

CREATE TABLE Reservas (
    Id  INT IDENTITY(1,1) PRIMARY KEY,
    HuespedId INT NOT NULL,
    HabitacionId INT NOT NULL,
    TemporadaId INT NOT NULL,
    FechaCheckIn DATE NOT NULL,
    FechaCheckOut DATE NOT NULL,
    Estado VARCHAR(15) NOT NULL DEFAULT ('Pendiente'),
    Noches AS (DATEDIFF(DAY, FechaCheckIn, FechaCheckOut)) PERSISTED,
    MontoEstimado DECIMAL(10,2) NOT NULL,
    UsuarioCreacionId INT NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT FK_Reservas_Huesped FOREIGN KEY (HuespedId) REFERENCES Huespedes(Id),
    CONSTRAINT FK_Reservas_Habitacion FOREIGN KEY (HabitacionId) REFERENCES Habitaciones(Id),
    CONSTRAINT FK_Reservas_Temporada FOREIGN KEY (TemporadaId) REFERENCES Temporadas(Id),
    CONSTRAINT FK_Reservas_UsuarioCreacion FOREIGN KEY (UsuarioCreacionId) REFERENCES Usuarios(Id),
    CONSTRAINT CK_Reservas_Estado CHECK (Estado IN ('Pendiente','Confirmada','Cancelada')),
    CONSTRAINT CK_Reservas_Fechas CHECK (FechaCheckOut > FechaCheckIn),
    CONSTRAINT CK_Reservas_Monto CHECK (MontoEstimado >= 0)
);
GO

CREATE TABLE Estadias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservaId INT NOT NULL,
    HabitacionId INT NOT NULL,
    HuespedId INT NOT NULL,
    FechaCheckInReal DATETIME NOT NULL DEFAULT (GETDATE()),
    FechaCheckOutReal DATETIME NULL,
    Estado VARCHAR(10) NOT NULL DEFAULT ('Activa'),
    UsuarioCheckInId  INT NOT NULL,
    UsuarioCheckOutId  INT NULL,
    CONSTRAINT UQ_Estadias_Reserva UNIQUE (ReservaId),
    CONSTRAINT FK_Estadias_Reserva FOREIGN KEY (ReservaId) REFERENCES Reservas(Id),
    CONSTRAINT FK_Estadias_Habitacion FOREIGN KEY (HabitacionId) REFERENCES Habitaciones(Id),
    CONSTRAINT FK_Estadias_Huesped FOREIGN KEY (HuespedId) REFERENCES Huespedes(Id),
    CONSTRAINT FK_Estadias_UsuarioCheckIn FOREIGN KEY (UsuarioCheckInId) REFERENCES Usuarios(Id),
    CONSTRAINT FK_Estadias_UsuarioCheckOut FOREIGN KEY (UsuarioCheckOutId) REFERENCES Usuarios(Id),
    CONSTRAINT CK_Estadias_Estado CHECK (Estado IN ('Activa','Cerrada'))
);
GO

-- Control de numeración secuencial de NCF (tipo Consumo Final = B02)
CREATE TABLE SecuenciaNCF (
    TipoNCF VARCHAR(3) NOT NULL PRIMARY KEY,
    UltimoNumero INT NOT NULL DEFAULT (0)
);
GO

CREATE TABLE Facturas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EstadiaId INT NOT NULL,
    NCF VARCHAR(13) NOT NULL,   -- Ej: B0200000001
    Subtotal DECIMAL(10,2)   NOT NULL,
    ITBIS DECIMAL(10,2)   NOT NULL,
    Propina DECIMAL(10,2)   NOT NULL,
    Total DECIMAL(10,2)   NOT NULL,
    FechaEmision DATETIME NOT NULL DEFAULT (GETDATE()),
    UsuarioId INT NOT NULL,
    CONSTRAINT UQ_Facturas_Estadia UNIQUE (EstadiaId),
    CONSTRAINT UQ_Facturas_NCF UNIQUE (NCF),
    CONSTRAINT FK_Facturas_Estadia FOREIGN KEY (EstadiaId) REFERENCES Estadias(Id),
    CONSTRAINT FK_Facturas_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
    CONSTRAINT CK_Facturas_Montos CHECK (Subtotal >= 0 AND ITBIS >= 0 AND Propina >= 0 AND Total >= 0)
);
GO

CREATE TABLE Bitacora (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Accion VARCHAR(40) NOT NULL,   -- Login, CheckIn, CheckOut, Facturacion, ...
    Detalle VARCHAR(300) NULL,
    FechaHora  DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

CREATE INDEX IX_Habitaciones_Estado ON Habitaciones(Estado);
CREATE INDEX IX_Habitaciones_Piso ON Habitaciones(Piso);
CREATE INDEX IX_Huespedes_NumeroDocumento ON Huespedes(NumeroDocumento);
CREATE INDEX IX_Huespedes_Apellido ON Huespedes(Apellido);
CREATE INDEX IX_Reservas_Fechas ON Reservas(FechaCheckIn, FechaCheckOut);
CREATE INDEX IX_Reservas_Estado ON Reservas(Estado);
CREATE INDEX IX_Estadias_Estado ON Estadias(Estado);
CREATE INDEX IX_Facturas_FechaEmision ON Facturas(FechaEmision);
CREATE INDEX IX_Bitacora_FechaHora ON Bitacora(FechaHora);
GO

-- Reporte 1: Ocupación del día (habitaciones ocupadas con su huésped actual)

CREATE VIEW vw_OcupacionDelDia AS
SELECT
    h.Numero AS Habitacion,
    th.Nombre AS TipoHabitacion,
    h.Piso,
    hu.Nombre + ' ' + hu.Apellido AS Huesped,
    hu.TipoDocumento,
    hu.NumeroDocumento,
    e.FechaCheckInReal,
    r.FechaCheckOut     AS CheckOutProgramado
FROM Estadias e
JOIN Habitaciones h        ON h.Id = e.HabitacionId
JOIN TiposHabitacion th     ON th.Id = h.TipoHabitacionId
JOIN Huespedes hu           ON hu.Id = e.HuespedId
JOIN Reservas r             ON r.Id = e.ReservaId
WHERE e.Estado = 'Activa';
GO

-- Reporte auxiliar: Reservas próximas (próximos 7 días desde hoy)
CREATE VIEW vw_ReservasProximas7Dias AS
SELECT
    r.Id                AS ReservaId,
    hu.Nombre + ' ' + hu.Apellido AS Huesped,
    h.Numero            AS Habitacion,
    r.FechaCheckIn,
    r.FechaCheckOut,
    r.Noches,
    r.Estado,
    r.MontoEstimado
FROM Reservas r
JOIN Huespedes hu ON hu.Id = r.HuespedId
JOIN Habitaciones h ON h.Id = r.HabitacionId
WHERE r.FechaCheckIn BETWEEN CAST(GETDATE() AS DATE) AND DATEADD(DAY, 7, CAST(GETDATE() AS DATE))
  AND r.Estado <> 'Cancelada';
GO

-- Vista base para el reporte 2, ingresos.
CREATE VIEW vw_FacturasDetalle AS
SELECT
    f.Id                AS FacturaId,
    f.NCF,
    f.FechaEmision,
    f.Subtotal,
    f.ITBIS,
    f.Propina,
    f.Total,
    hu.Nombre + ' ' + hu.Apellido AS Huesped,
    h.Numero            AS Habitacion,
    u.NombreUsuario     AS FacturadoPor
FROM Facturas f
JOIN Estadias e     ON e.Id = f.EstadiaId
JOIN Huespedes hu   ON hu.Id = e.HuespedId
JOIN Habitaciones h ON h.Id = e.HabitacionId
JOIN Usuarios u     ON u.Id = f.UsuarioId;
GO

--sp_Login: valida usuario/hash y devuelve rol + permisos
CREATE PROCEDURE sp_Login
    @NombreUsuario VARCHAR(50),
    @PasswordHash  VARBINARY(64)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT u.Id, u.NombreUsuario, u.NombreCompleto, r.Nombre AS Rol,
           r.PuedeEliminarHabitaciones, r.PuedeVerBitacora
    FROM Usuarios u
    JOIN Roles r ON r.Id = u.RolId
    WHERE u.NombreUsuario = @NombreUsuario
      AND u.PasswordHash = @PasswordHash
      AND u.Activo = 1;
END
GO

--Siguiente número de NCF (bloqueo de fila para evitar duplicados en concurrencia)
CREATE PROCEDURE sp_SiguienteNCF
    @TipoNCF VARCHAR(3),
    @NCFGenerado VARCHAR(13) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Numero INT;
 
    UPDATE SecuenciaNCF WITH (UPDLOCK, ROWLOCK)
    SET @Numero = UltimoNumero = UltimoNumero + 1
    WHERE TipoNCF = @TipoNCF;
 
    SET @NCFGenerado = @TipoNCF + RIGHT('00000000' + CAST(@Numero AS VARCHAR(8)), 8);
END
GO

--Crear reserva: calcula noches y monto (tarifa del tipo * noches * factor de temporada)
CREATE PROCEDURE sp_CrearReserva
    @HuespedId INT,
    @HabitacionId INT,
    @TemporadaId INT,
    @FechaCheckIn DATE,
    @FechaCheckOut DATE,
    @UsuarioCreacionId INT
AS
BEGIN
    SET NOCOUNT ON;
    IF @FechaCheckOut <= @FechaCheckIn
    BEGIN
        RAISERROR('La fecha de check-out debe ser posterior al check-in.', 16, 1);
        RETURN;
    END
 
    DECLARE @TarifaBase DECIMAL(10,2), @Factor DECIMAL(4,2), @Noches INT, @Monto DECIMAL(10,2);
 
    SELECT @TarifaBase = th.TarifaBase
    FROM Habitaciones h
    JOIN TiposHabitacion th ON th.Id = h.TipoHabitacionId
    WHERE h.Id = @HabitacionId;
 
    SELECT @Factor = FactorDescuento FROM Temporadas WHERE Id = @TemporadaId;
 
    SET @Noches = DATEDIFF(DAY, @FechaCheckIn, @FechaCheckOut);
    SET @Monto = @TarifaBase * @Noches * (1 - @Factor);
 
    INSERT INTO Reservas (HuespedId, HabitacionId, TemporadaId, FechaCheckIn, FechaCheckOut,
                           Estado, MontoEstimado, UsuarioCreacionId)
    VALUES (@HuespedId, @HabitacionId, @TemporadaId, @FechaCheckIn, @FechaCheckOut,
            'Pendiente', @Monto, @UsuarioCreacionId);
 
    SELECT SCOPE_IDENTITY() AS ReservaId, @Noches AS Noches, @Monto AS MontoEstimado;
END
GO

--Check-in: confirma reserva -> crea estadía -> habitación pasa a Ocupada
CREATE PROCEDURE sp_CheckIn
    @ReservaId INT,
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
 
        DECLARE @HabitacionId INT, @HuespedId INT, @EstadoHabitacion VARCHAR(15);
 
        SELECT @HabitacionId = HabitacionId, @HuespedId = HuespedId
        FROM Reservas WHERE Id = @ReservaId AND Estado = 'Confirmada';
 
        IF @HabitacionId IS NULL
        BEGIN
            RAISERROR('La reserva no existe o no está confirmada.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
 
        SELECT @EstadoHabitacion = Estado FROM Habitaciones WHERE Id = @HabitacionId;
        IF @EstadoHabitacion <> 'Disponible' AND @EstadoHabitacion <> 'Reservada'
        BEGIN
            RAISERROR('La habitación no está disponible para check-in.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
 
        INSERT INTO Estadias (ReservaId, HabitacionId, HuespedId, UsuarioCheckInId)
        VALUES (@ReservaId, @HabitacionId, @HuespedId, @UsuarioId);
 
        UPDATE Habitaciones SET Estado = 'Ocupada' WHERE Id = @HabitacionId;
 
        INSERT INTO Bitacora (UsuarioId, Accion, Detalle)
        VALUES (@UsuarioId, 'CheckIn', 'Reserva ' + CAST(@ReservaId AS VARCHAR(10)));
 
        COMMIT TRANSACTION;
        SELECT SCOPE_IDENTITY() AS EstadiaId;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

--Check-out: cierra estadía, genera factura con NCF, habitación pasa a Limpieza
CREATE PROCEDURE sp_CheckOut
    @EstadiaId INT,
    @UsuarioId INT,
    @PorcentajeITBIS DECIMAL(4,2) = 0.18,
    @PorcentajePropina DECIMAL(4,2) = 0.10
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
 
        DECLARE @HabitacionId INT, @TarifaBase DECIMAL(10,2), @Factor DECIMAL(4,2),
                @Noches INT, @Subtotal DECIMAL(10,2), @ITBIS DECIMAL(10,2),
                @Propina DECIMAL(10,2), @Total DECIMAL(10,2), @NCF VARCHAR(13);
 
        SELECT @HabitacionId = e.HabitacionId, @Noches = r.Noches,
               @TarifaBase = th.TarifaBase, @Factor = t.FactorDescuento
        FROM Estadias e
        JOIN Reservas r ON r.Id = e.ReservaId
        JOIN Habitaciones h ON h.Id = e.HabitacionId
        JOIN TiposHabitacion th ON th.Id = h.TipoHabitacionId
        JOIN Temporadas t ON t.Id = r.TemporadaId
        WHERE e.Id = @EstadiaId AND e.Estado = 'Activa';
 
        IF @HabitacionId IS NULL
        BEGIN
            RAISERROR('La estadía no existe o ya fue cerrada.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
 
        SET @Subtotal = @TarifaBase * @Noches * (1 - @Factor);
        SET @ITBIS = @Subtotal * @PorcentajeITBIS;
        SET @Propina = @Subtotal * @PorcentajePropina;
        SET @Total = @Subtotal + @ITBIS + @Propina;
 
        EXEC sp_SiguienteNCF @TipoNCF = 'B02', @NCFGenerado = @NCF OUTPUT;
 
        INSERT INTO Facturas (EstadiaId, NCF, Subtotal, ITBIS, Propina, Total, UsuarioId)
        VALUES (@EstadiaId, @NCF, @Subtotal, @ITBIS, @Propina, @Total, @UsuarioId);
 
        UPDATE Estadias
        SET Estado = 'Cerrada', FechaCheckOutReal = GETDATE(), UsuarioCheckOutId = @UsuarioId
        WHERE Id = @EstadiaId;
 
        UPDATE Habitaciones SET Estado = 'Limpieza' WHERE Id = @HabitacionId;
 
        INSERT INTO Bitacora (UsuarioId, Accion, Detalle)
        VALUES (@UsuarioId, 'CheckOut', 'Estadia ' + CAST(@EstadiaId AS VARCHAR(10)) + ' - NCF ' + @NCF);
        INSERT INTO Bitacora (UsuarioId, Accion, Detalle)
        VALUES (@UsuarioId, 'Facturacion', 'NCF ' + @NCF + ' Total ' + CAST(@Total AS VARCHAR(20)));
 
        COMMIT TRANSACTION;
        SELECT @NCF AS NCF, @Subtotal AS Subtotal, @ITBIS AS ITBIS, @Propina AS Propina, @Total AS Total;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

--Reporte: Ingresos por rango de fecha
CREATE PROCEDURE sp_ReporteIngresosPorRango
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        COUNT(*)            AS CantidadFacturas,
        SUM(Subtotal)       AS TotalSubtotal,
        SUM(ITBIS)          AS TotalITBIS,
        SUM(Propina)        AS TotalPropina,
        SUM(Total)          AS TotalIngresos
    FROM Facturas
    WHERE CAST(FechaEmision AS DATE) BETWEEN @FechaInicio AND @FechaFin;
END
GO

--Registrar bitácora (para acciones como Login, invocada desde la capa Negocio)
CREATE PROCEDURE sp_RegistrarBitacora
    @UsuarioId INT,
    @Accion VARCHAR(40),
    @Detalle VARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Bitacora (UsuarioId, Accion, Detalle) VALUES (@UsuarioId, @Accion, @Detalle);
END
GO

INSERT INTO Roles (Nombre, PuedeEliminarHabitaciones, PuedeVerBitacora) VALUES
('Administrador', 1, 1), ('Recepcionista', 0, 0);
GO

INSERT INTO Usuarios (NombreUsuario, PasswordHash, RolId, NombreCompleto) VALUES
('admin',  HASHBYTES('SHA2_256', 'admin123'),  1, 'Angel Natera'),
('recepcion1', HASHBYTES('SHA2_256', 'recep123'), 2, 'Recepcionista Uno');
GO

INSERT INTO TiposHabitacion (Nombre, TarifaBase, CapacidadMax) VALUES
('Sencilla', 2500.00, 2), ('Doble',    3800.00, 4), ('Suite',    6500.00, 4);
GO

INSERT INTO Temporadas (Nombre, FactorDescuento) VALUES
('Alta',  0.00), ('Media', 0.10), ('Baja',  0.20);
GO

INSERT INTO SecuenciaNCF (TipoNCF, UltimoNumero) VALUES ('B02', 0);
GO

INSERT INTO Habitaciones (Numero, TipoHabitacionId, Piso, Capacidad, Estado) VALUES
('101', 1, 1, 2, 'Disponible'), ('102', 1, 1, 2, 'Limpieza'), ('201', 2, 2, 4, 'Disponible'),
('202', 2, 2, 4, 'Ocupada'), ('301', 3, 3, 4, 'Disponible'), ('302', 3, 3, 4, 'Reservada');
GO

INSERT INTO Huespedes (Nombre, Apellido, TipoDocumento, NumeroDocumento, Nacionalidad, Telefono, Email) VALUES
('Juan',   'Perez',    'Cedula',    '40212345678', 'Dominicana', '8095551234', 'juan.perez@mail.com'),
('Maria',  'Gomez',    'Cedula',    '00112345678', 'Dominicana', '8095555678', 'maria.gomez@mail.com'),
('John',   'Smith',    'Pasaporte', 'US1234567',   'Estadounidense', '13055551234', 'john.smith@mail.com');
GO

-- Reserva de ejemplo ya confirmada + estadía activa en la habitación 202 (Ocupada)
INSERT INTO Reservas (HuespedId, HabitacionId, TemporadaId, FechaCheckIn, FechaCheckOut, Estado, MontoEstimado, UsuarioCreacionId)
VALUES (2, 4, 1, CAST(GETDATE() AS DATE), DATEADD(DAY, 3, CAST(GETDATE() AS DATE)), 'Confirmada', 3800.00 * 3, 2);
GO

INSERT INTO Estadias (ReservaId, HabitacionId, HuespedId, UsuarioCheckInId)
VALUES (1, 4, 2, 2);
GO

-- Reserva próxima dentro de 7 días, para probar el reporte correspondiente
INSERT INTO Reservas (HuespedId, HabitacionId, TemporadaId, FechaCheckIn, FechaCheckOut, Estado, MontoEstimado, UsuarioCreacionId)
VALUES (1, 3, 2, DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), DATEADD(DAY, 5, CAST(GETDATE() AS DATE)), 'Confirmada', 3800.00 * 3 * 0.9, 2);
GO

-- Factura de ejemplo histórica: se genera vía el flujo real (sp_CheckIn + sp_CheckOut)
-- sobre una reserva y habitación dedicadas, para dejar una factura ya cerrada con NCF.
INSERT INTO Reservas (HuespedId, HabitacionId, TemporadaId, FechaCheckIn, FechaCheckOut, Estado, MontoEstimado, UsuarioCreacionId)
VALUES (3, 1, 3, DATEADD(DAY,-3,CAST(GETDATE() AS DATE)), DATEADD(DAY,-1,CAST(GETDATE() AS DATE)), 'Confirmada', 2500.00 * 2 * 0.8, 1);
GO

DECLARE @ReservaHistorica INT = SCOPE_IDENTITY();
UPDATE Habitaciones SET Estado = 'Reservada' WHERE Id = 1; -- requisito de sp_CheckIn
EXEC sp_CheckIn @ReservaId = @ReservaHistorica, @UsuarioId = 1;
GO

DECLARE @EstadiaHistorica INT = (SELECT TOP 1 Id FROM Estadias WHERE Estado = 'Activa' AND HabitacionId = 1);
EXEC sp_CheckOut @EstadiaId = @EstadiaHistorica, @UsuarioId = 1;
GO

SELECT h.Numero, th.Nombre AS Tipo, h.Piso, h.Capacidad, h.Estado
FROM Habitaciones h JOIN TiposHabitacion th ON th.Id = h.TipoHabitacionId
ORDER BY h.Piso, h.Numero;

SELECT * FROM vw_OcupacionDelDia;

EXEC sp_ReporteIngresosPorRango @FechaInicio = '2000-01-01', @FechaFin = '2100-01-01';

SELECT * FROM vw_ReservasProximas7Dias;

SELECT * FROM Huespedes WHERE NumeroDocumento = '40212345678';

SELECT e.Id AS EstadiaId, h.Numero AS Habitacion, e.FechaCheckInReal, e.FechaCheckOutReal, e.Estado
FROM Estadias e JOIN Habitaciones h ON h.Id = e.HabitacionId
WHERE e.HuespedId = 2;

SELECT * FROM vw_FacturasDetalle ORDER BY FechaEmision DESC;

SELECT b.FechaHora, u.NombreUsuario, r.Nombre AS Rol, b.Accion, b.Detalle
FROM Bitacora b
JOIN Usuarios u ON u.Id = b.UsuarioId
JOIN Roles r ON r.Id = u.RolId
ORDER BY b.FechaHora DESC;

