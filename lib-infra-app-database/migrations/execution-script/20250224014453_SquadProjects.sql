BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224014453_SquadProjects'
)
BEGIN
    CREATE TABLE [squad_projects] (
        [squad_id] int NOT NULL,
        [project_id] int NOT NULL,
        [started_on] datetime2(7) NOT NULL,
        [ended_on] datetime2(7) NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(45) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(45) NULL,
        CONSTRAINT [PK_squad_projects] PRIMARY KEY ([squad_id], [project_id]),
        CONSTRAINT [FK_squad_projects_projects_project_id] FOREIGN KEY ([project_id]) REFERENCES [projects] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_squad_projects_squads_squad_id] FOREIGN KEY ([squad_id]) REFERENCES [squads] ([id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224014453_SquadProjects'
)
BEGIN
    CREATE INDEX [IX_squad_projects_project_id] ON [squad_projects] ([project_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224014453_SquadProjects'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224014453_SquadProjects', N'9.0.2');
END;

COMMIT;
GO

