BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] DROP CONSTRAINT [FK_deployment_item_tasks_deployment_statuses_status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    DROP INDEX [IX_deployment_item_tasks_status_id] ON [deployment_item_tasks];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[deployment_item_tasks]') AND [c].[name] = N'status_id');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [deployment_item_tasks] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [deployment_item_tasks] DROP COLUMN [status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2';
END;

COMMIT;
GO

