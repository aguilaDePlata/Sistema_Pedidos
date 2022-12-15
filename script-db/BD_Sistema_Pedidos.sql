USE master
CREATE DATABASE C3_BD_PEDIDOS
GO

USE C3_BD_PEDIDOS
GO

CREATE TABLE [Cargo] (
  [ID_Cargo] int IDENTITY(1,1) NOT NULL,
  [Cargo] nvarchar(50) NULL,
  [Sueldo] decimal(10,2) NULL,
  CONSTRAINT [_copy_2] PRIMARY KEY CLUSTERED ([ID_Cargo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Cliente] (
  [ID_Cliente] int IDENTITY(1,1) NOT NULL,
  [Nombre] nvarchar(50) NULL,
  [Apellidos] nvarchar(50) NULL,
  [Tipo_Cliente] nvarchar(20) NOT NULL,
  [Nro_Documento] nvarchar(11) NOT NULL,
  [Direccion] nvarchar(255) NULL,
  [Distrito] nvarchar(50) NULL,
  [Telefono] nvarchar(20) NULL,
  [Activo] bit NULL,
  CONSTRAINT [_copy_5] PRIMARY KEY CLUSTERED ([ID_Cliente])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Comprobante] (
  [ID_Comprobante] int IDENTITY(1,1) NOT NULL,
  [ID_Pedido] int NULL,
  [Tipo_Comprobante] nvarchar(20) NULL,
  [Fecha_Emision] date NULL,
  [Subtotal_Comp] decimal(10,2) NULL,
  [Descuento] decimal(10,2) NULL,
  [Igv] decimal(10,2) NULL,
  [Valor_Total] decimal(10,2) NULL,
  PRIMARY KEY CLUSTERED ([ID_Comprobante])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Detalle_Pedido] (
  [ID_Detalle] int IDENTITY(1,1) NOT NULL,
  [ID_Pedido] int NULL,
  [ID_Producto] int NULL,
  [Cantidad] int NULL,
  [Precio_Venta] decimal(10,2) NULL,
  [Subtotal_Prod] decimal(10,2) NULL,
  PRIMARY KEY CLUSTERED ([ID_Detalle])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Empleado] (
  [ID_Empleado] int IDENTITY(1,1) NOT NULL,
  [Nombres] nvarchar(50) NULL,
  [Apellidos] nvarchar(50) NULL,
  [DNI] int NULL,
  [Direccion] nvarchar(255) NULL,
  [Distrito] nvarchar(50) NULL,
  [Edad] int NULL,
  [Telefono] nvarchar(20) NULL,
  [ID_Cargo] int NULL,
  [Fecha_Contrato] date NULL,
  [Activo] bit NULL,
  CONSTRAINT [_copy_1] PRIMARY KEY CLUSTERED ([ID_Empleado])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Marca] (
  [ID_Marca] int IDENTITY(1,1) NOT NULL,
  [Marca] nvarchar(50) NULL,
  PRIMARY KEY CLUSTERED ([ID_Marca])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Modelo] (
  [ID_Modelo] int IDENTITY(1,1) NOT NULL,
  [Modelo] nvarchar(50) NULL,
  PRIMARY KEY CLUSTERED ([ID_Modelo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Pedido] (
  [ID_Pedido] int IDENTITY(1,1) NOT NULL,
  [ID_Cliente] int NULL,
  [ID_Empleado] int NULL,
  [Fecha_Pedido] date NULL,
  [Fecha_MaxEntrega] date NULL,
  [Valor_Total] decimal(10,2) NULL,
  [Activo] bit NULL,
  CONSTRAINT [_copy_3] PRIMARY KEY CLUSTERED ([ID_Pedido])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Producto] (
  [ID_Producto] int IDENTITY(1,1) NOT NULL,
  [Nombre_Producto] nvarchar(50) NULL,
  [Descripcion] nvarchar(255) NULL,
  [Precio_Venta] decimal(10,2) NULL,
  [ID_Marca] int NULL,
  [ID_Modelo] int NULL,
  [ID_Proveedor] int NULL,
  [Stock] int NULL,
  [Activo] bit NULL,
  CONSTRAINT [_copy_4] PRIMARY KEY CLUSTERED ([ID_Producto])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Proveedor] (
  [ID_Proveedor] int IDENTITY(1,1) NOT NULL,
  [Nombres_Prov] nvarchar(50) NULL,
  [Apellidos_Prov] nvarchar(50) NULL,
  [Direccion] nvarchar(255) NULL,
  [Distrito] nvarchar(50) NULL,
  [Telefono] nvarchar(20) NULL,
  [Activo] bit NULL,
  PRIMARY KEY CLUSTERED ([ID_Proveedor])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

ALTER TABLE [Comprobante] ADD CONSTRAINT [FK_Comprobante_Pedido] FOREIGN KEY ([ID_Pedido]) REFERENCES [Pedido] ([ID_Pedido])
GO
ALTER TABLE [Detalle_Pedido] ADD CONSTRAINT [FK_DetalleP_Pedido] FOREIGN KEY ([ID_Pedido]) REFERENCES [Pedido] ([ID_Pedido])
GO
ALTER TABLE [Detalle_Pedido] ADD CONSTRAINT [FK_DetalleP_Producto] FOREIGN KEY ([ID_Producto]) REFERENCES [Producto] ([ID_Producto])
GO
ALTER TABLE [Empleado] ADD CONSTRAINT [FK_Empleado_Cargo] FOREIGN KEY ([ID_Cargo]) REFERENCES [Cargo] ([ID_Cargo])
GO
ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_Cliente] FOREIGN KEY ([ID_Cliente]) REFERENCES [Cliente] ([ID_Cliente])
GO
ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_Empleado] FOREIGN KEY ([ID_Empleado]) REFERENCES [Empleado] ([ID_Empleado])
GO
ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_Marca] FOREIGN KEY ([ID_Marca]) REFERENCES [Marca] ([ID_Marca])
GO
ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_Modelo] FOREIGN KEY ([ID_Modelo]) REFERENCES [Modelo] ([ID_Modelo])
GO
ALTER TABLE [Producto] ADD CONSTRAINT [FK_Producto_Proveedor] FOREIGN KEY ([ID_Proveedor]) REFERENCES [Proveedor] ([ID_Proveedor])
GO

