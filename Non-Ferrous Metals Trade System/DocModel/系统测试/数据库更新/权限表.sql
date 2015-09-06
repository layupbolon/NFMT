

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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Ȩ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'AuthOperateId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Ȩ�ޱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'OperateCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Ȩ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'OperateName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'OperateType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�˵����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'MenuId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Ȩ�ޱ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthOperate'
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ȩ�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'AuthGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ա�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'EmpId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ա��Ȩ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroupDetail'
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ȩ�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AuthGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ȩ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AuthGroupName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ʒ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AssetId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ó�׷��򣨽��ڣ����ڣ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'TradeDirection'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ó�׷�����ó����ó��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'TradeBorder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ⲿ��Լ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'ContractInOut'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Լ���㵥' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'ContractLimit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��˾���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'CorpId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ȩ����״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'AuthGroupStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ȩ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AuthGroup'
GO
