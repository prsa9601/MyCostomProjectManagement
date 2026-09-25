
DECLARE @ProductVersion nvarchar(32) = N'10.0.9';

INSERT INTO [ParsaKarimiDevU].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES 
    (N'20260718165649_Start', @ProductVersion),
    (N'20260718170816_SetNullableFieldFromUserTable', @ProductVersion),
    (N'20260719212249_RoleChange', @ProductVersion),
    (N'20260721154102_AlterTableRoleAddIsDefaultColumn', @ProductVersion),
    (N'20260723155329_AlterSkillTable', @ProductVersion),
    (N'20260723235104_AlterPortfolioTable', @ProductVersion),
    (N'20260724184807_SetNullableFieldsFromPortfolioFileTable', @ProductVersion),
    (N'20260726160151_CreateFAQTable', @ProductVersion),
    (N'20260728194553_AddSiteSetting', @ProductVersion),
    (N'20260728195929_AddSiteSettings', @ProductVersion),
    (N'20260729185140_addPageManagement', @ProductVersion),
    (N'20260730045800_AddSeoIndexing', @ProductVersion),
    (N'20260806201556_AlterContactUs', @ProductVersion),
    (N'20260807171352_addProjectRequestv1', @ProductVersion),
    (N'20260807201756_AlterProjectRequestv1', @ProductVersion),
    (N'20260809175508_AddLogs', @ProductVersion),
    (N'20260826221542_SkillsChanges', @ProductVersion),
    (N'20260826222924_SkillsChanges_2', @ProductVersion),
    (N'20260917003600_Subscription', @ProductVersion),
    (N'20260917020747_Subscription_2', @ProductVersion),
    (N'20260921213602_initialTutorial', @ProductVersion),
    (N'20260925054509_InitialCreate', @ProductVersion);

    USE [parsakarimidev_ir_];

-- آیا جدول history هست؟
SELECT * FROM sys.tables WHERE name LIKE '%Migration%';

-- چی توشه؟
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL
    SELECT MigrationId, ProductVersion 
    FROM [__EFMigrationsHistory] 
    ORDER BY MigrationId;

-- آیا جدول AboutStats هست؟
SELECT * FROM sys.tables WHERE name = 'AboutStats';

USE [parsakarimidev_ir_];

-- اسم schema ها رو ببین
SELECT schema_id, name FROM sys.schemas ORDER BY schema_id;

-- default schema کاربر اپلیکیشن چیه؟
SELECT name, default_schema_name 
FROM sys.database_principals 
WHERE name = 'ParsaKarimiDevU';

BACKUP DATABASE [parsakarimidev_ir_] 
TO DISK = N'C:\Backup\parsakarimidev_ir__before_reset.bak'
WITH FORMAT, INIT, NAME = N'Before Reset';

USE [master];
ALTER DATABASE [parsakarimidev_ir_] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [parsakarimidev_ir_];