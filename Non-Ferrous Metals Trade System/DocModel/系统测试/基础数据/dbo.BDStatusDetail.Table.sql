USE [NFMT_Basic]
GO
/****** Object:  Table [dbo].[BDStatusDetail]    Script Date: 11/27/2014 11:03:26 ******/
DROP TABLE [dbo].[BDStatusDetail]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BDStatusDetail](
	[DetailId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[StatusName] [varchar](80) NOT NULL,
	[StatusCode] [varchar](80) NOT NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_BDStatusDetail] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础状态明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BDStatusDetail'
GO
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (1, 1, N'已作废', N'Closed', 1, CAST(0x0000A36A00E7FD89 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (10, 1, N'已关闭', N'Obsolete', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (20, 1, N'已录入', N'Entered', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (25, 1, N'已撤返', N'Backed', 1, CAST(0x0000A36A00E7FD89 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (30, 1, N'录入禁提交', N'Entered', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (35, 1, N'审核拒绝', N'Refusee', 1, CAST(0x0000A36A00E7FD89 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (40, 1, N'待审核', N'Pending', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (50, 1, N'已生效', N'Already', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (60, 1, N'已冻结', N'Frozen', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (70, 1, N'执行修改', N'Exec', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (80, 1, N'已完成', N'Completed', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (81, 1, N'部分完成', N'ParCompleted', 1, CAST(0x0000A36A00E7FD89 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (200, 2, N'预入库存', N'PreEntry', 1, CAST(0x0000A36100A06D39 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (201, 2, N'在途库存', N'InTrans', 1, CAST(0x0000A36A00E758F2 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (202, 2, N'待验库存', N'', 1, CAST(0x0000A36A00E79478 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (203, 2, N'在库正常', N'', 1, CAST(0x0000A36A00E79E81 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (204, 2, N'特储库存', N'', 1, CAST(0x0000A36A00E7A5AE AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (205, 2, N'回购申请', N'', 1, CAST(0x0000A36A00E7ACBD AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (206, 2, N'回购库存', N'', 1, CAST(0x0000A36A00E7B3C7 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (208, 2, N'质押配货', N'', 1, CAST(0x0000A36A00E7C161 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (209, 2, N'质押申请', N'', 1, CAST(0x0000A36A00E7CA14 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (210, 2, N'质押库存', N'', 1, CAST(0x0000A36A00E7D75B AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (211, 2, N'配货申请', N'', 1, CAST(0x0000A36A00E7DDBD AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (212, 2, N'配货库存', N'', 1, CAST(0x0000A36A00E7E3A7 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (213, 2, N'已售库存', N'', 1, CAST(0x0000A36A00E7EAA7 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (220, 2, N'已拆库存', N'', 1, CAST(0x0000A3DC010E6C3C AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (230, 2, N'报废库存', N'', 1, CAST(0x0000A36A00E7F141 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (240, 2, N'冻结库存', N'', 1, CAST(0x0000A36A00E7F761 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (250, 2, N'不合格品', N'', 1, CAST(0x0000A36A00E7FD89 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (260, 2, N'作废库存', N'', 1, CAST(0x0000A3DC010F03F9 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (300, 3, N'无效消息', N'InvalidSms', 1, CAST(0x0000A3A300A60297 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (301, 3, N'待处理消息', N'PendingSms', 1, CAST(0x0000A3A300A646A7 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (302, 3, N'已处理消息', N'ProcessedSms', 1, CAST(0x0000A3A300A66A3E AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (401, 4, N'在职', N'OnJob', 1, CAST(0x0000A3A900DC43D0 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (402, 4, N'离职', N'Dimission', 1, CAST(0x0000A3A900DC577C AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (403, 4, N'停职', N'Suspension', 1, CAST(0x0000A3A900DC87E5 AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (404, 4, N'退休', N'Retirement', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (501, 168, N'已作废', N'已作废', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (510, 168, N'已关闭', N'已关闭', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (520, 168, N'已录入', N'已录入', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (525, 168, N'已撤返', N'已撤返', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (535, 168, N'审核拒绝', N'审核拒绝', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (540, 168, N'待审核', N'待审核', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (550, 168, N'已生效', N'已生效', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (560, 168, N'已交单', N'已交单', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (570, 168, N'已承兑', N'已承兑', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (580, 168, N'银行退单', N'银行退单', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
INSERT [dbo].[BDStatusDetail] ([DetailId], [StatusId], [StatusName], [StatusCode], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (590, 168, N'已完成', N'已完成', 1, CAST(0x0000A3A900DCB29D AS DateTime), NULL, NULL)
