BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_squads_code] ON [squads] ([code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_squads_name] ON [squads] ([name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_projects_code] ON [projects] ([code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_projects_name] ON [projects] ([name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_project_categories_code] ON [project_categories] ([code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_project_categories_name] ON [project_categories] ([name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_environments_code] ON [environments] ([code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_environments_name] ON [environments] ([name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_deployments_name] ON [deployments] ([name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_deployment_task_types_code] ON [deployment_task_types] ([code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_deployment_task_types_name] ON [deployment_task_types] ([name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    CREATE UNIQUE INDEX [IX_associates_email_id] ON [associates] ([email_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250301152854_AddUniqueContraints'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250301152854_AddUniqueContraints', N'9.0.2');
END;

COMMIT;
GO

