BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] ADD [status_id] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    CREATE INDEX [IX_deployment_items_status_id] ON [deployment_items] ([status_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_items] ADD CONSTRAINT [FK_deployment_items_deployment_statuses_status_id] FOREIGN KEY ([status_id]) REFERENCES [deployment_statuses] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051032_DeploymentStatusesForeignKeys'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224051032_DeploymentStatusesForeignKeys', N'9.0.2');
END;

COMMIT;
GO

