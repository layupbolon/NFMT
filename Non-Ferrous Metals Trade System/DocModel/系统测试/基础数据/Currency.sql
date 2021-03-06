USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[Currency] ON
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [CurrencyStatus], [CurrencyFullName], [CurencyShort], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (5, N'美元', 50, N'美元', N'美元', 1, CAST(0x0000A3BF00ABEF6D AS DateTime), 1, CAST(0x0000A3BF00AC5DB1 AS DateTime))
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [CurrencyStatus], [CurrencyFullName], [CurencyShort], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (6, N'人民币', 50, N'人民币', N'人民币', 1, CAST(0x0000A3BF00AC248B AS DateTime), 1, CAST(0x0000A3BF00AC696A AS DateTime))
INSERT [dbo].[Currency] ([CurrencyId], [CurrencyName], [CurrencyStatus], [CurrencyFullName], [CurencyShort], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (7, N'欧元', 50, N'欧元', N'欧元', 1, CAST(0x0000A3BF00AC54F9 AS DateTime), 1, CAST(0x0000A3BF00AC71A6 AS DateTime))
SET IDENTITY_INSERT [dbo].[Currency] OFF
