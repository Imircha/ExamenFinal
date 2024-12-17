-- Crear la base de datos
CREATE DATABASE ConstructoraUH;
GO

USE ConstructoraUH;
GO

-- Tabla Empleados
CREATE TABLE Empleados (
    CarnetUnico INT PRIMARY KEY,
    NombreCompleto NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Direccion NVARCHAR(200) DEFAULT 'San José',
    Telefono NVARCHAR(20) NOT NULL,
    CorreoElectronico NVARCHAR(100) NOT NULL UNIQUE,
    Salario DECIMAL(18, 2) NOT NULL DEFAULT 250000 CHECK (Salario >= 250000 AND Salario <= 500000),
    CategoriaLaboral INT NOT NULL
);

-- Tabla Proyectos
CREATE TABLE Proyectos (
    CodigoProyecto INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFinalizacion DATE NULL
);

-- Tabla Asignaciones
CREATE TABLE Asignaciones (
    AsignacionId INT PRIMARY KEY IDENTITY(1,1),
    EmpleadoId INT NOT NULL,
    ProyectoId INT NOT NULL,
    FechaAsignacion DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (EmpleadoId) REFERENCES Empleados(CarnetUnico),
    FOREIGN KEY (ProyectoId) REFERENCES Proyectos(CodigoProyecto)
);

-- Tablas para ASP.NET Identity

-- Roles
CREATE TABLE AspNetRoles (
    Id NVARCHAR(450) PRIMARY KEY,
    Name NVARCHAR(256) NULL,
    NormalizedName NVARCHAR(256) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL
);

-- Usuarios
CREATE TABLE AspNetUsers (
    Id NVARCHAR(450) PRIMARY KEY,
    UserName NVARCHAR(256) NULL,
    NormalizedUserName NVARCHAR(256) NULL,
    Email NVARCHAR(256) NULL,
    NormalizedEmail NVARCHAR(256) NULL,
    EmailConfirmed BIT NOT NULL,
    PasswordHash NVARCHAR(MAX) NULL,
    SecurityStamp NVARCHAR(MAX) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL,
    PhoneNumber NVARCHAR(MAX) NULL,
    PhoneNumberConfirmed BIT NOT NULL,
    TwoFactorEnabled BIT NOT NULL,
    LockoutEnd DATETIMEOFFSET NULL,
    LockoutEnabled BIT NOT NULL,
    AccessFailedCount INT NOT NULL
);

-- Roles de Usuario
CREATE TABLE AspNetUserRoles (
    UserId NVARCHAR(450) NOT NULL,
    RoleId NVARCHAR(450) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id)
);

-- Claims de Usuario
CREATE TABLE AspNetUserClaims (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId NVARCHAR(450) NOT NULL,
    ClaimType NVARCHAR(MAX) NULL,
    ClaimValue NVARCHAR(MAX) NULL,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

-- Logins de Usuario
CREATE TABLE AspNetUserLogins (
    LoginProvider NVARCHAR(128) NOT NULL,
    ProviderKey NVARCHAR(128) NOT NULL,
    ProviderDisplayName NVARCHAR(MAX) NULL,
    UserId NVARCHAR(450) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

-- Tokens de Usuario
CREATE TABLE AspNetUserTokens (
    UserId NVARCHAR(450) NOT NULL,
    LoginProvider NVARCHAR(128) NOT NULL,
    Name NVARCHAR(128) NOT NULL,
    Value NVARCHAR(MAX) NULL,
    PRIMARY KEY (UserId, LoginProvider, Name),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

-- Claims de Rol
CREATE TABLE AspNetRoleClaims (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleId NVARCHAR(450) NOT NULL,
    ClaimType NVARCHAR(MAX) NULL,
    ClaimValue NVARCHAR(MAX) NULL,
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id)
);

-- Índices para mejorar el rendimiento
CREATE INDEX IX_AspNetUsers_NormalizedUserName ON AspNetUsers (NormalizedUserName);
CREATE INDEX IX_AspNetUsers_NormalizedEmail ON AspNetUsers (NormalizedEmail);
CREATE INDEX IX_AspNetRoles_NormalizedName ON AspNetRoles (NormalizedName);
CREATE INDEX IX_Empleados_CorreoElectronico ON Empleados (CorreoElectronico);
CREATE INDEX IX_Asignaciones_EmpleadoId ON Asignaciones (EmpleadoId);
CREATE INDEX IX_Asignaciones_ProyectoId ON Asignaciones (ProyectoId);

select * from Empleados;