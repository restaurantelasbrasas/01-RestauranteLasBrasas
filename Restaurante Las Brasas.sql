--------------------------------------------------------|
------- BASE DE DATOS DE RESTAURANTE LAS BRASAS --------|
--														|
-------------- REALIZADO POR: SysLogic ® ---------------|
--														|
------ Todos los derechos reservados - Copyright ©------|
--------------------------------------------------------|


Create DataBase BD_RestauranteLasBrasas
Go

Use BD_RestauranteLasBrasas
Go

CREATE SCHEMA Restaurante
GO

Create Table Restaurante.Categoria
(IdCategoria Int Identity Primary Key,
Descripcion Varchar(50) Not Null
)
Go

Create Table Restaurante.Producto
(IdProducto Int Identity Primary Key,
	IdCategoria Int Not Null References Restaurante.Categoria,
	Nombre Varchar(50) Not Null,
	Marca Varchar(80),
	Stock Int,
	PrecioVenta Decimal(6,2) Not Null,
	FechaVencimiento Date
)
Go

Create Table Restaurante.Cliente
	(IdCliente Int Identity Primary Key,
	Identidad Varchar(18) Not Null,
	Apellido Varchar(50) Not Null,
	Nombre Varchar(50) Not Null,
	Sexo Varchar(5) Not null,
	Direccion Varchar(100) Not Null,
	Telefono Varchar(12)
)
go

Create Table Restaurante.Cargo
	(IdCargo Int Identity Primary Key,
	Descripcion Varchar(30) Not Null
)
Go

Create Table Restaurante.Empleado
(	IdEmpleado Int Identity Primary Key,
	IdCargo Int Not Null References Restaurante.Cargo,
	Identidad Varchar(18) Not Null,
	Apellido Varchar(30) Not Null,
	Nombre Varchar(30) Not Null,
	Sexo Char(1) Not Null,
	FechaNac Date Not Null,
	Direccion Varchar(80) Not Null,
	EstadoCivil Char(1) Not Null
)
Go

Create Table Restaurante.Venta
	(IdVenta Int Identity Primary Key,
	IdEmpleado Int Not Null References Restaurante.Empleado,
	IdCliente Int Not Null References Restaurante.Cliente,
	NroDocumento Char(7) Not Null,
	Total Money Not Null,
    FechaVenta DateTime Not Null  DEFAULT GETDATE()
)
go

Create Table Restaurante.DetalleVenta
(	IdDetalleVenta Int Identity Primary Key,
	IdProducto Int Not Null References Restaurante.Producto,
	IdVenta Int Not Null References Restaurante.Venta,
	Cantidad Int Not Null,
	PrecioUnitario Decimal(6,2) Not Null,
	SubTotal Money Not Null
)
Go

Create Table Restaurante.Usuario
(	IdUsuario Int Identity Primary Key,
	IdEmpleado Int Not Null References Restaurante.Empleado,
	Usuario Varchar(20) Not Null,
	Contraseña Varchar(12) Not Null, 
	TipoUsuario Char(15) not null
)
Go

---------------- PROCEDIMIENTOS ALMACENADOS ----------------------
---REGISTRAR EL CLIENTE EN LA BD
CREATE PROC MantenimientoDelCliente
(
	@identidad Varchar(18) Out,
	@nombre Varchar(50) Out,
	@apellido Varchar(50) Out,
	@sexo Char(5) Output,
	@direccion Varchar(100) Out,
	@telefono Varchar(12) Out
)
As Begin TRANSACTION
BEGIN TRY
	If(NOT Exists(Select * From Restaurante.Cliente Where Identidad=@identidad))
		BEGIN
			Insert Restaurante.Cliente Values(@identidad,@nombre,@apellido,@sexo,@Direccion,@Telefono)
		END
	IF(EXISTS(SELECT * FROM Restaurante.Cliente WHERE Identidad = @identidad))
		BEGIN
			UPDATE Restaurante.Cliente Set Apellido = @apellido,Nombre = @nombre,Sexo = @sexo, Direccion = @Direccion, Telefono = @telefono 
			Where Identidad=@identidad
		END
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
---INGRESAR LOS DATOS A LA TABLA CLIENTE
EXEC MantenimientoDelCliente '35555206','Roberto','Rosa','M','Ave Siempre Viva','96833225'
GO
SELECT * FROM Restaurante.Cliente
GO

