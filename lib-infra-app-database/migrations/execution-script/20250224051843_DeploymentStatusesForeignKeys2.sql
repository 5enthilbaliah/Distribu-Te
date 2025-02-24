BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD [status_id] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    CREATE INDEX [IX_deployment_item_tasks_status_id] ON [deployment_item_tasks] ([status_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD CONSTRAINT [FK_deployment_item_tasks_deployment_statuses_status_id] FOREIGN KEY ([status_id]) REFERENCES [deployment_statuses] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224051843_DeploymentStatusesForeignKeys2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224051843_DeploymentStatusesForeignKeys2', N'9.0.2');
END;

COMMIT;
GO

