BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224142118_TaskTypesForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD [task_type_id] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224142118_TaskTypesForeignKeys'
)
BEGIN
    CREATE INDEX [IX_deployment_item_tasks_task_type_id] ON [deployment_item_tasks] ([task_type_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224142118_TaskTypesForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD CONSTRAINT [FK_deployment_item_tasks_deployment_task_types_task_type_id] FOREIGN KEY ([task_type_id]) REFERENCES [deployment_task_types] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224142118_TaskTypesForeignKeys'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224142118_TaskTypesForeignKeys', N'9.0.2');
END;

COMMIT;
GO

