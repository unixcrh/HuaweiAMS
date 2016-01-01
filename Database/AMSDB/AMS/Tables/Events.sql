CREATE TABLE [AMS].[Events]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY,
	[ChannelID] NVARCHAR(36) NOT NULL,
    [Name] NVARCHAR(128) NULL, 
    [Description] NVARCHAR(MAX) NULL,
	[Speakers] NVARCHAR(255) NULL,
	[State] NVARCHAR(32) NULL,
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [AMSProgramID] NVARCHAR(50) NULL, 
    [DefaultPlaybackUrl] NVARCHAR(MAX) NULL, 
    [CDNPlaybackUrl] NVARCHAR(MAX) NULL, 
    [PosterUrl] NVARCHAR(MAX) NULL, 
    [LogoUrl] NVARCHAR(MAX) NULL, 
    [Views] BIGINT NULL DEFAULT 0, 
    [Rating] DECIMAL(18, 2) NULL DEFAULT 0
)

GO

CREATE INDEX [IX_Events_ChannelID] ON [AMS].[Events] ([ChannelID])

GO


CREATE INDEX [IX_Events_StartTime] ON [AMS].[Events] ([StartTime])

GO

CREATE INDEX [IX_Events_EndTime] ON [AMS].[Events] ([EndTime])
