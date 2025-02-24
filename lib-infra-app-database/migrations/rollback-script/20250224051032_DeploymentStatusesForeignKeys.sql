BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] DROP CONSTRAINT [FK_deployment_items_deployment_statuses_status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    DROP INDEX [IX_deployment_items_status_id] ON [deployment_items];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[deployment_items]') AND [c].[name] = N'status_id');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [deployment_items] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [deployment_items] DROP COLUMN [status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys';
END;

COMMIT;
GO

