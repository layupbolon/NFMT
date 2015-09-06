USE [NFMT_User]
GO

SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (2, N'超级管理员', 50, 0, CAST(0x0000A35C00FBBD4C AS DateTime), 102, CAST(0x0000A3A900FA65C6 AS DateTime))
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (3, N'总裁', 50, 0, CAST(0x0000A35C00FC5D75 AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (4, N'副总裁', 50, 0, CAST(0x0000A35C00FC5D7B AS DateTime), 1, CAST(0x0000A363014F90FE AS DateTime))
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (5, N'总监', 50, 0, CAST(0x0000A35C00FC5D83 AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (6, N'总经理', 50, 0, CAST(0x0000A35C00FC5D8F AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (7, N'经理', 50, 0, CAST(0x0000A35C00FC5D92 AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (8, N'员工', 50, 0, CAST(0x0000A35C00FC5DA0 AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (12, N'2', 1, 1, CAST(0x0000A3A900F7C595 AS DateTime), 1, CAST(0x0000A3A900FA87E3 AS DateTime))
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (13, N'6', 35, 1, CAST(0x0000A3B000AC19D2 AS DateTime), 1, CAST(0x0000A3B000AC41FA AS DateTime))
INSERT [dbo].[Role] ([RoleId], [RoleName], [RoleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (14, N'4', 1, 1, CAST(0x0000A3C700A173BB AS DateTime), 1, CAST(0x0000A3C700A42F2A AS DateTime))
SET IDENTITY_INSERT [dbo].[Role] OFF