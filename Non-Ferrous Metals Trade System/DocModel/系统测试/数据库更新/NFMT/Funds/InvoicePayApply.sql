alter table dbo.Fun_InvoicePayApply_Ref
   drop constraint PK_FUN_INVOICEPAYAPPLY_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Fun_InvoicePayApply_Ref')
            and   type = 'U')
   drop table dbo.Fun_InvoicePayApply_Ref
go

/*==============================================================*/
/* Table: Fun_InvoicePayApply_Ref                               */
/*==============================================================*/
create table dbo.Fun_InvoicePayApply_Ref (
   RefId                int                  identity,
   PayApplyId           int                  null,
   InvoiceId            int                  null,
   SIId                 int                  null,
   ApplyBala            decimal(18,4)        null,
   DetailStatus         int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '发票付款申请',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票付款申请序号',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref', 'column', 'RefId'
go

execute sp_addextendedproperty 'MS_Description', 
   '付款申请序号',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref', 'column', 'PayApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '发票序号',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref', 'column', 'InvoiceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '价外票序号',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref', 'column', 'SIId'
go

execute sp_addextendedproperty 'MS_Description', 
   '申请金额',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref', 'column', 'ApplyBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', 'dbo', 'table', 'Fun_InvoicePayApply_Ref', 'column', 'DetailStatus'
go

alter table dbo.Fun_InvoicePayApply_Ref
   add constraint PK_FUN_INVOICEPAYAPPLY_REF primary key (RefId)
go


/****** Object:  Stored Procedure [dbo].Fun_InvoicePayApply_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_InvoicePayApply_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_InvoicePayApply_RefGet]
GO

/****** Object:  Stored Procedure [dbo].Fun_InvoicePayApply_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_InvoicePayApply_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_InvoicePayApply_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].Fun_InvoicePayApply_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_InvoicePayApply_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_InvoicePayApply_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].Fun_InvoicePayApply_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_InvoicePayApply_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_InvoicePayApply_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fun_InvoicePayApply_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_InvoicePayApply_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_InvoicePayApply_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fun_InvoicePayApply_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fun_InvoicePayApply_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fun_InvoicePayApply_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_InvoicePayApply_RefUpdateStatus
// 存储过程功能描述：更新Fun_InvoicePayApply_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_InvoicePayApply_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_InvoicePayApply_Ref'

set @str = 'update [dbo].[Fun_InvoicePayApply_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
exec(@str)

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_InvoicePayApply_RefGoBack
// 存储过程功能描述：撤返Fun_InvoicePayApply_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_InvoicePayApply_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fun_InvoicePayApply_Ref'

set @str = 'update [dbo].[Fun_InvoicePayApply_Ref] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where RefId = '+ Convert(varchar,@id) 
set @str = @str + ' update NFMT_WorkFlow.dbo.Wf_DataSource set DataStatus = (select DetailId from NFMT_Basic.dbo.BDStatusDetail where StatusName = ''已作废'' and StatusId = 1) where DataBaseName = '''+@dbName+''' and TableName = '''+@tableName+''' and RowId = ' + Convert(varchar,@id)
exec(@str)

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO









SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_InvoicePayApply_RefGet
// 存储过程功能描述：查询指定Fun_InvoicePayApply_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_InvoicePayApply_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[PayApplyId],
	[InvoiceId],
	[SIId],
	[ApplyBala],
	[DetailStatus]
FROM
	[dbo].[Fun_InvoicePayApply_Ref]
WHERE
	[RefId] = @id

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_InvoicePayApply_RefLoad
// 存储过程功能描述：查询所有Fun_InvoicePayApply_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_InvoicePayApply_RefLoad
AS

SELECT
	[RefId],
	[PayApplyId],
	[InvoiceId],
	[SIId],
	[ApplyBala],
	[DetailStatus]
FROM
	[dbo].[Fun_InvoicePayApply_Ref]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO







SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_InvoicePayApply_RefInsert
// 存储过程功能描述：新增一条Fun_InvoicePayApply_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_InvoicePayApply_RefInsert
	@PayApplyId int =NULL ,
	@InvoiceId int =NULL ,
	@SIId int =NULL ,
	@ApplyBala decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[Fun_InvoicePayApply_Ref] (
	[PayApplyId],
	[InvoiceId],
	[SIId],
	[ApplyBala],
	[DetailStatus]
) VALUES (
	@PayApplyId,
	@InvoiceId,
	@SIId,
	@ApplyBala,
	@DetailStatus
)


SET @RefId = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fun_InvoicePayApply_RefUpdate
// 存储过程功能描述：更新Fun_InvoicePayApply_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fun_InvoicePayApply_RefUpdate
    @RefId int,
@PayApplyId int = NULL,
@InvoiceId int = NULL,
@SIId int = NULL,
@ApplyBala decimal(18, 4) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[Fun_InvoicePayApply_Ref] SET
	[PayApplyId] = @PayApplyId,
	[InvoiceId] = @InvoiceId,
	[SIId] = @SIId,
	[ApplyBala] = @ApplyBala,
	[DetailStatus] = @DetailStatus
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



