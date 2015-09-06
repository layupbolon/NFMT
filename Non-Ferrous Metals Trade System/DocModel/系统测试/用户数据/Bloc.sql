USE [NFMT_User]
GO

SET IDENTITY_INSERT [dbo].[Bloc] ON
INSERT [dbo].[Bloc] ([BlocId], [BlocName], [BlocFullName], [BlocEname], [IsSelf], [BlocStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (1, N'迈科集团', N'西安迈科金属集团', N'maike group', 1, 50, 1, CAST(0x0000A34E00F69E3A AS DateTime), 102, CAST(0x0000A37000F21860 AS DateTime))
INSERT [dbo].[Bloc] ([BlocId], [BlocName], [BlocFullName], [BlocEname], [IsSelf], [BlocStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (2, N'青岛欧美', N' 青岛欧美进出口有限公司 ', N'Qingdao oumei import&export ltd', 0, 50, 1, CAST(0x0000A36100A96EF2 AS DateTime), 1, CAST(0x0000A37001112F42 AS DateTime))
INSERT [dbo].[Bloc] ([BlocId], [BlocName], [BlocFullName], [BlocEname], [IsSelf], [BlocStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (3, N'金川集团', N'金川集团股份有限公司', N'Jinchuan Group co.,ltd', 0, 50, 1, CAST(0x0000A36101092ABA AS DateTime), 1, CAST(0x0000A3700111567A AS DateTime))
INSERT [dbo].[Bloc] ([BlocId], [BlocName], [BlocFullName], [BlocEname], [IsSelf], [BlocStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (14, N'加能可', N'加能可', N'加能可', 0, 25, 1, CAST(0x0000A3BF00A05072 AS DateTime), 1, CAST(0x0000A3BF00A4F862 AS DateTime))
SET IDENTITY_INSERT [dbo].[Bloc] OFF