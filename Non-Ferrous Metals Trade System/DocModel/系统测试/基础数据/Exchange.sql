USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[Exchange] ON
INSERT [dbo].[Exchange] ([ExchangeId], [ExchangeName], [ExchangeCode], [ExchangeStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (5, N'�׶ؽ���������', N'LME', 50, 1, CAST(0x0000A3BF00B71697 AS DateTime), 1, CAST(0x0000A3BF00B72FEA AS DateTime))
INSERT [dbo].[Exchange] ([ExchangeId], [ExchangeName], [ExchangeCode], [ExchangeStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (6, N'�Ϻ��ڻ�������', N'SHFE', 50, 1, CAST(0x0000A3BF00B7280E AS DateTime), 1, CAST(0x0000A3BF00B73940 AS DateTime))
SET IDENTITY_INSERT [dbo].[Exchange] OFF