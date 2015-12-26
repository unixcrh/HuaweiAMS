CREATE TABLE [AMS].[Admins]
(
	[UserID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [LogonName] NVARCHAR(64) NULL, 
    [Name] NVARCHAR(64) NULL, 
    [Password] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME2 NULL DEFAULT GETUTCDATE()
)

GO

CREATE UNIQUE INDEX [IX_Admins_LogonName] ON [AMS].[Admins] ([LogonName])
