EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'相关资源的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ResourceID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ProcessID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'日志的主题',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程活动的名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ActivityName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程活动的ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ActivityID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'应用的名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ApplicationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'应用模块的名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ProgramName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Http请求上下文信息',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'HttpContextInfo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作的名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'OperationName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作人ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'OperatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作人名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'OperatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'真实操作人ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'RealOperatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'真实操作人名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'RealOperatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'记录创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户请求的Url',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'Url'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户请求的客户端地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ClientAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户请求的服务端地址',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = N'COLUMN',
    @level2name = N'ServerAddress'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'用户操作日志',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'UserOperationLogs',
    @level2type = NULL,
    @level2name = NULL