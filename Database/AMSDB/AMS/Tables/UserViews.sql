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
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户观看事件记录',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserViews',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserViews',
    @level2type = N'COLUMN',
    @level2name = N'EventID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserViews',
    @level2type = N'COLUMN',
    @level2name = N'UserID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserViews',
    @level2type = N'COLUMN',
    @level2name = N'UserName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserViews',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'