alter table St_StocktAttach
   drop constraint PK_ST_STOCKTATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_StocktAttach')
            and   type = 'U')
   drop table St_StocktAttach
go

/*==============================================================*/
/* Table: St_StocktAttach                                       */
/*==============================================================*/
create table St_StocktAttach (
   StockAttachId        int                  identity,
   StockId              int                  null,
   AttachId             int                  null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '库存附件',
   'user', @CurrentUser, 'table', 'St_StocktAttach'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存附件序号',
   'user', @CurrentUser, 'table', 'St_StocktAttach', 'column', 'StockAttachId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'St_StocktAttach', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件主表序号',
   'user', @CurrentUser, 'table', 'St_StocktAttach', 'column', 'AttachId'
go

alter table St_StocktAttach
   add constraint PK_ST_STOCKTATTACH primary key (StockAttachId)
go


/****** Object:  Stored Procedure [dbo].St_StocktAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StocktAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StocktAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_StocktAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StocktAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StocktAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StocktAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StocktAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StocktAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StocktAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StocktAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StocktAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StocktAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StocktAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StocktAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StocktAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StocktAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StocktAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StocktAttachUpdateStatus
// 存储过程功能描述：更新St_StocktAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StocktAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StocktAttach'

set @str = 'update [dbo].[St_StocktAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StocktAttachGoBack
// 存储过程功能描述：撤返St_StocktAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StocktAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StocktAttach'

set @str = 'update [dbo].[St_StocktAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StocktAttachGet
// 存储过程功能描述：查询指定St_StocktAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StocktAttachGet
    /*
	@StockAttachId int
    */
    @id int
AS

SELECT
	[StockAttachId],
	[StockId],
	[AttachId]
FROM
	[dbo].[St_StocktAttach]
WHERE
	[StockAttachId] = @id

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
// 存储过程名：[dbo].St_StocktAttachLoad
// 存储过程功能描述：查询所有St_StocktAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StocktAttachLoad
AS

SELECT
	[StockAttachId],
	[StockId],
	[AttachId]
FROM
	[dbo].[St_StocktAttach]

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
// 存储过程名：[dbo].St_StocktAttachInsert
// 存储过程功能描述：新增一条St_StocktAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StocktAttachInsert
	@StockId int =NULL ,
	@AttachId int =NULL ,
	@StockAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_StocktAttach] (
	[StockId],
	[AttachId]
) VALUES (
	@StockId,
	@AttachId
)


SET @StockAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_StocktAttachUpdate
// 存储过程功能描述：更新St_StocktAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StocktAttachUpdate
    @StockAttachId int,
@StockId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_StocktAttach] SET
	[StockId] = @StockId,
	[AttachId] = @AttachId
WHERE
	[StockAttachId] = @StockAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



