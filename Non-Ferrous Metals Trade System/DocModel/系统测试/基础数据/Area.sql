USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[Area] ON
INSERT [dbo].[Area] ([AreaId], [AreaName], [AreaFullName], [AreaShort], [AreaCode], [AreaZip], [AreaLevel], [ParentID], [AreaStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (74, N'上海', N'上海', N'上海', N'021', N'200000', 0, 0, 50, 1, CAST(0x0000A3BF00B39022 AS DateTime), 1, CAST(0x0000A3BF00B4D7C6 AS DateTime))
INSERT [dbo].[Area] ([AreaId], [AreaName], [AreaFullName], [AreaShort], [AreaCode], [AreaZip], [AreaLevel], [ParentID], [AreaStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (75, N'西安', N'西安', N'西安', N'029', N'0578', 0, 0, 50, 1, CAST(0x0000A3BF00B3C7D2 AS DateTime), 1, CAST(0x0000A3BF00B4E065 AS DateTime))
INSERT [dbo].[Area] ([AreaId], [AreaName], [AreaFullName], [AreaShort], [AreaCode], [AreaZip], [AreaLevel], [ParentID], [AreaStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (76, N'深圳', N'深圳', N'深圳', N'0751', N'2656565', 0, 0, 50, 1, CAST(0x0000A3BF00B3E936 AS DateTime), 1, CAST(0x0000A3BF00B4E811 AS DateTime))
SET IDENTITY_INSERT [dbo].[Area] OFF