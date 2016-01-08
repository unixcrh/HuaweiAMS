CREATE TABLE [AMS].[EventsChannels]
(
	[EventID] NVARCHAR(36) NOT NULL , 
    [ChannelID] NVARCHAR(36) NOT NULL, 
    [AMSProgramID] NVARCHAR(50) NULL,
	[State] NVARCHAR(32) NULL, 
	[DefaultPlaybackUrl] NVARCHAR(MAX) NULL, 
    [CDNPlaybackUrl] NVARCHAR(MAX) NULL, 
    [IsDefault] INT NULL DEFAULT 0, 
    PRIMARY KEY ([EventID], [ChannelID])
)

GO




CREATE INDEX [IX_EventsChannels_ChannelID] ON [AMS].[EventsChannels] ([ChannelID])

GO