USE [NFMT_User]
GO

SET IDENTITY_INSERT [dbo].[Account] ON
INSERT [dbo].[Account] ([AccId], [AccountName], [PassWord], [AccStatus], [EmpId], [IsValid], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (1, N'administrator', N'123456', 1, 1, 1, 1, CAST(0x0000A34E00F95ABA AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([AccId], [AccountName], [PassWord], [AccStatus], [EmpId], [IsValid], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (3, N'tom', N'234567', 1, 4, 1, 1, CAST(0x0000A3A100000000 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
