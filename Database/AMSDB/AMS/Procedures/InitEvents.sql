﻿CREATE PROCEDURE [AMS].[InitEvents]
AS
BEGIN
--IF (NOT EXISTS(SELECT TOP 1 * FROM AMS.Events))
	BEGIN
		TRUNCATE TABLE AMS.Events

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'10007953-95ad-b740-42f0-80e903564d45', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'Mirosoft services in NASCAR', N'描述微软在NASCAR实施的服务。', N'Scott Culbertson', N'NotStart', CAST(N'2015-12-12 21:16:00.000' AS DateTime), CAST(N'2015-12-12 21:16:00.000' AS DateTime), NULL, NULL, CAST(N'2015-12-11 13:18:14.917' AS DateTime), NULL, N'http://zhshenstudy.streaming.mediaservices.windows.net/58d8180a-469d-469f-a839-1774214d091c/107859_MS_Windows_NASCAR_Services_FINAL_Mixed_Revised_1_2000k.ism/manifest', N'http://cdn-zhshenstudy.streaming.mediaservices.windows.net/58d8180a-469d-469f-a839-1774214d091c/107859_MS_Windows_NASCAR_Services_FINAL_Mixed_Revised_1_2000k.ism/manifest', N'http://huaweiams.blob.core.windows.net/amsimages/107859_MS_Windows_NASCAR_Service_000002.png', N'http://huaweiams.blob.core.windows.net/amsimages/107859_MS_Windows_NASCAR_Service_000002.png', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'1af4e5c1-8f84-bf52-4793-67e6bca357bd', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'销售专家大讲堂—不忘初心，方得始终', N'', N'杨总', N'NotStart', CAST(N'2015-12-30 14:00:00.000' AS DateTime), CAST(N'2015-12-30 16:00:00.000' AS DateTime), N'', N'', CAST(N'2015-12-21 08:54:27.140' AS DateTime), N'', N'', N'', N'https://huaweiams.blob.core.windows.net/amsimages/7cbe50eb-2f3e-87e7-4142-e3eec8e95a0c-poster', N'https://huaweiams.blob.core.windows.net/amsimages/c09fdef7-0e41-b5a3-4816-9419fb8b0ab3-logo', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'1e327524-b015-8164-403a-9166f3c65011', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'销售专家大讲堂—客户经理4.0时代', N'', N'韩总', N'NotStart', CAST(N'2015-12-28 14:00:00.000' AS DateTime), CAST(N'2015-12-28 16:00:00.000' AS DateTime), NULL, NULL, CAST(N'2015-12-21 08:50:21.613' AS DateTime), NULL, NULL, NULL, N'https://huaweiams.blob.core.windows.net/amsimages/6226ec24-3b0a-b47f-4b4d-bb8b1317c8bd-poster', N'https://huaweiams.blob.core.windows.net/amsimages/dcef4a69-afbf-a941-446d-98a3e9de33c1-logo', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'28eb74cb-52f3-a430-422a-e9d3826b51e5', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'Azure Media Services 101 - Get your video online now!', N'Mingfei Yan from the Azure Media Services team shows Scott how easy it is to get video into Azure at scale, then view from any device.

		Areas covered in this video:

		 Azure Media Services portal
		 Creating a Media Service
		 Reviewing media content in Azure
		 Available Azure Media Encoding (Portal and APIs)
		 Publishing Media
		 On-Demand Streaming', N'Mingfei Yan', N'NotStart', CAST(N'2015-12-12 18:40:00.000' AS DateTime), CAST(N'2015-12-12 19:37:40.637' AS DateTime), N'', N'', CAST(N'2015-12-11 16:39:10.210' AS DateTime), N'', N'http://zhshenstudy.streaming.mediaservices.windows.net/be336ab1-fab0-48c3-b6eb-92bb46dfd1f1/20140428AzureFriday20Day1MediaServices1_high.ism/manifest', N'http://cdn-zhshenstudy.streaming.mediaservices.windows.net/be336ab1-fab0-48c3-b6eb-92bb46dfd1f1/20140428AzureFriday20Day1MediaServices1_high.ism/manifest', N'https://huaweiams.blob.core.windows.net/amsimages/44c4dc38-9c80-8abb-4be8-de35ec2ef53e-poster', N'https://huaweiams.blob.core.windows.net/amsimages/44c4dc38-9c80-8abb-4be8-de35ec2ef53e-poster', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'43a361c9-249c-8570-4901-dd9b9df47553', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'XBox One-Extend Your Movie Experience(Mooncake)', N'本片介绍了XBox One在视频体验上的扩展功能', N'Microsoft Corporation', N'NotStart', CAST(N'2015-12-14 12:40:00.000' AS DateTime), CAST(N'2015-12-14 13:40:00.000' AS DateTime), N'', N'', CAST(N'2015-12-11 16:41:55.747' AS DateTime), N'', N'http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/94706d4f-ea13-4eb4-a938-a58c9b9a0897/XBox%20Video.ism/manifest', N'http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/94706d4f-ea13-4eb4-a938-a58c9b9a0897/XBox%20Video.ism/manifest', N'https://huaweiams.blob.core.windows.net/amsimages/d8a2f685-7bd4-a40c-4954-abe3b1c6de57-poster', N'https://huaweiams.blob.core.windows.net/amsimages/d8a2f685-7bd4-a40c-4954-abe3b1c6de57-poster', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'9aed49d5-aa22-887f-408c-51bfc9baf6fc', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'Windows 10 商业演示', N'这里介绍了Windows 10 Dynamic Provisioning, Windows Hello, Continuum for phone以及Enterprise Data Protection。', N'Satya Nadella', N'Completed', CAST(N'2015-12-10 19:30:00.000' AS DateTime), CAST(N'2015-12-11 20:30:00.000' AS DateTime), N'', N'', CAST(N'2015-12-11 11:35:21.333' AS DateTime), N'', N'http://zhshenstudy.streaming.mediaservices.windows.net/58d8180a-469d-469f-a839-1774214d091c/107859_MS_Windows_NASCAR_Services_FINAL_Mixed_Revised_1_2000k.ism/manifest', N'http://cdn-zhshenstudy.streaming.mediaservices.windows.net/85a27234-45e6-45b6-87f6-387a5485f8f7/Windows10CommercialDemo.ism/manifest', N'http://huaweiams.blob.core.windows.net/amsimages/Windows10CommercialDemoPoster.jpg', N'http://huaweiams.blob.core.windows.net/amsimages/Windows10CommercialDemologo.jpg', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'a0fef9e4-e7e4-b378-4fcd-8169366a9ec7', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'销售专家大讲堂—客户关系及交易的科学与艺术', N'', N'陶总', N'NotStart', CAST(N'2015-12-29 14:00:00.000' AS DateTime), CAST(N'2015-12-29 16:00:00.000' AS DateTime), N'', N'', CAST(N'2015-12-21 08:53:22.037' AS DateTime), N'', N'', N'', N'https://huaweiams.blob.core.windows.net/amsimages/fc01b9db-95d0-8cdb-4fef-3acd43426bf0-poster', N'https://huaweiams.blob.core.windows.net/amsimages/6645a37f-de20-a348-47bf-6bc74cfbe80a-logo', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'df970166-2bc4-88d5-480c-f5fdd7ae4996', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'Windows Threshold Security', N'详细描述了Windows所面临的各种安全问题，以及在产品设计层面解决这些问题。', N'Chris Hallum, Dustin Ingalls', N'Completed', CAST(N'2015-12-04 11:11:00.000' AS DateTime), CAST(N'2015-12-05 11:11:00.000' AS DateTime), N'', N'', CAST(N'2015-12-04 03:11:24.730' AS DateTime), N'', N'http://zhshenstudy.streaming.mediaservices.windows.net/3c49e2f2-189d-4477-82ef-6b6aa0433795/WindowsThresholdSecurity.ism/manifest', N'http://cdn-zhshenstudy.streaming.mediaservices.windows.net/3c49e2f2-189d-4477-82ef-6b6aa0433795/WindowsThresholdSecurity.ism/manifest', N'http://huaweiams.blob.core.windows.net/amsimages/WindowsThresholdSecurity_000012.png', N'http://huaweiams.blob.core.windows.net/amsimages/WindowsThresholdSecurity_000012.png', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'ef2a0839-9f0f-87d6-4ac9-758eb3426350', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'Hololens(Mooncake)', N'这是对微软最新研制的VR工具Hololens的应用场景宣传片', N'Microsoft Corporation', N'NotStart', CAST(N'2015-12-13 17:39:00.000' AS DateTime), CAST(N'2015-12-13 18:39:00.000' AS DateTime), N'', N'', CAST(N'2015-12-11 16:40:19.740' AS DateTime), N'', N'http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/f34dba36-4015-4433-93ba-a61c0d3b1812/Hololens.ism/manifest', N'http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/f34dba36-4015-4433-93ba-a61c0d3b1812/Hololens.ism/manifest', N'https://huaweiams.blob.core.windows.net/amsimages/6277316d-05db-a117-4d0e-44ea59827a45-poster', N'https://huaweiams.blob.core.windows.net/amsimages/18f3027c-c169-b1a1-4cc7-db1622d14fd8-logo', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'f93e86e1-eaa3-b4ca-4987-3c6aca83c6dd', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'Windows 新纪元', N'这是新版本Windows 10发布现场会的集锦。本次发布会带来了诸多新技术、新消息，包括Windows 10统一手机、平板、PC、Xbox全平台、开始菜单回归、全新的Spartan浏览器、提出通用应用程序、Office等办公软件的跨平台特性、数字助理Cortana来到桌面、全系虚拟现实技术等。', N'Satya Nadella', N'Completed', CAST(N'2015-12-11 00:24:00.000' AS DateTime), CAST(N'2015-12-11 01:24:00.000' AS DateTime), N'', N'', CAST(N'2015-12-04 16:14:33.003' AS DateTime), N'', N'http://zhshenstudy.streaming.mediaservices.windows.net/c49ddcd1-d15b-4078-aac0-6dc1467ed5d6/A_New_Era_For_Windows.ism/manifest', N'http://cdn-zhshenstudy.streaming.mediaservices.windows.net/c49ddcd1-d15b-4078-aac0-6dc1467ed5d6/A_New_Era_For_Windows.ism/manifest', N'http://huaweiams.blob.core.windows.net/amsimages/A_New_Era_For_Windows_000001.jpg', N'http://huaweiams.blob.core.windows.net/amsimages/A_New_Era_For_Windows_000001.jpg', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'f994b9c1-15e4-993d-44a1-bbe8f61a256b', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'XBox One-Extend Your Movie Experience(Mooncake, Andriod)', N'', N'Microsoft Corporation', N'NotStart', CAST(N'2015-12-15 12:13:00.000' AS DateTime), CAST(N'2015-12-15 13:13:00.000' AS DateTime), NULL, NULL, CAST(N'2015-12-22 04:14:53.013' AS DateTime), NULL, N'http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/ce3ea288-af66-4943-ac60-17e216ceca51/XBox%20Video.ism/manifest', N'http://amshuaweichn.streaming.mediaservices.chinacloudapi.cn/ce3ea288-af66-4943-ac60-17e216ceca51/XBox%20Video.ism/manifest', N'https://huaweiams.blob.core.windows.net/amsimages/63fe25b8-795c-aec8-4fe5-a600e6a170c6-poster', N'https://huaweiams.blob.core.windows.net/amsimages/6d833d04-76dc-af36-42e3-3c34b3986d89-logo', 0, CAST(0.00 AS Decimal(18, 2)))

		INSERT [AMS].[Events] ([ID], [ChannelID], [Name], [Description], [Speakers], [State], [StartTime], [EndTime], [CreatorID], [CreatorName], [CreateTime], [AMSProgramID], [DefaultPlaybackUrl], [CDNPlaybackUrl], [PosterUrl], [LogoUrl], [Views], [Rating]) VALUES (N'236887fe-6bc0-b36b-4310-615801f85610', N'2228C26F-54E6-40E9-B017-1441C81DE258', N'直播测试1', N'', N'沈峥', N'NotStart', CAST(N'2015-12-22 15:36:00.000' AS DateTime), CAST(N'2015-12-22 16:47:00.000' AS DateTime), NULL, NULL, CAST(N'2015-12-22 07:37:04.547' AS DateTime), NULL, N'http://zhshenstudy.streaming.mediaservices.windows.net/e2fd9dc9-c247-48d8-bbec-0ddd1ec06fdc/c7392fac-3ced-47d4-87ac-c4cf40a0d984.ism/manifest', N'http://cdn-zhshenstudy.streaming.mediaservices.windows.net/e2fd9dc9-c247-48d8-bbec-0ddd1ec06fdc/c7392fac-3ced-47d4-87ac-c4cf40a0d984.ism/manifest', N'https://huaweiams.blob.core.windows.net/amsimages/ecbad594-0568-b7d8-4898-519162ef6d3d-poster', N'https://huaweiams.blob.core.windows.net/amsimages/96eea77f-e0a7-8e55-419e-c349a7dd6a51-logo', 0, CAST(0.00 AS Decimal(18, 2)))
	END
END