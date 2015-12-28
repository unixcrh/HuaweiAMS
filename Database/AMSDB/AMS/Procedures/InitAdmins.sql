CREATE PROCEDURE [AMS].[InitAdmins]
AS
BEGIN
	--IF (NOT EXISTS(SELECT TOP 1 * FROM AMS.Channels))
	BEGIN
		TRUNCATE TABLE AMS.Admins

		INSERT INTO AMS.Admins(UserID, LogonName, [Password], Name)
		VALUES('D6B16EA9-2678-4347-8C77-D149C41896F6', 'sam.lixiaoming@huawei.com', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD', '李晓明');

		INSERT INTO AMS.Admins(UserID, LogonName, [Password], Name)
		VALUES('98C15FF7-128B-4807-981A-98053E2DE747', 'zhshen@microsoft.com', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD', '沈峥');

		INSERT INTO AMS.Admins(UserID, LogonName, [Password], Name)
		VALUES('D675B6AF-4FF2-43F0-98CD-3629A045333A', 'ronchen@microsoft.com', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD', '陈荣华');

		INSERT INTO AMS.Admins(UserID, LogonName, [Password], Name)
		VALUES('D8A5B95B-10AF-4E6F-AAF7-9C14ACEF4B84', 'susan.chensumin@huawei.com', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD', 'Chen Sumin');

		INSERT INTO AMS.Admins(UserID, LogonName, [Password], Name)
		VALUES('630F456B-2932-4669-9553-B59E855DD42E', 'david.yangshaojun@huawei.com', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD', 'Yang Shaojun');

		INSERT INTO AMS.Admins(UserID, LogonName, [Password], Name)
		VALUES('C5C7BF17-22F9-46E6-8485-35C03FAD429B', 'liuxianping@huawei.com', 'B0-81-DB-E8-5E-1E-C3-FF-C3-D4-E7-D0-22-74-00-CD', 'Li Xianping');
	END
END
