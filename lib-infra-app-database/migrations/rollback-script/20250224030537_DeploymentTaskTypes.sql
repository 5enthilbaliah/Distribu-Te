BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224030537_DeploymentTaskTypes'
)
BEGIN
    DROP TABLE [deployment_task_types];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224030537_DeploymentTaskTypes'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224030537_DeploymentTaskTypes';
END;

COMMIT;
GO

