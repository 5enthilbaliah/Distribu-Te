BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3'
)
BEGIN
    ALTER TABLE [deployments] DROP CONSTRAINT [FK_deployments_deployment_statuses_status_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3'
)
BEGIN
    ALTER TABLE [deployments] ADD CONSTRAINT [FK_deployments_deployment_statuses_status_id] FOREIGN KEY ([status_id]) REFERENCES [deployment_statuses] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224052633_DeploymentStatusesForeignKeys3'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224052633_DeploymentStatusesForeignKeys3', N'9.0.2');
END;

COMMIT;
GO

