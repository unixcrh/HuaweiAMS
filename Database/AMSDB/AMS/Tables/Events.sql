﻿CREATE TABLE [AMS].[Events]
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
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE()
)

GO

CREATE INDEX [IX_Events_ChannelID] ON [AMS].[Events] ([ChannelID])
