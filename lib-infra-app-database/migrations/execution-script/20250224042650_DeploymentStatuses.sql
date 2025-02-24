BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    ALTER TABLE [deployments] ADD [status_id] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    CREATE TABLE [deployment_statuses] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(45) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(200) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(200) NULL,
        CONSTRAINT [PK_deployment_statuses] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    CREATE INDEX [IX_deployments_status_id] ON [deployments] ([status_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    ALTER TABLE [deployments] ADD CONSTRAINT [FK_deployments_deployment_statuses_status_id] FOREIGN KEY ([status_id]) REFERENCES [deployment_statuses] ([id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224042650_DeploymentStatuses'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224042650_DeploymentStatuses', N'9.0.2');
END;

COMMIT;
GO

