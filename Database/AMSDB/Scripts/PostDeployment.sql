/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DECLARE @initEnv NVARCHAR(64)

SET @initEnv = N'$(InitEnv)'

IF @initEnv = N'UnitTest'
BEGIN
	EXECUTE [AMS].[InitTestChannels]
	EXECUTE [AMS].[InitAdmins]
END
ELSE
BEGIN
	EXECUTE [AMS].[InitChannels]
END
--EXECUTE [AMS].[InitEvents]