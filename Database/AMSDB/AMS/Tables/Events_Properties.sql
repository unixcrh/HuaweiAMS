EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'ChannelID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件描述',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件的状态',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'StartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'时间结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'EndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人的名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'AMS中Program的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'AMSProgramID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无CDN的视频回放地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'DefaultPlaybackUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'带CDN的视频回放地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Events',
    @level2type = N'COLUMN',
    @level2name = N'CDNPlaybackUrl'