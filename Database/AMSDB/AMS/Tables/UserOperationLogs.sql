CREATE TABLE [AMS].[UserOperationLogs]
(
	[ID] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [ResourceID] NVARCHAR(36) NULL , 
    [Subject] NVARCHAR(512) NULL , 
    [ProcessID] NVARCHAR(36) NULL , 
    [ActivityID] NVARCHAR(36) NULL, 
    [ActivityName] NVARCHAR(64) NULL, 
    [ApplicationName] NVARCHAR(64) NULL, 
    [ProgramName] NVARCHAR(64) NULL, 
    [CorrelationID] NVARCHAR(36) NULL, 
    [HttpContextInfo] NVARCHAR(MAX) NULL, 
    [OperationName] NVARCHAR(32) NULL, 
    [OperatorID] NVARCHAR(MAX) NULL, 
    [OperatorName] NVARCHAR(64) NULL, 
    [RealOperatorID] NVARCHAR(36) NULL, 
    [RealOperatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [Url] NVARCHAR(MAX) NULL, 
    [ClientAddress] NVARCHAR(255) NULL, 
    [ServerAddress] NVARCHAR(255) NULL
)

GO

CREATE INDEX [IX_UserOperationLogs_ResourceID] ON [AMS].[UserOperationLogs] ([ResourceID])

GO

CREATE INDEX [IX_UserOperationLogs_ProcessID] ON [AMS].[UserOperationLogs] ([ProcessID])

GO

CREATE INDEX [IX_UserOperationLogs_ActivityID] ON [AMS].[UserOperationLogs] ([ActivityID])

GO
