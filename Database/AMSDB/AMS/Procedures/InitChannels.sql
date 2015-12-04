CREATE PROCEDURE [AMS].[InitChannels]
AS
BEGIN
	IF (NOT EXISTS(SELECT TOP 1 * FROM AMS.Channels))
	BEGIN
		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description])
		VALUES(CAST(NEWID() AS NVARCHAR(36)), 'nb:chid:UUID:67e9c167-8421-41d0-8bcc-06b753791fe0', 'zhshenstudy', 'TheFirst', '')

		INSERT INTO AMS.Channels(ID, AMSID, AMSAccountName, Name, [Description])
		VALUES(CAST(NEWID() AS NVARCHAR(36)), 'nb:chid:UUID:bd4e9d49-6706-491d-bbc4-de3df0476bff', 'zhshenstudy', 'TheSecondChannel', '')
	END
END
