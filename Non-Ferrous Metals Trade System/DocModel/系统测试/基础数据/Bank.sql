USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[Bank] ON
INSERT [dbo].[Bank] ([BankId], [BankName], [BankEname], [BankFullName], [BankShort], [CapitalType], [BankLevel], [ParentId], [BankStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (165, N'中国银行', N'中国银行', N'中国银行', N'中国银行', 73, 1, 0, 50, 1, CAST(0x0000A3BF00AC904D AS DateTime), 1, CAST(0x0000A3BF00ACE112 AS DateTime))
INSERT [dbo].[Bank] ([BankId], [BankName], [BankEname], [BankFullName], [BankShort], [CapitalType], [BankLevel], [ParentId], [BankStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (166, N'中国工商银行', N'中国工商银行', N'中国工商银行', N'中国工商银行', 73, 1, 0, 50, 1, CAST(0x0000A3BF00ACBDF8 AS DateTime), 1, CAST(0x0000A3BF00ACFB01 AS DateTime))
INSERT [dbo].[Bank] ([BankId], [BankName], [BankEname], [BankFullName], [BankShort], [CapitalType], [BankLevel], [ParentId], [BankStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (167, N'中国建设银行', N'中国建设银行', N'中国建设银行', N'中国建设银行', 73, 1, 0, 50, 1, CAST(0x0000A3BF00ACCDE6 AS DateTime), 1, CAST(0x0000A3BF00AD03CA AS DateTime))
SET IDENTITY_INSERT [dbo].[Bank] OFF