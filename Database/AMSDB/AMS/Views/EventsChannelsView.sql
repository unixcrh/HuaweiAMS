CREATE VIEW [AMS].[EventsChannelsView]
	AS
	SELECT E.ID, E.Name, E.Description, E.CreatorID, E.CreateTime, E.CreatorName, E.LogoUrl, E.PosterUrl, E.StartTime, E.EndTime, E.Rating, E.Speakers, E.Views,
		EC.ChannelID, EC.State, EC.CDNPlaybackUrl, EC.DefaultPlaybackUrl, EC.AMSProgramID
	FROM AMS.Events E INNER JOIN AMS.EventsChannels EC ON E.ID = EC.EventID
