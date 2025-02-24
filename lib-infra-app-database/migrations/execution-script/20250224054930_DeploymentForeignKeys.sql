BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] DROP CONSTRAINT [FK_deployment_item_tasks_deployment_items_deployment_item_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] DROP CONSTRAINT [FK_deployment_items_deployments_deployment_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    ALTER TABLE [deployments] DROP CONSTRAINT [FK_deployments_environments_environment_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD CONSTRAINT [FK_deployment_item_tasks_deployment_items_deployment_item_id] FOREIGN KEY ([deployment_item_id]) REFERENCES [deployment_items] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] ADD CONSTRAINT [FK_deployment_items_deployments_deployment_id] FOREIGN KEY ([deployment_id]) REFERENCES [deployments] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    ALTER TABLE [deployments] ADD CONSTRAINT [FK_deployments_environments_environment_id] FOREIGN KEY ([environment_id]) REFERENCES [environments] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054930_DeploymentForeignKeys'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224054930_DeploymentForeignKeys', N'9.0.2');
END;

COMMIT;
GO

