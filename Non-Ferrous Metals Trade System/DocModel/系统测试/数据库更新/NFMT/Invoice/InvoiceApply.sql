alter table Inv_InvoiceApply
   drop constraint PK_INV_INVOICEAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Inv_InvoiceApply')
            and   type = 'U')
   drop table Inv_InvoiceApply
go

/*==============================================================*/
/* Table: Inv_InvoiceApply                                      */
/*==============================================================*/
create table Inv_InvoiceApply (
   InvoiceApplyId       int                  not null,
   ApplyId              int                  null,
   TotalBala            numeric(18,4)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '开票申请',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开票申请序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'InvoiceApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'ApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '开票金总额',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'TotalBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Inv_InvoiceApply', 'column', 'LastModifyTime'
go

alter table Inv_InvoiceApply
   add constraint PK_INV_INVOICEAPPLY primary key (InvoiceApplyId)
go


/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyGet    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyGet]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyLoad    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyInsert    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyUpdate    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyUpdateStatus    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Inv_InvoiceApplyUpdateStatus    Script Date: 2015年1月28日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Inv_InvoiceApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Inv_InvoiceApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Inv_InvoiceApplyUpdateStatus
// 存储过程功能描述：更新Inv_InvoiceApply中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_InvoiceApply'

set @str = 'update [dbo].[Inv_InvoiceApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where InvoiceApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_InvoiceApplyGoBack
// 存储过程功能描述：撤返Inv_InvoiceApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Inv_InvoiceApply'

set @str = 'update [dbo].[Inv_InvoiceApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where InvoiceApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Inv_InvoiceApplyGet
// 存储过程功能描述：查询指定Inv_InvoiceApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyGet
    /*
	@InvoiceApplyId int
    */
    @id int
AS

SELECT
	[InvoiceApplyId],
	[ApplyId],
	[TotalBala],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_InvoiceApply]
WHERE
	[InvoiceApplyId] = @id

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
// 存储过程名：[dbo].Inv_InvoiceApplyLoad
// 存储过程功能描述：查询所有Inv_InvoiceApply记录
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyLoad
AS

SELECT
	[InvoiceApplyId],
	[ApplyId],
	[TotalBala],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Inv_InvoiceApply]

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
// 存储过程名：[dbo].Inv_InvoiceApplyInsert
// 存储过程功能描述：新增一条Inv_InvoiceApply记录
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyInsert
	@ApplyId int =NULL ,
	@TotalBala numeric(18, 4) =NULL ,
	@CreatorId int =NULL ,
	@InvoiceApplyId int OUTPUT
AS

INSERT INTO [dbo].[Inv_InvoiceApply] (
	[ApplyId],
	[TotalBala],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ApplyId,
	@TotalBala,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @InvoiceApplyId = @@IDENTITY

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
// 存储过程名：[dbo].Inv_InvoiceApplyUpdate
// 存储过程功能描述：更新Inv_InvoiceApply
// 创建人：CodeSmith
// 创建时间： 2015年1月28日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Inv_InvoiceApplyUpdate
    @InvoiceApplyId int,
@ApplyId int = NULL,
@TotalBala numeric(18, 4) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Inv_InvoiceApply] SET
	[ApplyId] = @ApplyId,
	[TotalBala] = @TotalBala,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[InvoiceApplyId] = @InvoiceApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