--REGISTRAR UN NUEVO PRODUCTO
CREATE PROC RegistrarProducto
(
	@idCategoria INT,
	@nombre VARCHAR(50),
	@marca VARCHAR(80),
	@stock INT,
	@precioVenta DECIMAL(6,2),
	@fechaVencimiento DATE OUT
)
AS BEGIN TRANSACTION 
	BEGIN TRY
		INSERT Restaurante.Producto VALUES(@idCategoria, @nombre, @marca, @stock, @precioVenta, @fechaVencimiento)
	COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH

GO
--INGRESA LOS DATOS A LA TABLA PRODUCTO
EXEC RegistrarProducto 1, 'Horchata','Hecho en Casa',15,20,'2019/11/13'
GO
SELECT * FROM Restaurante.Producto
GO

---ACTUALIZAR UN PRODUCTO
CREATE PROC ActualizarProducto
(
	@idProducto INT,
	@idCategoria INT,
	@nombre VARCHAR(50),
	@marca VARCHAR(80),
	@stock INT,
	@precioVenta DECIMAL(6,2),
	@fechaVencimiento DATE OUT
)
AS BEGIN TRANSACTION
BEGIN TRY
		UPDATE Restaurante.Producto SET IdCategoria = @idCategoria,Nombre = @nombre, Marca = @marca, 
				Stock = @stock,PrecioVenta = @precioVenta, FechaVencimiento = @fechaVencimiento
		WHERE IdProducto = @idProducto	
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
---REGISTAR UN NUEVO CARGO
CREATE PROC RegistarCargo
(
	@descripcion VARCHAR(50) out
)
AS BEGIN TRANSACTION
BEGIN TRY
		INSERT Restaurante.Cargo VALUES(@descripcion)		
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
---ACTUALIZAR CARGO
CREATE PROC ActualizarCargo
(
	@descripcion VARCHAR(50),
	@idCargo int out
)
AS BEGIN TRANSACTION
BEGIN TRY
		UPDATE Restaurante.Cargo SET Descripcion = @descripcion
		WHERE  [IdCargo]= @idCargo 
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
--INGRESAR LOS DATOS AL CLIENTE 
EXEC ActualizarCargo 'Gerente',1
GO
SELECT * FROM Restaurante.Cargo
GO

---PROCEDIMIENTO PARA PODER DAR MANTENIMIENTO A LA TABLA EMPLEADO

CREATE PROC GenerarIdEmpleado
@IdEmpleado INT OUT
AS BEGIN
	DECLARE @Cant AS INT
	IF(NOT EXISTS(SELECT IdEmpleado FROM Restaurante.Empleado))
		Set @IdEmpleado=1
	ELSE  BEGIN
		SET @IdEmpleado = (SELECT MAX(IdEmpleado) + 1 FROM Restaurante.Empleado)
		END
	End
GO
CREATE PROC MantenimientoEmpleados
(
	@IdEmpleado INT,
	@IdCargo INT,
	@IDN VARCHAR(18),
	@Apellidos VARCHAR(30),
	@Nombres VARCHAR(30),
	@Sexo CHAR(1),
	@FechaNac DATE,
	@Direccion VARCHAR(80),
	@EstadoCivil CHAR(1) OUT
)
AS BEGIN TRANSACTION
BEGIN TRY
	IF(NOT EXISTS(SELECT * FROM Restaurante.Empleado WHERE Identidad = @IDN))
		BEGIN
		INSERT Restaurante.Empleado VALUES(@IdCargo, @IDN, @Apellidos, @Nombres, @Sexo, @FechaNac, @Direccion, @EstadoCivil)
		END
	ELSE BEGIN
	IF(EXISTS(SELECT * FROM Restaurante.Empleado WHERE Identidad=@IDN))
		BEGIN
		UPDATE Restaurante.Empleado SET IdCargo = @IdCargo, Identidad = @IDN, Apellido = @Apellidos, Nombre = @Nombres, Sexo = @Sexo,
		FechaNac = @FechaNac, Direccion = @Direccion, EstadoCivil = @EstadoCivil Where IdEmpleado = @IdEmpleado
		END
	END
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
--INGRESARLE LOS DATOS A LA TABLA EMPLEADOS 
EXEC MantenimientoEmpleados 1,1,'0318199800356','Rosa','Rudy Roberto', 'F', '1998/05/15','A la par de un lado','S'
GO
SELECT * FROM Restaurante.Empleado
GO


