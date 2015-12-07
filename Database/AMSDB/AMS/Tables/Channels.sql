﻿CREATE TABLE [AMS].[Channels]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
	[AMSID] NVARCHAR(64) NULL,
    [Name] NVARCHAR(64) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
	[State] NVARCHAR(32) NULL,
	[AMSAccountName] NVARCHAR(64) NULL,
    [AMSLastModified] DATETIME NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [PrimaryInputUrl] NVARCHAR(MAX) NULL, 
    [SecondaryInputUrl] NVARCHAR(MAX) NULL, 
    [PreviewUrl] NVARCHAR(MAX) NULL 
)


GO
