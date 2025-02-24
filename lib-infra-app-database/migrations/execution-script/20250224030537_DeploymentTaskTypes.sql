BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224030537_DeploymentTaskTypes'
)
BEGIN
    CREATE TABLE [deployment_task_types] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(45) NOT NULL,
        [code] varchar(20) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(200) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(200) NULL,
        CONSTRAINT [PK_deployment_task_types] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224030537_DeploymentTaskTypes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224030537_DeploymentTaskTypes', N'9.0.2');
END;

COMMIT;
GO

