IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Battles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [DtBegining] datetime2 NOT NULL,
    [DtEnd] datetime2 NOT NULL,
    CONSTRAINT [PK_Battles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Heros] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [BattleId] int NOT NULL,
    CONSTRAINT [PK_Heros] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Heros_Battles_BattleId] FOREIGN KEY ([BattleId]) REFERENCES [Battles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Guns] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [HeroId] int NOT NULL,
    CONSTRAINT [PK_Guns] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Guns_Heros_HeroId] FOREIGN KEY ([HeroId]) REFERENCES [Heros] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Guns_HeroId] ON [Guns] ([HeroId]);
GO

CREATE INDEX [IX_Heros_BattleId] ON [Heros] ([BattleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220723035511_initial', N'7.0.0-preview.6.22329.4');
GO

COMMIT;
GO

