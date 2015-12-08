CREATE TABLE [AMS].[Events]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY,
	[ChannelID] NVARCHAR(36) NOT NULL,
    [Name] NVARCHAR(128) NULL, 
    [Description] NVARCHAR(MAX) NULL,
	[State] NVARCHAR(32) NULL,
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [AMSProgramID] NVARCHAR(50) NULL, 
    [DefaultPlaybackUrl] NVARCHAR(MAX) NULL, 
    [CDNPlaybackUrl] NVARCHAR(MAX) NULL
)

GO

CREATE INDEX [IX_Events_ChannelID] ON [AMS].[Events] ([ChannelID])

GO


CREATE INDEX [IX_Events_StartTime] ON [AMS].[Events] ([StartTime])
