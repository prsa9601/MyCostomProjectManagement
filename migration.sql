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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [AboutStats] (
        [Id] uniqueidentifier NOT NULL,
        [CustomerSatisfaction] int NOT NULL,
        [ExperienceYears] int NOT NULL,
        [ActiveCustomers] int NOT NULL,
        [SuccessProjects] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_AboutStats] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [ContactUs] (
        [Id] uniqueidentifier NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Subject] nvarchar(max) NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ContactUs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [GeneralSiteInformation] (
        [Id] uniqueidentifier NOT NULL,
        [TeamName] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_GeneralSiteInformation] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [Packages] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] int NOT NULL,
        [Badge] int NOT NULL,
        [ExpiresAt] datetime2 NOT NULL,
        [Features] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Packages] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [Portfolios] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Portfolios] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [Projects] (
        [Id] uniqueidentifier NOT NULL,
        [MyProperty] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [ProjectSectionSetting] (
        [Id] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectSectionSetting] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [Roles] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [SpecializedServicesSection] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_SpecializedServicesSection] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [TechnicalSkills] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [SkillPercentage] int NOT NULL,
        [Icon] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_TechnicalSkills] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [PhoneNumberIsVerify] bit NOT NULL,
        [Email] nvarchar(max) NULL,
        [HashPassword] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [AboutMeSection] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Iamage] nvarchar(max) NOT NULL,
        [AboutStatsId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_AboutMeSection] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AboutMeSection_AboutStats_AboutStatsId] FOREIGN KEY ([AboutStatsId]) REFERENCES [AboutStats] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [ProjectSectionSettingOption] (
        [Id] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [ProjectIds] nvarchar(max) NOT NULL,
        [ProjectSectionSettingId] uniqueidentifier NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectSectionSettingOption] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProjectSectionSettingOption_ProjectSectionSetting_ProjectSectionSettingId] FOREIGN KEY ([ProjectSectionSettingId]) REFERENCES [ProjectSectionSetting] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [RolePermission] (
        [Id] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        [Permissions] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_RolePermission] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RolePermission_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [SpecializedServicesSectionOptions] (
        [Id] uniqueidentifier NOT NULL,
        [Icon] nvarchar(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [SpecializedServicesSectionId] uniqueidentifier NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_SpecializedServicesSectionOptions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SpecializedServicesSectionOptions_SpecializedServicesSection_SpecializedServicesSectionId] FOREIGN KEY ([SpecializedServicesSectionId]) REFERENCES [SpecializedServicesSection] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [UserBlackList] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserBlackList] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserBlackList_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [UserOtp] (
        [Id] uniqueidentifier NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserOtp] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserOtp_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [UserOtpSession] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [Token] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserOtpSession] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserOtpSession_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [UserRole] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserRole] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserRole_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [UserSession] (
        [Id] uniqueidentifier NOT NULL,
        [HashRefreshToken] nvarchar(max) NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserSession] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserSession_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [UserSessionBlackList] (
        [Id] uniqueidentifier NOT NULL,
        [HashToken] nvarchar(max) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ExpireDate] datetime2 NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_UserSessionBlackList] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserSessionBlackList_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [SiteSettings] (
        [Id] uniqueidentifier NOT NULL,
        [SiteIsActive] bit NOT NULL,
        [GeneralSiteInformationId] uniqueidentifier NOT NULL,
        [ProjectSectionSettingId] uniqueidentifier NOT NULL,
        [SpecializedServicesSectionId] uniqueidentifier NOT NULL,
        [AboutMeSectionId] uniqueidentifier NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_SiteSettings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SiteSettings_AboutMeSection_AboutMeSectionId] FOREIGN KEY ([AboutMeSectionId]) REFERENCES [AboutMeSection] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId] FOREIGN KEY ([GeneralSiteInformationId]) REFERENCES [GeneralSiteInformation] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId] FOREIGN KEY ([ProjectSectionSettingId]) REFERENCES [ProjectSectionSetting] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId] FOREIGN KEY ([SpecializedServicesSectionId]) REFERENCES [SpecializedServicesSection] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE TABLE [MenuSetting] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Sequence] int NOT NULL,
        [ImplementationStyle] int NOT NULL,
        [SiteSettingId] uniqueidentifier NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_MenuSetting] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MenuSetting_SiteSettings_SiteSettingId] FOREIGN KEY ([SiteSettingId]) REFERENCES [SiteSettings] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_AboutMeSection_AboutStatsId] ON [AboutMeSection] ([AboutStatsId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_MenuSetting_SiteSettingId] ON [MenuSetting] ([SiteSettingId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_ProjectSectionSettingOption_ProjectSectionSettingId] ON [ProjectSectionSettingOption] ([ProjectSectionSettingId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_RolePermission_RoleId] ON [RolePermission] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_SiteSettings_AboutMeSectionId] ON [SiteSettings] ([AboutMeSectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_SiteSettings_GeneralSiteInformationId] ON [SiteSettings] ([GeneralSiteInformationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_SiteSettings_ProjectSectionSettingId] ON [SiteSettings] ([ProjectSectionSettingId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_SiteSettings_SpecializedServicesSectionId] ON [SiteSettings] ([SpecializedServicesSectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_SpecializedServicesSectionOptions_SpecializedServicesSectionId] ON [SpecializedServicesSectionOptions] ([SpecializedServicesSectionId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_UserBlackList_UserId] ON [UserBlackList] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_UserOtp_UserId] ON [UserOtp] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_UserOtpSession_UserId] ON [UserOtpSession] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_UserSession_UserId] ON [UserSession] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    CREATE INDEX [IX_UserSessionBlackList_UserId] ON [UserSessionBlackList] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718165649_Start'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260718165649_Start', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718170816_SetNullableFieldFromUserTable'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'HashPassword');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [Users] ALTER COLUMN [HashPassword] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718170816_SetNullableFieldFromUserTable'
)
BEGIN
    DECLARE @var1 nvarchar(max);
    SELECT @var1 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'FullName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT ' + @var1 + ';');
    ALTER TABLE [Users] ALTER COLUMN [FullName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260718170816_SetNullableFieldFromUserTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260718170816_SetNullableFieldFromUserTable', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260719212249_RoleChange'
)
BEGIN
    ALTER TABLE [Roles] ADD [Description] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260719212249_RoleChange'
)
BEGIN
    ALTER TABLE [Roles] ADD [Icon] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260719212249_RoleChange'
)
BEGIN
    ALTER TABLE [Roles] ADD [IconColorCode] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260719212249_RoleChange'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260719212249_RoleChange', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260721154102_AlterTableRoleAddIsDefaultColumn'
)
BEGIN
    ALTER TABLE [Roles] ADD [IsDefault] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260721154102_AlterTableRoleAddIsDefaultColumn'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260721154102_AlterTableRoleAddIsDefaultColumn', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723155329_AlterSkillTable'
)
BEGIN
    ALTER TABLE [TechnicalSkills] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723155329_AlterSkillTable'
)
BEGIN
    ALTER TABLE [TechnicalSkills] ADD [SkillTypes] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723155329_AlterSkillTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260723155329_AlterSkillTable', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [Portfolios] DROP CONSTRAINT [PK_Portfolios];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    IF SCHEMA_ID(N'portfolio') IS NULL EXEC(N'CREATE SCHEMA [portfolio];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    EXEC sp_rename N'[Portfolios]', N'PortfolioFiles', 'OBJECT';
    ALTER SCHEMA [portfolio] TRANSFER [PortfolioFiles];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    EXEC sp_rename N'[portfolio].[PortfolioFiles].[Image]', N'File_VideoAddress', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [Category] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [File_CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [File_Id] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [File_ImageAddress] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [File_IsImage] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [File_IsVideo] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD [Link] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    ALTER TABLE [portfolio].[PortfolioFiles] ADD CONSTRAINT [PK_PortfolioFiles] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260723235104_AlterPortfolioTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260723235104_AlterPortfolioTable', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260724184807_SetNullableFieldsFromPortfolioFileTable'
)
BEGIN
    DECLARE @var2 nvarchar(max);
    SELECT @var2 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[portfolio].[PortfolioFiles]') AND [c].[name] = N'File_VideoAddress');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [portfolio].[PortfolioFiles] DROP CONSTRAINT ' + @var2 + ';');
    ALTER TABLE [portfolio].[PortfolioFiles] ALTER COLUMN [File_VideoAddress] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260724184807_SetNullableFieldsFromPortfolioFileTable'
)
BEGIN
    DECLARE @var3 nvarchar(max);
    SELECT @var3 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[portfolio].[PortfolioFiles]') AND [c].[name] = N'File_ImageAddress');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [portfolio].[PortfolioFiles] DROP CONSTRAINT ' + @var3 + ';');
    ALTER TABLE [portfolio].[PortfolioFiles] ALTER COLUMN [File_ImageAddress] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260724184807_SetNullableFieldsFromPortfolioFileTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260724184807_SetNullableFieldsFromPortfolioFileTable', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260726160151_CreateFAQTable'
)
BEGIN
    CREATE TABLE [FAQs] (
        [Id] uniqueidentifier NOT NULL,
        [Question] nvarchar(max) NOT NULL,
        [Answer] nvarchar(max) NOT NULL,
        [Sequense] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_FAQs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260726160151_CreateFAQTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260726160151_CreateFAQTable', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728194553_AddSiteSetting'
)
BEGIN
    EXEC sp_rename N'[AboutMeSection].[Iamage]', N'Image', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728194553_AddSiteSetting'
)
BEGIN
    CREATE TABLE [SiteLinks] (
        [Id] uniqueidentifier NOT NULL,
        [IsActive] bit NOT NULL,
        [Sequense] int NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Icon] nvarchar(max) NOT NULL,
        [SiteSettingId] uniqueidentifier NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_SiteLinks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SiteLinks_SiteSettings_SiteSettingId] FOREIGN KEY ([SiteSettingId]) REFERENCES [SiteSettings] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728194553_AddSiteSetting'
)
BEGIN
    CREATE INDEX [IX_SiteLinks_SiteSettingId] ON [SiteLinks] ([SiteSettingId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728194553_AddSiteSetting'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260728194553_AddSiteSetting', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    ALTER TABLE [SiteSettings] DROP CONSTRAINT [FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    ALTER TABLE [SiteSettings] DROP CONSTRAINT [FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    ALTER TABLE [SiteSettings] DROP CONSTRAINT [FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    DECLARE @var4 nvarchar(max);
    SELECT @var4 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteSettings]') AND [c].[name] = N'SpecializedServicesSectionId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [SiteSettings] DROP CONSTRAINT ' + @var4 + ';');
    ALTER TABLE [SiteSettings] ALTER COLUMN [SpecializedServicesSectionId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    DECLARE @var5 nvarchar(max);
    SELECT @var5 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteSettings]') AND [c].[name] = N'ProjectSectionSettingId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [SiteSettings] DROP CONSTRAINT ' + @var5 + ';');
    ALTER TABLE [SiteSettings] ALTER COLUMN [ProjectSectionSettingId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    DECLARE @var6 nvarchar(max);
    SELECT @var6 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteSettings]') AND [c].[name] = N'GeneralSiteInformationId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [SiteSettings] DROP CONSTRAINT ' + @var6 + ';');
    ALTER TABLE [SiteSettings] ALTER COLUMN [GeneralSiteInformationId] uniqueidentifier NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    ALTER TABLE [SiteSettings] ADD CONSTRAINT [FK_SiteSettings_GeneralSiteInformation_GeneralSiteInformationId] FOREIGN KEY ([GeneralSiteInformationId]) REFERENCES [GeneralSiteInformation] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    ALTER TABLE [SiteSettings] ADD CONSTRAINT [FK_SiteSettings_ProjectSectionSetting_ProjectSectionSettingId] FOREIGN KEY ([ProjectSectionSettingId]) REFERENCES [ProjectSectionSetting] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    ALTER TABLE [SiteSettings] ADD CONSTRAINT [FK_SiteSettings_SpecializedServicesSection_SpecializedServicesSectionId] FOREIGN KEY ([SpecializedServicesSectionId]) REFERENCES [SpecializedServicesSection] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260728195929_AddSiteSettings'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260728195929_AddSiteSettings', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260729185140_addPageManagement'
)
BEGIN
    CREATE TABLE [PageManagements] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Url] nvarchar(max) NOT NULL,
        [IsUnderConstruction] bit NOT NULL,
        [CustomMessage] nvarchar(max) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_PageManagements] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260729185140_addPageManagement'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260729185140_addPageManagement', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260730045800_AddSeoIndexing'
)
BEGIN
    ALTER TABLE [PageManagements] ADD [MetaRobots] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260730045800_AddSeoIndexing'
)
BEGIN
    ALTER TABLE [PageManagements] ADD [SeoIndexing] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260730045800_AddSeoIndexing'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260730045800_AddSeoIndexing', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806201556_AlterContactUs'
)
BEGIN
    ALTER TABLE [ContactUs] ADD [IsAnswered] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806201556_AlterContactUs'
)
BEGIN
    ALTER TABLE [ContactUs] ADD [IsRead] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806201556_AlterContactUs'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260806201556_AlterContactUs', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807173152_addProjectRequestv1'
)
BEGIN
    CREATE TABLE [ProjectRequestV1] (
        [Id] uniqueidentifier NOT NULL,
        [FillName] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Type] int NOT NULL,
        [Price] bigint NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [IsWasSeen] bit NOT NULL,
        [IsWorked] bit NOT NULL,
        [ISDone] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ProjectRequestV1] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807173152_addProjectRequestv1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260807173152_addProjectRequestv1', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807201756_AlterProjectRequestv1'
)
BEGIN
    EXEC sp_rename N'[ProjectRequestV1].[ISDone]', N'IsDone', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807201756_AlterProjectRequestv1'
)
BEGIN
    EXEC sp_rename N'[ProjectRequestV1].[FillName]', N'FullName', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807201756_AlterProjectRequestv1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260807201756_AlterProjectRequestv1', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260809175508_AddLogs'
)
BEGIN
    CREATE TABLE [Logs] (
        [Id] int NOT NULL IDENTITY,
        [CreationDate] datetime2 NOT NULL,
        [Exception] nvarchar(max) NOT NULL,
        [Message] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Logs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260809175508_AddLogs'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260809175508_AddLogs', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260826215421_SkillsChanges'
)
BEGIN
    ALTER TABLE [FAQs] DROP CONSTRAINT [PK_FAQs];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260826215421_SkillsChanges'
)
BEGIN
    IF SCHEMA_ID(N'FAQ') IS NULL EXEC(N'CREATE SCHEMA [FAQ];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260826215421_SkillsChanges'
)
BEGIN
    EXEC sp_rename N'[FAQs]', N'faq', 'OBJECT';
    ALTER SCHEMA [FAQ] TRANSFER [faq];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260826215421_SkillsChanges'
)
BEGIN
    ALTER TABLE [FAQ].[faq] ADD CONSTRAINT [PK_faq] PRIMARY KEY ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260826215421_SkillsChanges'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260826215421_SkillsChanges', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260826222924_SkillsChangesن'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260826222924_SkillsChangesن', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260917003600_Subscription'
)
BEGIN
    CREATE TABLE [Subscriptions] (
        [Id] uniqueidentifier NOT NULL,
        [SubscriptionFor] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Type] int NOT NULL,
        [Token] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260917003600_Subscription'
)
BEGIN
    CREATE TABLE [SubscriptionUsers] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [SubscriptionId] uniqueidentifier NOT NULL,
        [UsedToken] int NOT NULL,
        [RemainingToken] int NOT NULL,
        [SubscriptionFor] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Type] int NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_SubscriptionUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260917003600_Subscription'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260917003600_Subscription', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260917020747_Subscriptionس'
)
BEGIN
    ALTER TABLE [Subscriptions] ADD [Price] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260917020747_Subscriptionس'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260917020747_Subscriptionس', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260921213602_initialTutorialh'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260921213602_initialTutorialh', N'10.0.9');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260925054509_InitialCreate'
)
BEGIN
    IF SCHEMA_ID(N'tutorial') IS NULL EXEC(N'CREATE SCHEMA [tutorial];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260925054509_InitialCreate'
)
BEGIN
    CREATE TABLE [tutorial].[Tutorials] (
        [Id] uniqueidentifier NOT NULL,
        [TutorialApiId] int NOT NULL,
        [Title] nvarchar(500) NOT NULL,
        [Topic] nvarchar(200) NOT NULL,
        [Subject] nvarchar(100) NOT NULL,
        [Level] nvarchar(20) NOT NULL,
        [Category] nvarchar(30) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [SectionCount] int NOT NULL,
        [CodeBlockCount] int NOT NULL,
        [IsApproved] bit NOT NULL,
        [IsDelete] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Tutorials] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260925054509_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Tutorials_TutorialApiId] ON [tutorial].[Tutorials] ([TutorialApiId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260925054509_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260925054509_InitialCreate', N'10.0.9');
END;

COMMIT;
GO

