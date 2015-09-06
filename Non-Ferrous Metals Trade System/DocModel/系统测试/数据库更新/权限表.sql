

CREATE TABLE [dbo].[AuthOperate](
	[AuthOperateId] [int] IDENTITY(1,1) NOT NULL,
	[OperateCode] [varchar](50) NULL,
	[OperateName] [varchar](50) NULL,
	[OperateType] [int] NULL,
	[MenuId] [int] NULL,
	[EmpId] [int] NULL,
	[AuthOperateStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_AUTHOPERATE] PRIMARY KEY CLUSTERED 
(
	[AuthOperateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作权限序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'AuthOperateId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'OperateCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作权限名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'OperateName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'OperateType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'MenuId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate'
GO


CREATE TABLE [dbo].[AuthGroupDetail](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[AuthGroupId] [int] NULL,
	[EmpId] [int] NULL,
	[DetailStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_AUTHGROUPDETAIL] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限组序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'AuthGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'EmpId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工权限组关联表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail'
GO

CREATE TABLE [dbo].[AuthGroup](
	[AuthGroupId] [int] IDENTITY(1,1) NOT NULL,
	[AuthGroupName] [varchar](800) NULL,
	[AssetId] [int] NULL,
	[TradeDirection] [int] NULL,
	[TradeBorder] [int] NULL,
	[ContractInOut] [int] NULL,
	[ContractLimit] [int] NULL,
	[CorpId] [int] NULL,
	[AuthGroupStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_AUTHGROUP] PRIMARY KEY CLUSTERED 
(
	[AuthGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限组序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AuthGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限组名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AuthGroupName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品种' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AssetId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贸易方向（进口，出口）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'TradeDirection'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'贸易方向（外贸，内贸）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'TradeBorder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内外部合约' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'ContractInOut'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合约长零单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'ContractLimit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'CorpId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限组状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AuthGroupStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup'
GO