---REGISTRAR UNA NUEVA CATEGORIA 
CREATE PROC RegistrarCategoria
@Descripcion VARCHAR(50) OUT
AS BEGIN TRANSACTION
BEGIN TRY
		INSERT Restaurante.Categoria VALUES(@Descripcion)
		COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
---ACTUALIZAR LA CATEGORIA
CREATE PROC ActualizarCategoria
@Descripcion VARCHAR(50),
@idCategoria int OUT
AS BEGIN TRANSACTION
BEGIN TRY
		UPDATE Restaurante.Categoria SET Descripcion = @Descripcion
		WHERE IdCategoria = @idCategoria
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
---INGRESA LOS DATOS A LA TABLA CATEGORIA

EXEC RegistrarCategoria 'PARRILLADA'
GO
SELECT * FROM Restaurante.Categoria
GO
---PROCEDIMIENTO ALMACENAMIENTO PARA VENTAS
CREATE PROC GenerarIdVenta
	@IdVenta INT OUT
AS BEGIN
	IF(Not Exists(SELECT  IdVenta FROM Restaurante.Venta))
		SET @IdVenta=1
	ELSE BEGIN
		SET @IdVenta=(SELECT MAX(IdVenta)+1 FROM Restaurante.Venta)
		END
	END
Go
--PROCEDIMIENTO ALMACENADO PARA PODER REGISTRAR UNA VENTA
CREATE PROC RegistrarVenta
	@IdEmpleado INT,
	@IdCliente INT,
	@NroDocumento CHAR(7),
	@Total MONEY OUT
AS BEGIN TRANSACTION
BEGIN TRY
	INSERT Restaurante.Venta VALUES(@IdEmpleado,@IdCliente,@NroDocumento,@Total,getdate())
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
GO
--INGRESAR LA VENTA 

EXEC RegistrarVenta 2,1,'AS1',400
GO
SELECT * FROM [Restaurante].[Venta]
GO
--PROCEDIMIENTO ALMACENADO PARA EL REGISTRO DETALLE VENTA 
CREATE PROC RegistrarDetalleVenta
	@IdProducto INT,
	@IdVenta INT,
	@Cantidad INT,
	@PrecioUnitario DECIMAL(6,2),
	@SubTotal MONEY OUT
AS BEGIN TRANSACTION
BEGIN TRY
	DECLARE @Stock As Int
	SET @Stock = (SELECT Stock FROM Restaurante.Producto WHERE IdProducto=@IdProducto)
	BEGIN
		INSERT  Restaurante.DetalleVenta VALUES(@IdProducto,@IdVenta,@Cantidad,@PrecioUnitariO,@SubTotal)
	END
		UPDATE Restaurante.Producto SET Stock=@Stock-@Cantidad WHERE IdProducto=@IdProducto
	COMMIT
END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION
END CATCH
Go

Insert Into Restaurante.Cargo (Descripcion)
Values	('Administrador'),
		('Gerente'),
		('Empleado');


EXEC RegistrarDetalleVenta 1,1,2,100,200
GO
SELECT * FROM Restaurante.VENTA
GO
SELECT * FROM Restaurante.Empleado
GO
SELECT * FROM Restaurante.Cliente
GO
SELECT * FROM Restaurante.Producto
GO
