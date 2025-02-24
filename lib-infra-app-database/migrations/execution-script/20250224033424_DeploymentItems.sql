BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224033424_DeploymentItems'
)
BEGIN
    CREATE TABLE [deployment_items] (
        [id] int NOT NULL IDENTITY,
        [deployment_id] int NOT NULL,
        [project_id] int NOT NULL,
        [sequence] int NOT NULL,
        [actual_start] datetime2(7) NULL,
        [actual_end] datetime2(7) NULL,
        [comments] nvarchar(2000) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(45) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(45) NULL,
        CONSTRAINT [PK_deployment_items] PRIMARY KEY ([id]),
        CONSTRAINT [FK_deployment_items_deployments_deployment_id] FOREIGN KEY ([deployment_id]) REFERENCES [deployments] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_deployment_items_projects_project_id] FOREIGN KEY ([project_id]) REFERENCES [projects] ([id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224033424_DeploymentItems'
)
BEGIN
    CREATE INDEX [IX_deployment_items_deployment_id] ON [deployment_items] ([deployment_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224033424_DeploymentItems'
)
BEGIN
    CREATE INDEX [IX_deployment_items_project_id] ON [deployment_items] ([project_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224033424_DeploymentItems'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224033424_DeploymentItems', N'9.0.2');
END;

COMMIT;
GO

