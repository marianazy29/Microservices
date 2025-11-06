-- Crear base de datos Ventas
CREATE DATABASE Ventas;
GO

-- Usar base de datos Ventas
USE Ventas;
GO

-- Crear tabla Venta
CREATE TABLE Venta (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UsuarioId INT,
    ClienteId INT,
    Fecha DATE,
    Descuento DECIMAL(18,2),
    MontoTotal DECIMAL(18,2),
    Comentarios VARCHAR(100),
    Estado VARCHAR(8)
);
GO

-- Crear tabla DetalleDeVenta
CREATE TABLE DetalleDeVenta (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    VentaId UNIQUEIDENTIFIER,
    ProductoId INT,
    Cantidad INT,
    Precio DECIMAL(18,2),
    Descuento DECIMAL(18,2),
    TotalLinea DECIMAL(18,2),
    Estado VARCHAR(8),
	FOREIGN KEY (VentaId) REFERENCES Venta(Id)
);
GO