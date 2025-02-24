BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    ALTER TABLE [deployments] DROP CONSTRAINT [FK_deployments_deployment_statuses_status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    DROP TABLE [deployment_statuses];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    DROP INDEX [IX_deployments_status_id] ON [deployments];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[deployments]') AND [c].[name] = N'status_id');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [deployments] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [deployments] DROP COLUMN [status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses';
END;

COMMIT;
GO

