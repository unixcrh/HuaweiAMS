CREATE PROCEDURE [AMS].[InitEventsChannels]	
AS
BEGIN
	INSERT INTO AMS.EventsChannels(EventID, ChannelID, [State], IsDefault, DefaultPlaybackUrl, CDNPlaybackUrl)
	SELECT [ID], ChannelID, [State], 1, DefaultPlaybackUrl, CDNPlaybackUrl
	FROM AMS.Events
END
