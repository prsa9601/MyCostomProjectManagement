-- ساخت جدول History اگر نیست
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

-- ثبت ۲۱ Migration قدیمی (فقط اونایی که از قبل جدولشون هست)
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
SELECT v.MigrationId, v.ProductVersion
FROM (VALUES
    ('20260718165649_Start',                                  '8.0.0'),
    ('20260718170816_SetNullableFieldFromUserTable',          '8.0.0'),
    ('20260719212249_RoleChange',                             '8.0.0'),
    ('20260721154102_AlterTableRoleAddsDefaultColumn',        '8.0.0'),
    ('20260723153229_AlterSkillTable',                        '8.0.0'),
    ('20260723235104_AlterPortfolioTable',                    '8.0.0'),
    ('20260724184807_SetNullableFieldsFromPortfolioFileTable','8.0.0'),
    ('20260726160151_CreateFAQTable',                         '8.0.0'),
    ('20260728194553_AddSiteSetting',                         '8.0.0'),
    ('20260728195929_AddSiteSettings',                        '8.0.0'),
    ('20260729185140_addPageManagement',                      '8.0.0'),
    ('20260730045800_AddSeoIndexing',                         '8.0.0'),
    ('20260806201556_AlterContactUs',                         '8.0.0'),
    ('20260807173152_addProjectRequestv1',                    '8.0.0'),
    ('20260807201756_AlterProjectRequestv1',                  '8.0.0'),
    ('20260809175508_AddLogs',                                '8.0.0'),
    ('20260826215421_SkillsChanges',                          '8.0.0'),
    ('20260826222924_SkillsChanges_Update',                   '8.0.0'),
    ('20260917003600_Subscription',                           '8.0.0'),
    ('20260917020747_Subscription_Update',                    '8.0.0'),
    ('20260921213602_initialTutorials',                       '8.0.0')
) AS v(MigrationId, ProductVersion)
WHERE NOT EXISTS (
    SELECT 1 FROM [__EFMigrationsHistory] h WHERE h.MigrationId = v.MigrationId
);
GO