BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks'
)
BEGIN
    CREATE TABLE [deployment_item_tasks] (
        [id] int NOT NULL IDENTITY,
        [deployment_item_id] int NOT NULL,
        [associate_id] int NOT NULL,
        [sequence] int NOT NULL,
        [actual_start] datetime2(7) NULL,
        [actual_end] datetime2(7) NULL,
        [comments] nvarchar(2000) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(45) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(45) NULL,
        CONSTRAINT [PK_deployment_item_tasks] PRIMARY KEY ([id]),
        CONSTRAINT [FK_deployment_item_tasks_associates_associate_id] FOREIGN KEY ([associate_id]) REFERENCES [associates] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_deployment_item_tasks_deployment_items_deployment_item_id] FOREIGN KEY ([deployment_item_id]) REFERENCES [deployment_items] ([id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks'
)
BEGIN
    CREATE INDEX [IX_deployment_item_tasks_associate_id] ON [deployment_item_tasks] ([associate_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks'
)
BEGIN
    CREATE INDEX [IX_deployment_item_tasks_deployment_item_id] ON [deployment_item_tasks] ([deployment_item_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224040213_DeploymentItemTasks'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224040213_DeploymentItemTasks', N'9.0.2');
END;

COMMIT;
GO

