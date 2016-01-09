CREATE PROCEDURE [AMS].[InitEventsChannels]	
AS
BEGIN
	INSERT INTO AMS.EventsChannels(EventID, ChannelID, [State], IsDefault)
	SELECT [ID], ChannelID, [State], 1
	FROM AMS.Events
END
