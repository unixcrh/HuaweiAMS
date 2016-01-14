CREATE PROCEDURE [AMS].[InitChannels]
AS
BEGIN
	--IF (NOT EXISTS(SELECT TOP 1 * FROM AMS.Channels))
	BEGIN
		TRUNCATE TABLE AMS.Channels

		--这是zhshenstudy的频道
		/*
		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl)
		VALUES('2228C26F-54E6-40E9-B017-1441C81DE258', 'nb:chid:UUID:67e9c167-8421-41d0-8bcc-06b753791fe0', 'zhshenstudy', 'TheFirst', '', 
		'http://thefirst-zhshenstudy.channel.mediaservices.windows.net/preview.isml/manifest',
		'rtmp://thefirst-zhshenstudy.channel.mediaservices.windows.net:1935/live/c386e0f077614e32862af4234cc19118',
		'rtmp://thefirst-zhshenstudy.channel.mediaservices.windows.net:1936/live/c386e0f077614e32862af4234cc19118')

		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl)
		VALUES('39C41445-C0DF-477C-B45C-69F518F9BE0D', 'nb:chid:UUID:bd4e9d49-6706-491d-bbc4-de3df0476bff', 'zhshenstudy', 'TheSecondChannel', '',
		'http://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net/preview.isml/manifest',
		'rtmp://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net:1935/live/31b5cd88092a42caad51c877b0d7d145',
		'rtmp://thesecondchannel-zhshenstudy.channel.mediaservices.windows.net:1936/live/31b5cd88092a42caad51c877b0d7d145')
		*/

		--这是amshuaweirel的频道
		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl, CDNPrefixMode, CDNPrefix, AlternateCDNEndpoint)
		VALUES('BDBC67E0-BF27-4E0B-BDC9-237EDF26B96D', 'nb:chid:UUID:2932f550-bd1f-4293-b1a5-54694e178450', 'amshuaweirel', 'sales-training', '销售大讲堂', 
		'http://sales-training-amshuaweirel.channel.mediaservices.windows.net/preview.isml/manifest',
		'rtmp://sales-training-amshuaweirel.channel.mediaservices.windows.net:1935/live/538540210e614d06adc37429e41481fb',
		'rtmp://sales-training-amshuaweirel.channel.mediaservices.windows.net:1936/live/538540210e614d06adc37429e41481fb',
		'Prefix', 'cdn-', 'vod2.cqkfz.com')

		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl, CDNPrefixMode, CDNPrefix, AlternateCDNEndpoint)
		VALUES('DBE36981-ED6B-4B69-86BD-9173BEF61879', 'nb:chid:UUID:0f0cbd12-fb35-464b-aa71-be20729f3618', 'amshuaweirel', 'tech-training', '技术大讲堂',
		'http://tech-training-amshuaweirel.channel.mediaservices.windows.net/preview.isml/manifest',
		'rtmp://tech-training-amshuaweirel.channel.mediaservices.windows.net:1935/live/30b10c5ee9b5454d87dc41e2f36f52cc',
		'rtmp://tech-training-amshuaweirel.channel.mediaservices.windows.net:1936/live/30b10c5ee9b5454d87dc41e2f36f52cc',
		'Prefix', 'cdn-', 'vod2.cqkfz.com')

		--这是amshuaweichn的频道
		--INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl, AlternateCDNEndpoint)
		--VALUES('33686296-3F55-4B76-8877-2F12ADA77117', 'nb:chid:UUID:07050564-cf6a-4433-83b6-6494dd1a84b8', 'amshuaweichn', 'sales-channel-chn', '销售大讲堂(中国)', 
		--'http://sales-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn/preview.isml/manifest',
		--'rtmp://sales-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn:1935/live/87c9367b20d74e02820ead101a100891',
		--'rtmp://sales-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn:1936/live/87c9367b20d74e02820ead101a100891',
		--'az843435.vo.msecnd.net')

		--INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description], PreviewUrl, PrimaryInputUrl, SecondaryInputUrl, AlternateCDNEndpoint)
		--VALUES('8B21E4BB-86CB-4843-9122-AD5599EBAE5C', 'nb:chid:UUID:3e8b29ae-a0be-44b4-8ec9-cf307f8d0c79', 'amshuaweichn', 'tech-channel-chn', '技术大讲堂(中国)',
		--'http://tech-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn/preview.isml/manifest',
		--'rtmp://tech-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn:1935/live/9ad677e9f55d4ff0b2e9f77e022d7364',
		--'rtmp://tech-channel-chn-amshuaweichn.channel.mediaservices.chinacloudapi.cn:1936/live/9ad677e9f55d4ff0b2e9f77e022d7364',
		--'az843435.vo.msecnd.net')
	END
END
