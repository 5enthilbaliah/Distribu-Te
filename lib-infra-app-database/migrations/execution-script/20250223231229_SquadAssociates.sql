BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[squads]') AND [c].[name] = N'name');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [squads] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [squads] ALTER COLUMN [name] varchar(45) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[squads]') AND [c].[name] = N'modified_by');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [squads] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [squads] ALTER COLUMN [modified_by] varchar(200) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[squads]') AND [c].[name] = N'description');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [squads] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [squads] ALTER COLUMN [description] varchar(200) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[squads]') AND [c].[name] = N'created_by');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [squads] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [squads] ALTER COLUMN [created_by] varchar(200) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[squads]') AND [c].[name] = N'code');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [squads] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [squads] ALTER COLUMN [code] varchar(20) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[projects]') AND [c].[name] = N'name');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [projects] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [projects] ALTER COLUMN [name] varchar(45) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[projects]') AND [c].[name] = N'modified_by');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [projects] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [projects] ALTER COLUMN [modified_by] varchar(45) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[projects]') AND [c].[name] = N'description');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [projects] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [projects] ALTER COLUMN [description] varchar(200) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[projects]') AND [c].[name] = N'created_by');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [projects] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [projects] ALTER COLUMN [created_by] varchar(45) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[projects]') AND [c].[name] = N'code');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [projects] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [projects] ALTER COLUMN [code] varchar(20) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[project_categories]') AND [c].[name] = N'name');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [project_categories] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [project_categories] ALTER COLUMN [name] varchar(45) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[project_categories]') AND [c].[name] = N'modified_by');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [project_categories] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [project_categories] ALTER COLUMN [modified_by] varchar(45) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[project_categories]') AND [c].[name] = N'description');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [project_categories] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [project_categories] ALTER COLUMN [description] varchar(200) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[project_categories]') AND [c].[name] = N'created_by');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [project_categories] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [project_categories] ALTER COLUMN [created_by] varchar(45) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[project_categories]') AND [c].[name] = N'code');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [project_categories] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [project_categories] ALTER COLUMN [code] varchar(20) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    CREATE TABLE [associates] (
        [id] int NOT NULL IDENTITY,
        [first_name] varchar(45) NOT NULL,
        [last_name] varchar(45) NOT NULL,
        [middle_name] varchar(45) NULL,
        [gender] char(1) NOT NULL,
        [email_id] varchar(45) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(45) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(45) NULL,
        CONSTRAINT [PK_associates] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    CREATE TABLE [squad_associates] (
        [associate_id] int NOT NULL,
        [squad_id] int NOT NULL,
        [started_on] datetime2(7) NOT NULL,
        [ended_on] datetime2(7) NULL,
        [capacity] decimal(5,4) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(45) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(45) NULL,
        CONSTRAINT [PK_squad_associates] PRIMARY KEY ([squad_id], [associate_id]),
        CONSTRAINT [FK_squad_associates_associates_associate_id] FOREIGN KEY ([associate_id]) REFERENCES [associates] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_squad_associates_squads_squad_id] FOREIGN KEY ([squad_id]) REFERENCES [squads] ([id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    CREATE INDEX [IX_squad_associates_associate_id] ON [squad_associates] ([associate_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223231229_SquadAssociates'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250223231229_SquadAssociates', N'9.0.2');
END;

COMMIT;
GO

