USE AcademicaDb;
GO
--drop table Profesores
--drop table Estudiantes
--drop table Materias
--drop table Inscripciones

CREATE TABLE Profesores (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    FechaGrabacion DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE Materias (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Creditos INT NOT NULL DEFAULT 3,
    ProfesorId UNIQUEIDENTIFIER NOT NULL,
    FechaGrabacion DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (ProfesorId) REFERENCES Profesores(Id)
);

CREATE TABLE Estudiantes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId NVARCHAR(100) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaGrabacion DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE Inscripciones (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    EstudianteId UNIQUEIDENTIFIER NOT NULL,
    MateriaId UNIQUEIDENTIFIER NOT NULL,
    FechaGrabacion DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id),
    FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
);
