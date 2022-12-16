USE C3_BD_PEDIDOS
GO

SELECT @@LANGUAGE
SET LANGUAGE SPANISH --Para poder ingresar fecha en formato DD-MM-YYY sin errores
GO

INSERT INTO Cargo VALUES('Vendedor', 1400)
INSERT INTO Cargo VALUES('Despachador', 1000)
GO

INSERT INTO Cliente VALUES('Pedro','Salas Rojas','Natural','49672364','Av. Peru 485','SMP','954781364',1)
INSERT INTO Cliente VALUES('Margarita','Perez Rivas','Natural','59910012','Av. Los Heroes 164','SJM','980994671',1)
INSERT INTO Cliente VALUES('Paula','Ciro Bustamante','Natural','54189623','Av. Los Girasoles 098','Surco','964949121',1)
INSERT INTO Cliente VALUES('Jaime','Jimenez Valencia','Natural','10012015','Jr. Libertarios 465','San Borja','956005489',1)
GO

INSERT INTO Empleado VALUES('Marjhorie','Aguirre Abanto','59012347','Av. Centenario 408','SJM',27,'962121348',1,'13/07/2020',1)
INSERT INTO Empleado VALUES('Daniel','Condori Martinez','79006451','Av. Los Heroes 840','SJM',22,'905236741',2,'25/02/2022',1)
GO

INSERT INTO Marca VALUES('Generico')
INSERT INTO Marca VALUES('El Chino')
INSERT INTO Marca VALUES('Mattel')
INSERT INTO Marca VALUES('Pokemon')
GO

INSERT INTO Modelo VALUES('Oso')
INSERT INTO Modelo VALUES('Leon')
INSERT INTO Modelo VALUES('Stitch')
INSERT INTO Modelo VALUES('Pikachu')
GO

INSERT INTO Proveedor VALUES('Alico','Jimenez Salas','Jr. Mesa Redonda 964','Lima','984561347',1)
INSERT INTO Proveedor VALUES('Rosario','Silva Salgado','Jr. Mesa Redonda 905','Lima','941693123',1)
GO

INSERT INTO Producto VALUES('Leon Mediano','Leon de peluche mediano de color naranja y con camiseta roja',
							12.50,2,2,1,36,1)
INSERT INTO Producto VALUES('Leon Pequeño','Leon de peluche chico de color naranja y con camiseta celeste',
							9.00,2,2,1,20,1)
INSERT INTO Producto VALUES('Stitch Futbolista','Peluche de Stitch con camiseta de futbol de Brasil, Argentina o Peru',
							15.00,3,3,2,47,1)
INSERT INTO Producto VALUES('Pikachu Surfista','Peluche de Pikachu macho o hembra con tabla de surf',
							22.00,4,4,2,20,1)
GO

INSERT INTO Pedido VALUES(1,1,'15/05/2022','15/05/2022',45.00,1)
INSERT INTO Pedido VALUES(2,1,'08/06/2022','13/06/2022',21.50,1)
INSERT INTO Pedido VALUES(3,1,'19/08/2022','23/08/2022',18.00,1)
INSERT INTO Pedido VALUES(4,1,'12/06/2022','15/06/2022',44.00,1)
GO

INSERT INTO Detalle_Pedido VALUES(1,3,3,15.00,45.00)
INSERT INTO Detalle_Pedido VALUES(2,1,1,12.50,12.50)
INSERT INTO Detalle_Pedido VALUES(2,2,1,9.00,9.00)
INSERT INTO Detalle_Pedido VALUES(3,2,2,9.00,18.00)
INSERT INTO Detalle_Pedido VALUES(4,4,2,22.00,44.00)
GO

INSERT INTO Comprobante VALUES(1,'Boleta','15/05/2022',45.00,0,0,45.00)
INSERT INTO Comprobante VALUES(2,'Boleta','13/06/2022',21.50,0,0,21.50)
INSERT INTO Comprobante VALUES(3,'Boleta','23/08/2022',18.00,0,0,18.00)
INSERT INTO Comprobante VALUES(4,'Boleta','15/06/2022',44.00,0,0,44.00)
GO
go
INSERT INTO Usuario (ID_Empleado, Usuario, Password) VALUES (1, 'mAguirre', '1234')
INSERT INTO Usuario (ID_Empleado, Usuario, Password) VALUES (2, 'dCondori', '1234')


--Comprobacion de Datos insertados
SELECT * FROM Cargo
SELECT * FROM Cliente
SELECT * FROM Empleado
SELECT * FROM Marca
SELECT * FROM Modelo
SELECT * FROM Proveedor
SELECT * FROM Producto
SELECT * FROM Pedido
SELECT * FROM Detalle_Pedido
SELECT * FROM Comprobante
GO
