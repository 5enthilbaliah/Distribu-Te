BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224022218_Environments'
)
BEGIN
    CREATE TABLE [environments] (
        [id] int NOT NULL IDENTITY,
        [name] varchar(45) NOT NULL,
        [code] varchar(20) NOT NULL,
        [created_on] datetime2(7) NOT NULL,
        [created_by] varchar(200) NOT NULL,
        [modified_on] datetime2(7) NULL,
        [modified_by] varchar(200) NULL,
        CONSTRAINT [PK_environments] PRIMARY KEY ([id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250224022218_Environments'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250224022218_Environments', N'9.0.2');
END;

COMMIT;
GO

