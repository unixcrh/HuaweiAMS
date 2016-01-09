﻿CREATE PROCEDURE [AMS].[ClearAllData]
AS
BEGIN
	TRUNCATE TABLE AMS.Channels
	TRUNCATE TABLE [AMS].[Events]
	TRUNCATE TABLE AMS.EventsChannels
	TRUNCATE TABLE AMS.UserOperationLogs
	TRUNCATE TABLE AMS.Locks
	TRUNCATE TABLE AMS.[Queue]
	TRUNCATE TABLE AMS.UserOperationLogs
	TRUNCATE TABLE AMS.UserViews
	TRUNCATE TABLE AMS.Admins
END
