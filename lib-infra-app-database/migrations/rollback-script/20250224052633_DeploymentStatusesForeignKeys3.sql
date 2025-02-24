BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3'
)
BEGIN
    ALTER TABLE [deployments] DROP CONSTRAINT [FK_deployments_deployment_statuses_status_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3'
)
BEGIN
    ALTER TABLE [deployments] ADD CONSTRAINT [FK_deployments_deployment_statuses_status_id] FOREIGN KEY ([status_id]) REFERENCES [deployment_statuses] ([id]) ON DELETE CASCADE;
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3';
END;

COMMIT;
GO

