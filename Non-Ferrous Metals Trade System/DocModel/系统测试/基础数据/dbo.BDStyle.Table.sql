USE [NFMT_Basic]
GO
/****** Object:  Table [dbo].[BDStyle]    Script Date: 11/27/2014 11:03:26 ******/
DROP TABLE [dbo].[BDStyle]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BDStyle](
	[BDStyleId] [int] NOT NULL,
	[BDStyleCode] [varchar](80) NOT NULL,
	[BDStyleName] [varchar](80) NOT NULL,
	[BDStyleStatus] [int] NOT NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_BDStyle] PRIMARY KEY CLUSTERED 
(
	[BDStyleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础类型编码表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BDStyle'
GO
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (1, N'PayMatter', N'付款事项', 50, 1, CAST(0x0000A35E009BAA07 AS DateTime), 1, CAST(0x0000A35E009BAA07 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (2, N'DPType', N'交货地类型', 50, 1, CAST(0x0000A35E009BAA08 AS DateTime), 1, CAST(0x0000A35E009BAA08 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (3, N'PAType', N'付款类型', 50, 1, CAST(0x0000A35E009BAA08 AS DateTime), 1, CAST(0x0000A35E009BAA08 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (4, N'PayMode', N'付款方式', 50, 1, CAST(0x0000A35E009BAA08 AS DateTime), 1, CAST(0x0000A35E009BAA08 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (5, N'CustomType', N'报关状态', 50, 1, CAST(0x0000A35E009BAA08 AS DateTime), 1, CAST(0x0000A35E009BAA08 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (6, N'StockType', N'库存类型', 50, 1, CAST(0x0000A35E009BAA08 AS DateTime), 1, CAST(0x0000A35E009BAA08 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (7, N'CorpType', N'公司类型', 50, 1, CAST(0x0000A35E009BAA08 AS DateTime), 1, CAST(0x0000A35E009BAA08 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (8, N'LogDirection', N'流水方向', 50, 1, CAST(0x0000A35E009BAA09 AS DateTime), 1, CAST(0x0000A35E009BAA09 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (9, N'InvioceType', N'发票类型', 50, 1, CAST(0x0000A35E009BAA09 AS DateTime), 1, CAST(0x0000A35E009BAA09 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (10, N'InvoiceDirection', N'发票方向', 50, 1, CAST(0x0000A35E009BAA09 AS DateTime), 1, CAST(0x0000A35E009BAA09 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (11, N'TradeBorder', N'贸易境区', 50, 1, CAST(0x0000A35E009BAA09 AS DateTime), 1, CAST(0x0000A35E009BAA09 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (12, N'ContractLimit', N'合同时限', 50, 1, CAST(0x0000A35E009BAA0D AS DateTime), 1, CAST(0x0000A35E009BAA0D AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (13, N'ContractSide', N'合同对方', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (14, N'HaveGoodsFlow', N'贸易融资', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (15, N'PriceMode', N'定价方式', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (16, N'MarginMode', N'价格保证金方式', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (17, N'TradeDirection', N'贸易方向', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (18, N'WorkStatus', N'在职状态', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (19, N'BusinessType', N'业务类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (20, N'SmsType', N'消息类别', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (21, N'ApplyType', N'申请类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (22, N'ConditionType', N'条件类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (23, N'LogicType', N'逻辑类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (24, N'NodeType', N'节点类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (25, N'LogSourceType', N'流水来源类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (26, N'DeptType', N'部门类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (27, N'CapitalType', N'资本类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (28, N'AttachType', N'附件类型', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (29, N'DoPriceType', N'作价方式', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (30, N'DoPriceReason', N'点价动因', 50, 1, CAST(0x0000A35E009BAA0E AS DateTime), 1, CAST(0x0000A35E009BAA0E AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (31, N'PledgeType', N'质押状态', 50, 1, CAST(0x0000A35E009BAA0F AS DateTime), 1, CAST(0x0000A35E009BAA0F AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (32, N'BillType', N'单据类型', 50, 1, CAST(0x0000A35E009BAA0F AS DateTime), 1, CAST(0x0000A35E009BAA0F AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (33, N'ContractPayment', N'合约付款方式', 50, 1, CAST(0x0000A35E009BAA10 AS DateTime), 1, CAST(0x0000A35E009BAA10 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (34, N'LogType', N'流水类型', 50, 1, CAST(0x0000A35E009BAA10 AS DateTime), 1, CAST(0x0000A35E009BAA10 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (35, N'FlowDirection', N'收付方向', 50, 1, CAST(0x0000A35E009BAA10 AS DateTime), 1, CAST(0x0000A35E009BAA10 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (36, N'FundsLog', N'资金流水类型', 50, 1, CAST(0x0000A35E009BAA10 AS DateTime), 1, CAST(0x0000A35E009BAA10 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (37, N'DisplayType', N'合约条款显示类型', 50, 1, CAST(0x0000A35E009BAA10 AS DateTime), 1, CAST(0x0000A35E009BAA10 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (38, N'ValueType', N'值类型', 50, 1, CAST(0x0000A36F00000000 AS DateTime), 1, CAST(0x0000A35E009BAA10 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (39, N'ValueRateType', N'费率类型', 50, 1, CAST(0x0000A37200000000 AS DateTime), 1, CAST(0x0000A37200000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (40, N'DiscountBase', N'贴现基准', 50, 1, CAST(0x0000A37200000000 AS DateTime), 1, CAST(0x0000A37200000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (41, N'WhoDoPrice', N'点价方', 50, 1, CAST(0x0000A37200000000 AS DateTime), 1, CAST(0x0000A37200000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (42, N'SummaryPrice', N'计价方式', 50, 1, CAST(0x0000A37200000000 AS DateTime), 1, CAST(0x0000A37200000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (43, N'FundsType', N'财务类型', 50, 1, CAST(0x0000A37200000000 AS DateTime), 1, CAST(0x0000A37200000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (44, N'AllotFrom', N'分配来源', 50, 1, CAST(0x0000A38B011416AA AS DateTime), 1, CAST(0x0000A39900FB232D AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (45, N'FeeType', N'发票内容', 50, 1, CAST(0x0000A39900FB232D AS DateTime), 1, CAST(0x0000A39900FB232D AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (46, N'ReceiptType', N'回执类型', 50, 1, CAST(0x0000A39900FB232D AS DateTime), 1, CAST(0x0000A39900FB232D AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (47, N'ReadStatus', N'已读状态', 50, 1, CAST(0x0000A3A100E30CEB AS DateTime), 1, CAST(0x0000A3A100E30CEB AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (48, N'AttachType', N'附件类型', 50, 1, CAST(0x0000A3A100E30CEB AS DateTime), 1, CAST(0x0000A3A100E30CEB AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (49, N'PricingDirection', N'点价方向', 50, 1, CAST(0x0000A3C300000000 AS DateTime), 1, CAST(0x0000A3C300000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (50, N'CashInAllotTypeEnum', N'收款分配类型', 50, 1, CAST(0x0000A3C300000000 AS DateTime), 1, CAST(0x0000A3C300000000 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (51, N'AuditEmpType', N'审核人类型', 50, 1, CAST(0x0000A3E1010FFE43 AS DateTime), 1, CAST(0x0000A3E1010FFE43 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (52, N'OperateType', N'操作类型', 50, 1, CAST(0x0000A3E1010FFE43 AS DateTime), 1, CAST(0x0000A3E1010FFE43 AS DateTime))
INSERT [dbo].[BDStyle] ([BDStyleId], [BDStyleCode], [BDStyleName], [BDStyleStatus], [CreatorId], [CreateTime], [LastModifyId], [LastModifyTime]) VALUES (53, N'OrderType', N'制单指令类型', 50, 1, CAST(0x0000A3E900000000 AS DateTime), 1, CAST(0x0000A3E900000000 AS DateTime))
