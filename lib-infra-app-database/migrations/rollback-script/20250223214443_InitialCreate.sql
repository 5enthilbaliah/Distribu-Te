BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    DROP TABLE [projects];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    DROP TABLE [squads];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    DROP TABLE [project_categories];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250223214443_InitialCreate';
END;

COMMIT;
GO

