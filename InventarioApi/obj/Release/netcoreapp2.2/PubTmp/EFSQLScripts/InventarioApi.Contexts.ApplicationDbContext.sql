IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE TABLE [Areas] (
        [Id] bigint NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Areas] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE TABLE [Tdocuemntos] (
        [Id] bigint NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        [Sigla] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Tdocuemntos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE TABLE [Telementos] (
        [Id] bigint NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Telementos] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE TABLE [Personas] (
        [Id] bigint NOT NULL IDENTITY,
        [Documento] nvarchar(max) NOT NULL,
        [PrimerNombre] nvarchar(max) NOT NULL,
        [SegundoNombre] nvarchar(max) NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [FechaNacimiento] datetime2 NOT NULL,
        [Direccion] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [TdocumentoId] bigint NOT NULL,
        [AreaId] bigint NOT NULL,
        CONSTRAINT [PK_Personas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Personas_Areas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [Areas] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Personas_Tdocuemntos_TdocumentoId] FOREIGN KEY ([TdocumentoId]) REFERENCES [Tdocuemntos] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE TABLE [Elementos] (
        [Id] bigint NOT NULL IDENTITY,
        [Descripcion] nvarchar(max) NULL,
        [Serial] nvarchar(max) NULL,
        [ValorCompra] float NOT NULL,
        [FechaCompra] datetime2 NOT NULL,
        [Estado] nvarchar(max) NULL,
        [TelementoId] bigint NOT NULL,
        CONSTRAINT [PK_Elementos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Elementos_Telementos_TelementoId] FOREIGN KEY ([TelementoId]) REFERENCES [Telementos] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE TABLE [PersonaElementos] (
        [Id] bigint NOT NULL IDENTITY,
        [ObservacionAsignacion] nvarchar(max) NULL,
        [ObservacionRetiro] nvarchar(max) NULL,
        [FechaAsignacion] datetime2 NOT NULL,
        [FechaRetiro] datetime2 NOT NULL,
        [PersonaId] bigint NOT NULL,
        [ElementoId] bigint NOT NULL,
        CONSTRAINT [PK_PersonaElementos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PersonaElementos_Elementos_ElementoId] FOREIGN KEY ([ElementoId]) REFERENCES [Elementos] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PersonaElementos_Personas_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Personas] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE INDEX [IX_Elementos_TelementoId] ON [Elementos] ([TelementoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE INDEX [IX_PersonaElementos_ElementoId] ON [PersonaElementos] ([ElementoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE INDEX [IX_PersonaElementos_PersonaId] ON [PersonaElementos] ([PersonaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE INDEX [IX_Personas_AreaId] ON [Personas] ([AreaId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    CREATE INDEX [IX_Personas_TdocumentoId] ON [Personas] ([TdocumentoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825130711_inicial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190825130711_inicial', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825153002_Elementos')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Personas]') AND [c].[name] = N'Documento');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Personas] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Personas] ALTER COLUMN [Documento] nvarchar(10) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825153002_Elementos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190825153002_Elementos', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825153312_telementos')
BEGIN
    ALTER TABLE [Elementos] DROP CONSTRAINT [FK_Elementos_Telementos_TelementoId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825153312_telementos')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Elementos]') AND [c].[name] = N'TelementoId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Elementos] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Elementos] ALTER COLUMN [TelementoId] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825153312_telementos')
BEGIN
    ALTER TABLE [Elementos] ADD CONSTRAINT [FK_Elementos_Telementos_TelementoId] FOREIGN KEY ([TelementoId]) REFERENCES [Telementos] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825153312_telementos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190825153312_telementos', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825154001_nuevo_elementos')
BEGIN
    ALTER TABLE [Elementos] DROP CONSTRAINT [FK_Elementos_Telementos_TelementoId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825154001_nuevo_elementos')
BEGIN
    DROP INDEX [IX_Elementos_TelementoId] ON [Elementos];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Elementos]') AND [c].[name] = N'TelementoId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Elementos] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Elementos] ALTER COLUMN [TelementoId] bigint NOT NULL;
    CREATE INDEX [IX_Elementos_TelementoId] ON [Elementos] ([TelementoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825154001_nuevo_elementos')
BEGIN
    ALTER TABLE [Elementos] ADD CONSTRAINT [FK_Elementos_Telementos_TelementoId] FOREIGN KEY ([TelementoId]) REFERENCES [Telementos] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190825154001_nuevo_elementos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190825154001_nuevo_elementos', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827135211_email')
BEGIN
    ALTER TABLE [Personas] ADD [Email] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827135211_email')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190827135211_email', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827222037_tdocumentos')
BEGIN
    ALTER TABLE [Personas] DROP CONSTRAINT [FK_Personas_Tdocuemntos_TdocumentoId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827222037_tdocumentos')
BEGIN
    ALTER TABLE [Tdocuemntos] DROP CONSTRAINT [PK_Tdocuemntos];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827222037_tdocumentos')
BEGIN
    EXEC sp_rename N'[Tdocuemntos]', N'Tdocumentos';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827222037_tdocumentos')
BEGIN
    ALTER TABLE [Tdocumentos] ADD CONSTRAINT [PK_Tdocumentos] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827222037_tdocumentos')
BEGIN
    ALTER TABLE [Personas] ADD CONSTRAINT [FK_Personas_Tdocumentos_TdocumentoId] FOREIGN KEY ([TdocumentoId]) REFERENCES [Tdocumentos] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190827222037_tdocumentos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190827222037_tdocumentos', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190828005445_final')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190828005445_final', N'2.2.6-servicing-10079');
END;

GO

