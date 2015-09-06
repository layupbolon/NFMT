alter table St_CustomsApplyDetail
   drop constraint PK_ST_CUSTOMSAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_CustomsApplyDetail')
            and   type = 'U')
   drop table St_CustomsApplyDetail
go

/*==============================================================*/
/* Table: St_CustomsApplyDetail                                 */
/*==============================================================*/
create table St_CustomsApplyDetail (
   DetailId             int                  identity,
   CustomsApplyId       int                  null,
   StockId              int                  null,
   GrossWeight          decimal(18,4)        null,
   NetWeight            decimal(18,4)        null,
   CustomsPrice         decimal(18,4)        null,
   DetailStatus         int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '报关申请明细',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关申请序号',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'CustomsApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请毛量',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'GrossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请净量',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'NetWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报关单价',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'CustomsPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'St_CustomsApplyDetail', 'column', 'DetailStatus'
go

alter table St_CustomsApplyDetail
   add constraint PK_ST_CUSTOMSAPPLYDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].St_CustomsApplyDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_CustomsApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_CustomsApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_CustomsApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_CustomsApplyDetailUpdateStatus
// 存储过程功能描述：更新St_CustomsApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsApplyDetail'

set @str = 'update [dbo].[St_CustomsApplyDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsApplyDetailGoBack
// 存储过程功能描述：撤返St_CustomsApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_CustomsApplyDetail'

set @str = 'update [dbo].[St_CustomsApplyDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_CustomsApplyDetailGet
// 存储过程功能描述：查询指定St_CustomsApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[CustomsApplyId],
	[StockId],
	[GrossWeight],
	[NetWeight],
	[CustomsPrice],
	[DetailStatus]
FROM
	[dbo].[St_CustomsApplyDetail]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].St_CustomsApplyDetailLoad
// 存储过程功能描述：查询所有St_CustomsApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyDetailLoad
AS

SELECT
	[DetailId],
	[CustomsApplyId],
	[StockId],
	[GrossWeight],
	[NetWeight],
	[CustomsPrice],
	[DetailStatus]
FROM
	[dbo].[St_CustomsApplyDetail]

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
// 存储过程名：[dbo].St_CustomsApplyDetailInsert
// 存储过程功能描述：新增一条St_CustomsApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyDetailInsert
	@CustomsApplyId int =NULL ,
	@StockId int =NULL ,
	@GrossWeight decimal(18, 4) =NULL ,
	@NetWeight decimal(18, 4) =NULL ,
	@CustomsPrice decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[St_CustomsApplyDetail] (
	[CustomsApplyId],
	[StockId],
	[GrossWeight],
	[NetWeight],
	[CustomsPrice],
	[DetailStatus]
) VALUES (
	@CustomsApplyId,
	@StockId,
	@GrossWeight,
	@NetWeight,
	@CustomsPrice,
	@DetailStatus
)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].St_CustomsApplyDetailUpdate
// 存储过程功能描述：更新St_CustomsApplyDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_CustomsApplyDetailUpdate
    @DetailId int,
@CustomsApplyId int = NULL,
@StockId int = NULL,
@GrossWeight decimal(18, 4) = NULL,
@NetWeight decimal(18, 4) = NULL,
@CustomsPrice decimal(18, 4) = NULL,
@DetailStatus int = NULL
AS

UPDATE [dbo].[St_CustomsApplyDetail] SET
	[CustomsApplyId] = @CustomsApplyId,
	[StockId] = @StockId,
	[GrossWeight] = @GrossWeight,
	[NetWeight] = @NetWeight,
	[CustomsPrice] = @CustomsPrice,
	[DetailStatus] = @DetailStatus
WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



