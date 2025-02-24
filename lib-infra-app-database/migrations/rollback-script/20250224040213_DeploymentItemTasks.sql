BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks'
)
BEGIN
    DROP TABLE [deployment_item_tasks];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks';
END;

COMMIT;
GO

