USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[Asset] ON
INSERT [dbo].[Asset] ([AssetId], [AssetName], [MUId], [MisTake], [AssetStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (29, N'µç½âÍ­', 13, CAST(0.00000000 AS Numeric(18, 8)), 50, 1, CAST(0x0000A3BF00AB6D9C AS DateTime), 1, CAST(0x0000A3BF00ABCABB AS DateTime))
INSERT [dbo].[Asset] ([AssetId], [AssetName], [MUId], [MisTake], [AssetStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (30, N'ÂÁ', 13, CAST(0.00000000 AS Numeric(18, 8)), 50, 1, CAST(0x0000A3BF00ABBC2E AS DateTime), 1, CAST(0x0000A3BF00ABD2E4 AS DateTime))
SET IDENTITY_INSERT [dbo].[Asset] OFF