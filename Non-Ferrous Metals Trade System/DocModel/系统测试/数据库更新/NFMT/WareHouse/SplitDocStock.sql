alter table St_SplitDocStock_Ref
   drop constraint PK_ST_SPLITDOCSTOCK_REF
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_SplitDocStock_Ref')
            and   type = 'U')
   drop table St_SplitDocStock_Ref
go

/*==============================================================*/
/* Table: St_SplitDocStock_Ref                                  */
/*==============================================================*/
create table St_SplitDocStock_Ref (
   RefId                int                  identity,
   SplitDocDetailId     int                  null,
   NewRefNoId           int                  null,
   NewStockId           int                  null,
   OldRefNoId           int                  null,
   OldStockId           int                  null,
   RefStatus            int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '拆单库存关联表',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联序号',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'RefId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拆单明细序号',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'SplitDocDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新业务单序号',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'NewRefNoId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'NewStockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '旧业务单序号',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'OldRefNoId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '旧库存序号',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'OldStockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关联状态',
   'user', @CurrentUser, 'table', 'St_SplitDocStock_Ref', 'column', 'RefStatus'
go

alter table St_SplitDocStock_Ref
   add constraint PK_ST_SPLITDOCSTOCK_REF primary key (RefId)
go

/****** Object:  Stored Procedure [dbo].St_SplitDocStock_RefGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocStock_RefGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocStock_RefGet]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocStock_RefLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocStock_RefLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocStock_RefLoad]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocStock_RefInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocStock_RefInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocStock_RefInsert]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocStock_RefUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocStock_RefUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocStock_RefUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocStock_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocStock_RefUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocStock_RefUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_SplitDocStock_RefUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_SplitDocStock_RefGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_SplitDocStock_RefGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_SplitDocStock_RefUpdateStatus
// 存储过程功能描述：更新St_SplitDocStock_Ref中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocStock_RefUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDocStock_Ref'

set @str = 'update [dbo].[St_SplitDocStock_Ref] '+
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
// 存储过程名：[dbo].St_SplitDocStock_RefGoBack
// 存储过程功能描述：撤返St_SplitDocStock_Ref，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocStock_RefGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_SplitDocStock_Ref'

set @str = 'update [dbo].[St_SplitDocStock_Ref] '+
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
// 存储过程名：[dbo].St_SplitDocStock_RefGet
// 存储过程功能描述：查询指定St_SplitDocStock_Ref的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocStock_RefGet
    /*
	@RefId int
    */
    @id int
AS

SELECT
	[RefId],
	[SplitDocDetailId],
	[NewRefNoId],
	[NewStockId],
	[OldRefNoId],
	[OldStockId],
	[RefStatus]
FROM
	[dbo].[St_SplitDocStock_Ref]
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
// 存储过程名：[dbo].St_SplitDocStock_RefLoad
// 存储过程功能描述：查询所有St_SplitDocStock_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocStock_RefLoad
AS

SELECT
	[RefId],
	[SplitDocDetailId],
	[NewRefNoId],
	[NewStockId],
	[OldRefNoId],
	[OldStockId],
	[RefStatus]
FROM
	[dbo].[St_SplitDocStock_Ref]

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
// 存储过程名：[dbo].St_SplitDocStock_RefInsert
// 存储过程功能描述：新增一条St_SplitDocStock_Ref记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocStock_RefInsert
	@SplitDocDetailId int =NULL ,
	@NewRefNoId int =NULL ,
	@NewStockId int =NULL ,
	@OldRefNoId int =NULL ,
	@OldStockId int =NULL ,
	@RefStatus int =NULL ,
	@RefId int OUTPUT
AS

INSERT INTO [dbo].[St_SplitDocStock_Ref] (
	[SplitDocDetailId],
	[NewRefNoId],
	[NewStockId],
	[OldRefNoId],
	[OldStockId],
	[RefStatus]
) VALUES (
	@SplitDocDetailId,
	@NewRefNoId,
	@NewStockId,
	@OldRefNoId,
	@OldStockId,
	@RefStatus
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
// 存储过程名：[dbo].St_SplitDocStock_RefUpdate
// 存储过程功能描述：更新St_SplitDocStock_Ref
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_SplitDocStock_RefUpdate
    @RefId int,
@SplitDocDetailId int = NULL,
@NewRefNoId int = NULL,
@NewStockId int = NULL,
@OldRefNoId int = NULL,
@OldStockId int = NULL,
@RefStatus int = NULL
AS

UPDATE [dbo].[St_SplitDocStock_Ref] SET
	[SplitDocDetailId] = @SplitDocDetailId,
	[NewRefNoId] = @NewRefNoId,
	[NewStockId] = @NewStockId,
	[OldRefNoId] = @OldRefNoId,
	[OldStockId] = @OldStockId,
	[RefStatus] = @RefStatus
WHERE
	[RefId] = @RefId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



