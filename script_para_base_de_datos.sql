IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'BD_RESTAURANTENHAWAI')
BEGIN
CREATE DATABASE  BD_RESTAURANTENHAWAI
END
go

use BD_RESTAURANTENHAWAI
GO

--Platos, Ventas, Reservas, Clientes
create table Platos(
id int IDENTITY(1,1) PRIMARY KEY,
nombre varchar(1000) not null,
descripcion varchar(1000) not null,
precio float not null,
categoria int not null,
imagen text not null
)
go

create table Ventas(
id int IDENTITY(1,1) PRIMARY KEY,
id_plato int,
fecha_hora datetime,
cantidad_vendida int,
CONSTRAINT fk_Plato FOREIGN KEY (id_plato) REFERENCES Platos(id)

)
go

create table Reservas(
id int IDENTITY(1,1) PRIMARY KEY,
fecha_hora datetime not null,
nombre_cliente varchar(1000),
cantidad_personas int
)
go

create table Clientes (
id int IDENTITY(1,1) PRIMARY KEY,
nombre varchar(1000) not null,
direccion varchar(1000) not null,
telefono varchar(1000) not null,
correo varchar(1000) not null,
administrador int not null,
)
go
