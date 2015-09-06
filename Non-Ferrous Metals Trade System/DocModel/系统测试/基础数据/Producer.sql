USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[Producer] ON
INSERT [dbo].[Producer] ([ProducerId], [ProducerName], [ProducerFullName], [ProducerShort], [ProducerArea], [ProducerStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (8, N'电解铜生产商1', N'电解铜生产商1', N'电解铜生产商1', 74, 50, 1, CAST(0x0000A3BF00B52399 AS DateTime), 1, CAST(0x0000A3BF00B53C3E AS DateTime))
INSERT [dbo].[Producer] ([ProducerId], [ProducerName], [ProducerFullName], [ProducerShort], [ProducerArea], [ProducerStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (9, N'电解铜生产商2', N'电解铜生产商2', N'电解铜生产商2', 76, 50, 1, CAST(0x0000A3BF00B53124 AS DateTime), 1, CAST(0x0000A3BF00B544A5 AS DateTime))
SET IDENTITY_INSERT [dbo].[Producer] OFF