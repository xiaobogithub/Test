INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('BlobPath'
           ,'http://changetechstoragetest.blob.core.windows.net/')

INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('EmailFromAddress'
           ,'changetechservice@gmail.com')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('EmailFromName'
           ,'ChangeTech Service')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('WebServerPath'
           ,'http://changetech.cloudapp.net/')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('LogFileDirectory'
           ,'')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('LogFileName'
           ,'')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('ReminderTemplateGUID'
           ,'64c6ea7b-a723-4b96-b04e-b9ff0196129c')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('WelcomeTemplateGUID'
           ,'08608919-dfae-4e18-9cd2-9964ac5b4702')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('ResendPasswordTempalteGUID'
           ,'755fb213-bd71-4e64-9609-586cef781c02')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('InviteFriendTemplateGUID'
           ,'287837bc-b707-4652-a55f-f110a83cda61')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('MailServer'
           ,'smtp.gmail.com')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('Port'
           ,'25')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('EnableSsl'
           ,'true')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('UserToAcessMailServer'
           ,'changetechservice@gmail.com')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('PasswordToAccessMailServer'
           ,'ChangeTech2010')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('DefaultForgetPasswordTemplate'
           ,'Hi {0}, Here is your password {1}. Best Regards, ChangeTech')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('AzureTableURL'
           ,'http://changetechstoragetest.table.core.windows.net')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('AzureStorageAccountKey'
           ,'N5LVrFmIBcu19kVgu4e8Z5L48x+8x44z+he/QBQ9+Wtag46Gsn5Ok58idmXAZOe7C372opmNLPcxbmqoz9lgTw==')
INSERT INTO [changetech].[dbo].[SystemSetting]
           ([Name]
           ,[Value])
     VALUES
           ('AzureStorageAccountName'
           ,'changetechstorage')
GO

update [SystemSetting]
Set [Value] = 'http://changetechstorage.blob.core.windows.net/'
where [Name] = 'BlobPath'