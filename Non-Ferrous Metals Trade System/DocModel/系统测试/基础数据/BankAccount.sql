USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[BankAccount] ON
INSERT [dbo].[BankAccount] ([BankAccId], [CompanyId], [BankId], [AccountNo], [CurrencyId], [BankAccDesc], [BankAccStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (243, 5, 165, N'62358845364444555', 5, N'�й�������Ԫ�˻�', 50, 1, CAST(0x0000A3BF00AF582C AS DateTime), 1, CAST(0x0000A3BF00B367A7 AS DateTime))
INSERT [dbo].[BankAccount] ([BankAccId], [CompanyId], [BankId], [AccountNo], [CurrencyId], [BankAccDesc], [BankAccStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (244, 2, 166, N'6253842174217575', 5, N'������Ԫ�˻�', 50, 1, CAST(0x0000A3BF00AF87CB AS DateTime), 1, CAST(0x0000A3BF00B359F9 AS DateTime))
INSERT [dbo].[BankAccount] ([BankAccId], [CompanyId], [BankId], [AccountNo], [CurrencyId], [BankAccDesc], [BankAccStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (245, 2, 167, N'6258452125515165', 6, N'����������˻�', 50, 1, CAST(0x0000A3BF00AFD9BB AS DateTime), 1, CAST(0x0000A3BF00B34B8D AS DateTime))
INSERT [dbo].[BankAccount] ([BankAccId], [CompanyId], [BankId], [AccountNo], [CurrencyId], [BankAccDesc], [BankAccStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (246, 5, 165, N'6322454545', 6, N'����������˻�', 50, 1, CAST(0x0000A3BF00B2308E AS DateTime), 1, CAST(0x0000A3BF00B3722D AS DateTime))
SET IDENTITY_INSERT [dbo].[BankAccount] OFF