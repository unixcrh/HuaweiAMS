EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'队列表',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'队列ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'队列的类别',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = N'COLUMN',
    @level2name = N'Category'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资源ID',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = N'COLUMN',
    @level2name = N'ResourceID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资源名称',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = N'COLUMN',
    @level2name = N'ResourceName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'队列项类型',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = N'COLUMN',
    @level2name = N'ItemType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'AMS',
    @level1type = N'TABLE',
    @level1name = N'Queue',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'