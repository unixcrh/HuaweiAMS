CREATE PROCEDURE [AMS].[InitChannels]
AS
BEGIN
	--IF (NOT EXISTS(SELECT TOP 1 * FROM AMS.Channels))
	BEGIN
		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl)
		VALUES(CAST(NEWID() AS NVARCHAR(36)), 'nb:chid:UUID:67e9c167-8421-41d0-8bcc-06b753791fe0', 'zhshenstudy', 'TheFirst', '', 
		'http://thefirst-zhshenstudy.channel.mediaservices.windows.net/preview.isml/manifest',
		'rtmp://thefirst-zhshenstudy.channel.mediaservices.windows.net:1935/live/c386e0f077614e32862af4234cc19118',
		'rtmp://thefirst-zhshenstudy.channel.mediaservices.windows.net:1936/live/c386e0f077614e32862af4234cc19118')

		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl)
		VALUES(CAST(NEWID() AS NVARCHAR(36)), 'nb:chid:UUID:bd4e9d49-6706-491d-bbc4-de3df0476bff', 'zhshenstudy', 'TheSecondChannel', '',
		'http://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net/preview.isml/manifest',
		'rtmp://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net:1935/live/31b5cd88092a42caad51c877b0d7d145',
		'rtmp://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net:1936/live/31b5cd88092a42caad51c877b0d7d145')
	END
END
