BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224025046_Deployments'
)
BEGIN
    CREATE TABLE [deployments] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(45) NOT NULL,
        [squad_id] int NOT NULL,
        [environment_id] int NOT NULL,
        [planned_start] datetime2(7) NOT NULL,
        [planned_end] datetime2(7) NOT NULL,
        [actual_start] datetime2(7) NULL,
        [actual_end] datetime2(7) NULL,
        [comments] nvarchar(2000) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(45) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(45) NULL,
        CONSTRAINT [PK_deployments] PRIMARY KEY ([id]),
        CONSTRAINT [FK_deployments_environments_environment_id] FOREIGN KEY ([environment_id]) REFERENCES [environments] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_deployments_squads_squad_id] FOREIGN KEY ([squad_id]) REFERENCES [squads] ([id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224025046_Deployments'
)
BEGIN
    CREATE INDEX [IX_deployments_environment_id] ON [deployments] ([environment_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224025046_Deployments'
)
BEGIN
    CREATE INDEX [IX_deployments_squad_id] ON [deployments] ([squad_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224025046_Deployments'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224025046_Deployments', N'9.0.2');
END;

COMMIT;
GO

