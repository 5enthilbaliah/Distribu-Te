BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_squads_code] ON [squads];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_squads_name] ON [squads];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_projects_code] ON [projects];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_projects_name] ON [projects];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_project_categories_code] ON [project_categories];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_project_categories_name] ON [project_categories];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_environments_code] ON [environments];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_environments_name] ON [environments];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_deployments_name] ON [deployments];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_deployment_task_types_code] ON [deployment_task_types];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_deployment_task_types_name] ON [deployment_task_types];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DROP INDEX [IX_associates_email_id] ON [associates];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints';
END;

COMMIT;
GO

