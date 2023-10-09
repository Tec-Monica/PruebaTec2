CREATE DATABASE PruebaTec2;
GO

USE PruebaTec2; 

CREATE TABLE Roles(
Id INT NOT NULL IDENTITY(1,1),
Nombre NVARCHAR(30) NOT NULL,
PRIMARY KEY(Id)
);
GO

CREATE TABLE Tipos(
Id INT IDENTITY (1,1) NOT NULL,
Nombre NVARCHAR(50) NOT NULL,
PRIMARY KEY(Id)
);
GO

CREATE TABLE Aves(
Id INT IDENTITY (1,1) NOT NULL,
IdTipo INT NOT NULL,
Nombre NVARCHAR(30) NOT NULL,
Imagen NVARCHAR(max) NOT NULL,
Descripcion NVARCHAR(max) NOT NULL,
PRIMARY KEY(Id),
FOREIGN KEY(IdTipo) REFERENCES Tipos(Id)
);
GO

CREATE TABLE Usuarios(
Id INT NOT NULL IDENTITY(1,1),
IdRol INT NOT NULL,
Nombre NVARCHAR(50) NOT NULL,
Apellido NVARCHAR(50) NOT NULL,
[Login] NVARCHAR(25) NOT NULL,
[Password] NCHAR(50) NOT NULL,
Estatus TINYINT NOT NULL,
PRIMARY KEY(Id),
FOREIGN KEY(IdRol) REFERENCES Roles(Id)
);
GO

INSERT INTO Roles(Nombre) VALUES('Administrador');

INSERT INTO Tipos(Nombre) VALUES('Apodiformes');

INSERT INTO Aves(IdTipo, Nombre, Imagen, Descripcion) 
              VALUES(1, 'Colibri',' https://t0.gstatic.com/licensed-image?q=tbn:ANd9GcSRcaUTuRpCJ90FQszOIHbMWfN1KRYUTzF4eUNQXnY31xNZzQNk5QKsFED9qt_u39se ', 'son animales sumamente rápidos, pueden batir sus alas hasta por 70 veces por segundo manteniéndose en el mismo sitio mientras extrae el néctar de la flor. A menudo los tubos florales se adaptan muy bien con la longitud de la curva de sus picos.');

--Password Vasquez2023
INSERT INTO Usuarios(IdRol, Nombre, Apellido, [Login], [Password], Estatus)
VALUES(1, 'Gerson Antonio', 'Vásquez Ortega', 'gv', 'd8ddb8c6e7868a197093b9938aca2b35', 1);



