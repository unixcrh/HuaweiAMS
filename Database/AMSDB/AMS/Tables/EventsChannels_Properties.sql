EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件和频道对应表',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'EventID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'频道ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'ChannelID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事件的状态',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'State'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'AMS中Program的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'AMSProgramID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无CDN的视频回放地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'DefaultPlaybackUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'带CDN的视频回放地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'CDNPlaybackUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否是默认频道',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'IsDefault'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'节目的状态',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'EventsChannels',
    @level2type = N'COLUMN',
    @level2name = N'State'