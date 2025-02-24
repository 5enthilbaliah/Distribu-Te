BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] DROP CONSTRAINT [FK_deployment_item_tasks_associates_associate_id];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD CONSTRAINT [FK_deployment_item_tasks_associates_associate_id] FOREIGN KEY ([associate_id]) REFERENCES [associates] ([id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224054037_AssociateForeignKeys', N'9.0.2');
END;

COMMIT;
GO

