USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[MeasureUnit] ON
INSERT [dbo].[MeasureUnit] ([MUId], [MUName], [BaseId], [TransformRate], [MUStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (13, N'¶Ö', 0, CAST(0.00 AS Decimal(8, 2)), 50, 1, CAST(0x0000A3BF00AA2A45 AS DateTime), 1, CAST(0x0000A3BF00AA4741 AS DateTime))
INSERT [dbo].[MeasureUnit] ([MUId], [MUName], [BaseId], [TransformRate], [MUStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (14, N'Ç§¿Ë', 0, CAST(1000.00 AS Decimal(8, 2)), 50, 1, CAST(0x0000A3BF00AB0ADD AS DateTime), 1, CAST(0x0000A3BF00AB426F AS DateTime))
SET IDENTITY_INSERT [dbo].[MeasureUnit] OFF