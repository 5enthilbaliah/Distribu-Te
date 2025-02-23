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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    CREATE TABLE [project_categories] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(45) NOT NULL,
        [code] nvarchar(20) NOT NULL,
        [description] nvarchar(200) NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] nvarchar(200) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] nvarchar(200) NULL,
        CONSTRAINT [PK_project_categories] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    CREATE TABLE [squads] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(45) NOT NULL,
        [code] nvarchar(20) NOT NULL,
        [description] nvarchar(200) NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] nvarchar(200) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] nvarchar(200) NULL,
        CONSTRAINT [PK_squads] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    CREATE TABLE [projects] (
        [id] int NOT NULL IDENTITY,
        [name] nvarchar(45) NOT NULL,
        [code] nvarchar(20) NOT NULL,
        [description] nvarchar(200) NULL,
        [category_id] int NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] nvarchar(200) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] nvarchar(200) NULL,
        CONSTRAINT [PK_projects] PRIMARY KEY ([id]),
        CONSTRAINT [FK_projects_project_categories_category_id] FOREIGN KEY ([category_id]) REFERENCES [project_categories] ([id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_projects_category_id] ON [projects] ([category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250223214443_InitialCreate', N'9.0.2');
END;

COMMIT;
GO

