BEGIN TRANSACTION;
IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] DROP CONSTRAINT [FK_deployment_item_tasks_associates_associate_id];
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys'
)
BEGIN
    ALTER TABLE [deployment_item_tasks] ADD CONSTRAINT [FK_deployment_item_tasks_associates_associate_id] FOREIGN KEY ([associate_id]) REFERENCES [associates] ([id]) ON DELETE CASCADE;
END;

IF EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys'
)
BEGIN
    DELETE FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224054037_AssociateForeignKeys';
END;

COMMIT;
GO

