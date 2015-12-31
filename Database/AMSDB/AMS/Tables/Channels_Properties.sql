EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'AMS中对应频道的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'AMSID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道的名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道的描述',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道最后更新时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = 'AMSLastModified'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'记录创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'媒体服务账户名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'AMSAccountName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道的状态：0-Stopped, 1-Starting, 2-Running, 3-Stopping, 4-Deleting',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'视频采集主地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'PrimaryInputUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'视频采集辅助地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'SecondaryInputUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'视频预览地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'PreviewUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'CDN前缀的模式，分为None, Prefix, Host',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'CDNPrefixMode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'CDN的前缀',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Channels',
    @level2type = N'COLUMN',
    @level2name = N'CDNPrefix'