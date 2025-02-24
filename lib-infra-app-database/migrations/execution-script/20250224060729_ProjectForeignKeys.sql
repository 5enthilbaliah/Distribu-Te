BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] DROP CONSTRAINT [FK_deployment_items_projects_project_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [deployments] DROP CONSTRAINT [FK_deployments_squads_squad_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [projects] DROP CONSTRAINT [FK_projects_project_categories_category_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_associates] DROP CONSTRAINT [FK_squad_associates_associates_associate_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_associates] DROP CONSTRAINT [FK_squad_associates_squads_squad_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_projects] DROP CONSTRAINT [FK_squad_projects_projects_project_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_projects] DROP CONSTRAINT [FK_squad_projects_squads_squad_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] ADD CONSTRAINT [FK_deployment_items_projects_project_id] FOREIGN KEY ([project_id]) REFERENCES [projects] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [deployments] ADD CONSTRAINT [FK_deployments_squads_squad_id] FOREIGN KEY ([squad_id]) REFERENCES [squads] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [projects] ADD CONSTRAINT [FK_projects_project_categories_category_id] FOREIGN KEY ([category_id]) REFERENCES [project_categories] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_associates] ADD CONSTRAINT [FK_squad_associates_associates_associate_id] FOREIGN KEY ([associate_id]) REFERENCES [associates] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_associates] ADD CONSTRAINT [FK_squad_associates_squads_squad_id] FOREIGN KEY ([squad_id]) REFERENCES [squads] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_projects] ADD CONSTRAINT [FK_squad_projects_projects_project_id] FOREIGN KEY ([project_id]) REFERENCES [projects] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    ALTER TABLE [squad_projects] ADD CONSTRAINT [FK_squad_projects_squads_squad_id] FOREIGN KEY ([squad_id]) REFERENCES [squads] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224060729_ProjectForeignKeys'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224060729_ProjectForeignKeys', N'9.0.2');
END;

COMMIT;
GO

