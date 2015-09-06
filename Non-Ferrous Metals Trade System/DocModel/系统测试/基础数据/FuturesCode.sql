USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[FuturesCode] ON
INSERT [dbo].[FuturesCode] ([FuturesCodeId], [ExchageId], [CodeSize], [FirstTradeDate], [LastTradeDate], [MUId], [CurrencyId], [AssetId], [FuturesCodeStatus], [TradeCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (1, 6, CAST(5.0000 AS Numeric(19, 4)), CAST(0x0000A3B700000000 AS DateTime), CAST(0x0000A3D500000000 AS DateTime), 13, 6, 29, 50, N'CU1410', 1, CAST(0x0000A3C400BD63E3 AS DateTime), 1, CAST(0x0000A3C5009D715D AS DateTime))
INSERT [dbo].[FuturesCode] ([FuturesCodeId], [ExchageId], [CodeSize], [FirstTradeDate], [LastTradeDate], [MUId], [CurrencyId], [AssetId], [FuturesCodeStatus], [TradeCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (2, 5, CAST(25.0000 AS Numeric(19, 4)), CAST(0x0000A3B700000000 AS DateTime), CAST(0x0000A3D500000000 AS DateTime), 13, 5, 29, 50, N'CU1410', 1, CAST(0x0000A3C400BDBED4 AS DateTime), 1, CAST(0x0000A3C5009D64C1 AS DateTime))
SET IDENTITY_INSERT [dbo].[FuturesCode] OFF