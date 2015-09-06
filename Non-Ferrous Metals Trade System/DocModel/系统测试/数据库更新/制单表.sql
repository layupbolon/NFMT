USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentStock]    Script Date: 11/27/2014 11:13:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentStock]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentStock]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentStock]    Script Date: 11/27/2014 11:13:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_DocumentStock](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentId] [int] NULL,
	[OrderId] [int] NULL,
	[OrderStockDetailId] [int] NULL,
	[StockId] [int] NULL,
	[StockNameId] [int] NULL,
	[RefNo] [varchar](200) NULL,
	[DetailStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTSTOCK] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'DocumentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ������ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'OrderStockDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'StockId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҵ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'StockNameId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҵ�񵥺�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'RefNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ������ϸ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentStock'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderStock]    Script Date: 11/27/2014 11:13:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderStock]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentOrderStock]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderStock]    Script Date: 11/27/2014 11:13:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_DocumentOrderStock](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[StockId] [int] NULL,
	[StockNameId] [int] NULL,
	[RefNo] [varchar](200) NULL,
	[ApplyAmount] [decimal](18, 4) NULL,
	[DetailStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTORDERSTOCK] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'StockId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҵ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'StockNameId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ҵ�񵥺�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'RefNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'ApplyAmount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ������ϸ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderStock'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderInvoice]    Script Date: 11/27/2014 11:13:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderInvoice]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentOrderInvoice]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderInvoice]    Script Date: 11/27/2014 11:13:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_DocumentOrderInvoice](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[StockDetailId] [int] NULL,
	[InvoiceNo] [varchar](200) NULL,
	[InvoiceBala] [decimal](18, 4) NULL,
	[DetailStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTORDERINVOICE] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'StockDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'InvoiceNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'InvoiceBala'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ�Ʊ��ϸ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderInvoice'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderDetail]    Script Date: 11/27/2014 11:13:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderDetail]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentOrderDetail]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderDetail]    Script Date: 11/27/2014 11:13:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_DocumentOrderDetail](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[InvoiceCopies] [int] NULL,
	[InvoiceSpecific] [varchar](2000) NULL,
	[QualityCopies] [int] NULL,
	[QualitySpecific] [varchar](2000) NULL,
	[WeightCopies] [int] NULL,
	[WeightSpecific] [varchar](2000) NULL,
	[TexCopies] [int] NULL,
	[TexSpecific] [varchar](2000) NULL,
	[DeliverCopies] [int] NULL,
	[DeliverSpecific] [varchar](2000) NULL,
	[TotalInvCopies] [int] NULL,
	[TotalInvSpecific] [varchar](2000) NULL,
	[DetailStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTORDERDETAIL] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'InvoiceCopies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ����Ҫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'InvoiceSpecific'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'QualityCopies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤������Ҫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'QualitySpecific'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'WeightCopies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤����Ҫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'WeightSpecific'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'װ�䵥����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'TexCopies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'װ�䵥����Ҫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'TexSpecific'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'DeliverCopies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֤������Ҫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'DeliverSpecific'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'TotalInvCopies'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ����Ҫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'TotalInvSpecific'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ����ϸ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderDetail'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderAttach]    Script Date: 11/27/2014 11:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrderAttach]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentOrderAttach]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrderAttach]    Script Date: 11/27/2014 11:13:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Doc_DocumentOrderAttach](
	[OrderAttachId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[AttachId] [int] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTORDERATTACH] PRIMARY KEY CLUSTERED 
(
	[OrderAttachId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderAttach', @level2type=N'COLUMN',@level2name=N'OrderAttachId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderAttach', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderAttach', @level2type=N'COLUMN',@level2name=N'AttachId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrderAttach'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrder]    Script Date: 11/27/2014 11:13:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentOrder]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentOrder]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentOrder]    Script Date: 11/27/2014 11:13:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_DocumentOrder](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](200) NULL,
	[ApplyId] [int] NULL,
	[ContractId] [int] NULL,
	[ContractNo] [varchar](200) NULL,
	[SubId] [int] NULL,
	[LCId] [int] NULL,
	[LCNo] [varchar](200) NULL,
	[LCDay] [int] NULL,
	[OrderType] [int] NULL,
	[OrderDate] [datetime] NULL,
	[ApplyCorp] [int] NULL,
	[ApplyDept] [int] NULL,
	[SellerCorp] [int] NULL,
	[BuyerCorp] [int] NULL,
	[BuyerCorpName] [varchar](200) NULL,
	[BuyerAddress] [varchar](800) NULL,
	[PaymentStyle] [int] NULL,
	[RecBankId] [int] NULL,
	[DiscountBase] [varchar](200) NULL,
	[AssetId] [int] NULL,
	[BrandId] [int] NULL,
	[AreaId] [int] NULL,
	[AreaName] [varchar](400) NULL,
	[BankCode] [varchar](400) NULL,
	[GrossAmount] [decimal](18, 4) NULL,
	[NetAmount] [decimal](18, 4) NULL,
	[UnitId] [int] NULL,
	[Currency] [int] NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[InvBala] [decimal](18, 4) NULL,
	[InvGap] [decimal](18, 4) NULL,
	[Bundles] [int] NULL,
	[Meno] [varchar](4000) NULL,
	[OrderStatus] [int] NULL,
	[ApplyEmpId] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTORDER] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���κ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'OrderNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'ApplyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Լ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'ContractId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Լ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'ContractNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ӻ�Լ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'SubId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LC���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'LCId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LC���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'LCNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LC����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'LCDay'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'OrderType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���빫˾' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'ApplyCorp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���벿��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'ApplyDept'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ҷ���˾' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'SellerCorp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ҹ�˾' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'BuyerCorp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ҹ�˾����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'BuyerCorpName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ҵ�ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'BuyerAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�տʽ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'PaymentStyle'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�տ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'RecBankId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�۸�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'DiscountBase'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ʒ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'AssetId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ʒ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'BrandId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'AreaId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ԭ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'AreaName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���б�д' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'BankCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ë��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'GrossAmount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'NetAmount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��λ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'UnitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'Currency'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�۸�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ�ܼ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'InvBala'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'InvGap'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'Bundles'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ��״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'OrderStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'ApplyEmpId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentOrder'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentInvoice]    Script Date: 11/27/2014 11:13:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_DocumentInvoice]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_DocumentInvoice]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_DocumentInvoice]    Script Date: 11/27/2014 11:13:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_DocumentInvoice](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentId] [int] NULL,
	[OrderId] [int] NULL,
	[StockDetailId] [int] NULL,
	[OrderInvoiceDetailId] [int] NULL,
	[InvoiceNo] [varchar](200) NULL,
	[InvoiceId] [int] NULL,
	[InvoiceBala] [decimal](18, 4) NULL,
	[DetailStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENTINVOICE] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'DetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'DocumentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'StockDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ָ�Ʊ��ϸ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'OrderInvoiceDetailId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'InvoiceNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'InvoiceId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ʊ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'InvoiceBala'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ϸ״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'DetailStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ���Ʊ��ϸ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_DocumentInvoice'
GO


USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_Document]    Script Date: 11/27/2014 11:13:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doc_Document]') AND type in (N'U'))
DROP TABLE [dbo].[Doc_Document]
GO

USE [NFMT]
GO

/****** Object:  Table [dbo].[Doc_Document]    Script Date: 11/27/2014 11:13:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Doc_Document](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[DocumentDate] [datetime] NULL,
	[DocEmpId] [int] NULL,
	[PresentDate] [datetime] NULL,
	[Presenter] [int] NULL,
	[AcceptanceDate] [datetime] NULL,
	[Acceptancer] [int] NULL,
	[Meno] [varchar](4000) NULL,
	[DocumentStatus] [int] NULL,
	[CreatorId] [int] NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_DOC_DOCUMENT] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'DocumentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�ָ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'OrderId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'DocumentDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'DocEmpId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'PresentDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ж�ȷ��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'AcceptanceDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�״̬' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'DocumentStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'LastModifyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����޸�ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƶ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Doc_Document'
GO


