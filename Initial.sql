IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Entidad] (
    [EntidadId] bigint NOT NULL IDENTITY,
    [Nombre] nvarchar(255) NOT NULL,
    [RazonSocial] nvarchar(255) NOT NULL,
    [RUC] nvarchar(255) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(255) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [Direccion] nvarchar(255) NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Entidad] PRIMARY KEY ([EntidadId])
);
GO

CREATE TABLE [Permissions] (
    [PermissionId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([PermissionId])
);
GO

CREATE TABLE [Area] (
    [AreaId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [IdParent] bigint NOT NULL,
    [IdEntidad] bigint NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Area] PRIMARY KEY ([AreaId]),
    CONSTRAINT [FK_Area_Entidad_IdEntidad] FOREIGN KEY ([IdEntidad]) REFERENCES [Entidad] ([EntidadId])
);
GO

CREATE TABLE [Groups] (
    [GroupId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [IdEntidad] bigint NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([GroupId]),
    CONSTRAINT [FK_Groups_Entidad_IdEntidad] FOREIGN KEY ([IdEntidad]) REFERENCES [Entidad] ([EntidadId])
);
GO

CREATE TABLE [Users] (
    [UserId] bigint NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Email] nvarchar(150) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [IdEntidad] bigint NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Entidad_IdEntidad] FOREIGN KEY ([IdEntidad]) REFERENCES [Entidad] ([EntidadId])
);
GO

CREATE TABLE [Maquina] (
    [MaquinaId] bigint NOT NULL IDENTITY,
    [IdArea] bigint NOT NULL,
    [Name] nvarchar(150) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [TipoMaquina] nvarchar(150) NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Maquina] PRIMARY KEY ([MaquinaId]),
    CONSTRAINT [FK_Maquina_Area_IdArea] FOREIGN KEY ([IdArea]) REFERENCES [Area] ([AreaId])
);
GO

CREATE TABLE [GroupPermission] (
    [GroupPermissionId] bigint NOT NULL IDENTITY,
    [GroupId] bigint NOT NULL,
    [PermissionId] bigint NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_GroupPermission] PRIMARY KEY ([GroupPermissionId]),
    CONSTRAINT [FK_GroupPermission_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([GroupId]),
    CONSTRAINT [FK_GroupPermission_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([PermissionId])
);
GO

CREATE TABLE [GroupUsers] (
    [GroupUsersId] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [GroupId] bigint NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_GroupUsers] PRIMARY KEY ([GroupUsersId]),
    CONSTRAINT [FK_GroupUsers_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([GroupId]),
    CONSTRAINT [FK_GroupUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [Componente] (
    [ComponenteId] bigint NOT NULL IDENTITY,
    [IdMaquina] bigint NOT NULL,
    [Name] nvarchar(150) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    [Potencia] int NOT NULL,
    [Velocidad] int NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Componente] PRIMARY KEY ([ComponenteId]),
    CONSTRAINT [FK_Componente_Maquina_IdMaquina] FOREIGN KEY ([IdMaquina]) REFERENCES [Maquina] ([MaquinaId])
);
GO

CREATE TABLE [PuntoMonitoreo] (
    [PuntoMonitoreoId] bigint NOT NULL IDENTITY,
    [IdComponente] bigint NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Detail] nvarchar(max) NOT NULL,
    [DireccionMedicion] nvarchar(255) NOT NULL,
    [AnguloDireccion] nvarchar(255) NOT NULL,
    [DatosMedicion] nvarchar(max) NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_PuntoMonitoreo] PRIMARY KEY ([PuntoMonitoreoId]),
    CONSTRAINT [FK_PuntoMonitoreo_Componente_IdComponente] FOREIGN KEY ([IdComponente]) REFERENCES [Componente] ([ComponenteId])
);
GO

CREATE TABLE [Metrica] (
    [MetricaId] bigint NOT NULL IDENTITY,
    [IdPuntoMonitoreo] bigint NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Tipo] int NOT NULL,
    [State] int NOT NULL,
    [AuditCreateUser] bigint NULL,
    [AuditCreateDate] datetime2 NOT NULL,
    [AuditUpdateUser] bigint NULL,
    [AuditUpdateDate] datetime2 NULL,
    [AuditDeleteUser] bigint NULL,
    [AuditDeleteDate] datetime2 NULL,
    CONSTRAINT [PK_Metrica] PRIMARY KEY ([MetricaId]),
    CONSTRAINT [FK_Metrica_PuntoMonitoreo_IdPuntoMonitoreo] FOREIGN KEY ([IdPuntoMonitoreo]) REFERENCES [PuntoMonitoreo] ([PuntoMonitoreoId])
);
GO

CREATE INDEX [IX_Area_IdEntidad] ON [Area] ([IdEntidad]);
GO

CREATE INDEX [IX_Componente_IdMaquina] ON [Componente] ([IdMaquina]);
GO

CREATE INDEX [IX_GroupPermission_GroupId] ON [GroupPermission] ([GroupId]);
GO

CREATE INDEX [IX_GroupPermission_PermissionId] ON [GroupPermission] ([PermissionId]);
GO

CREATE INDEX [IX_Groups_IdEntidad] ON [Groups] ([IdEntidad]);
GO

CREATE INDEX [IX_GroupUsers_GroupId] ON [GroupUsers] ([GroupId]);
GO

CREATE INDEX [IX_GroupUsers_UserId] ON [GroupUsers] ([UserId]);
GO

CREATE INDEX [IX_Maquina_IdArea] ON [Maquina] ([IdArea]);
GO

CREATE INDEX [IX_Metrica_IdPuntoMonitoreo] ON [Metrica] ([IdPuntoMonitoreo]);
GO

CREATE INDEX [IX_PuntoMonitoreo_IdComponente] ON [PuntoMonitoreo] ([IdComponente]);
GO

CREATE INDEX [IX_Users_IdEntidad] ON [Users] ([IdEntidad]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240607023909_Initial_Migrations', N'8.0.0');
GO

COMMIT;
GO

