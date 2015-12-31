CREATE PROCEDURE [AMS].[InitTestChannels]
AS
BEGIN
	TRUNCATE TABLE AMS.Channels

	--这是zhshenstudy的频道
	INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl, CDNPrefixMode, CDNPrefix)
	VALUES('2228C26F-54E6-40E9-B017-1441C81DE258', 'nb:chid:UUID:67e9c167-8421-41d0-8bcc-06b753791fe0', 'zhshenstudy', 'TheFirst', '', 
	'http://thefirst-zhshenstudy.channel.mediaservices.windows.net/preview.isml/manifest',
	'rtmp://thefirst-zhshenstudy.channel.mediaservices.windows.net:1935/live/c386e0f077614e32862af4234cc19118',
	'rtmp://thefirst-zhshenstudy.channel.mediaservices.windows.net:1936/live/c386e0f077614e32862af4234cc19118',
	'Prefix', 'cdn-')

	INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl, CDNPrefixMode, CDNPrefix)
	VALUES('39C41445-C0DF-477C-B45C-69F518F9BE0D', 'nb:chid:UUID:bd4e9d49-6706-491d-bbc4-de3df0476bff', 'zhshenstudy', 'TheSecondChannel', '',
	'http://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net/preview.isml/manifest',
	'rtmp://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net:1935/live/31b5cd88092a42caad51c877b0d7d145',
	'rtmp://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net:1936/live/31b5cd88092a42caad51c877b0d7d145',
	'Prefix', 'cdn-')
	
	--这是amshuaweichn的频道
	INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl)
	VALUES('D15214A7-DA20-4FF9-A696-5EAC938F0069', 'nb:chid:UUID:497a61f9-abf0-4d7f-bca5-5673785e9fad', 'amshuaweichn', 'test-channel-chn', '测试频道', 
	'http://test-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn/preview.isml/manifest',
	'rtmp://test-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn:1935/live/aa4f1a2cc9ff4799b80a1634d0a0ec96',
	'rtmp://test-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn:1936/live/aa4f1a2cc9ff4799b80a1634d0a0ec96')
END
