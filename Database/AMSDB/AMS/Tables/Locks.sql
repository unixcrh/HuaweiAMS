CREATE TABLE [AMS].[Locks]
(
	[LockID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [ResourceID] NVARCHAR(36) NULL, 
    [LockPersonID] NVARCHAR(36) NULL, 
    [LockPersonName] NVARCHAR(255) NULL, 
    [LockTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [EffectiveTime] INT NULL, 
    [LockType] NVARCHAR(32) NULL, 
    [Description] NVARCHAR(255) NULL
)

GO
