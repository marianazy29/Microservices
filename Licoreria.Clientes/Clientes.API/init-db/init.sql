-- Crear base de datos Clientes
CREATE DATABASE Clientes;
GO

-- Usar base de datos Clientes
USE Clientes;
GO

-- Crear tabla Clientes
CREATE TABLE Cliente (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    NombreCompleto VARCHAR(50),
    PuntosAcumulados DECIMAL(18,2),
    Estado VARCHAR(8)
);
GO

-- Crear tabla Premio
CREATE TABLE Premio (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClienteId UNIQUEIDENTIFIER,
    ProductoId INT,
    Descripcion VARCHAR(100),
    Fecha DATE,
    Estado VARCHAR(8),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);
GO

-- Insertar filas en Clientes
INSERT INTO Cliente (Nombrecompleto, PuntosAcumulados, Estado)
VALUES 
('Juan Perez', 100.00, 'ACTIVO'),
('Maria Rodriguez', 50.00, 'ACTIVO'),
('Carlos Lopez', 200.00, 'ACTIVO');
GO