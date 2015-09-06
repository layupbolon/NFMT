USE [NFMT_Basic]
GO

SET IDENTITY_INSERT [dbo].[DeliverPlace] ON
INSERT [dbo].[DeliverPlace] ([DPId], [DPType], [DPArea], [DPCompany], [DPName], [DPFullName], [DPStatus], [DPAddress], [DPEAddress], [DPTel], [DPContact], [DPFax], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (152, 1, 74, 5, N'洋山港', N'洋山港', 50, N'', N'', N'02158745236', N'', N'', 1, CAST(0x0000A3BF00E755BB AS DateTime), NULL, NULL)
INSERT [dbo].[DeliverPlace] ([DPId], [DPType], [DPArea], [DPCompany], [DPName], [DPFullName], [DPStatus], [DPAddress], [DPEAddress], [DPTel], [DPContact], [DPFax], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (153, 1, 74, 5, N'吴淞港', N'吴淞港', 50, N'', N'', N'02158745236', N'', N'', 1, CAST(0x0000A3BF00E77F98 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[DeliverPlace] OFF