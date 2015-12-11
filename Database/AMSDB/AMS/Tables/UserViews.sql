CREATE TABLE [AMS].[UserViews]
(
	[EventID] NVARCHAR(36) NOT NULL , 
    [UserID] NVARCHAR(36) NOT NULL, 
	[UserName] NVARCHAR(64) NOT NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [LastClientAccessIP] NVARCHAR(255) NULL, 
    [LastAccessTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    PRIMARY KEY ([EventID], [UserID])
)

GO

CREATE INDEX [IX_UserViews_UserID] ON [AMS].[UserViews] ([UserID])

GO
