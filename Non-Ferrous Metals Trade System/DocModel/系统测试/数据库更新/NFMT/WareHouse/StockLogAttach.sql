alter table dbo.St_StockLogAttach
   drop constraint PK_ST_STOCKLOGATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockLogAttach')
            and   type = 'U')
   drop table dbo.St_StockLogAttach
go

/*==============================================================*/
/* Table: St_StockLogAttach                                     */
/*==============================================================*/
create table dbo.St_StockLogAttach (
   StockLogAttachId     int                  identity,
   StockLogId           int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '流水附件',
   'user', 'dbo', 'table', 'St_StockLogAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '流水附件序号',
   'user', 'dbo', 'table', 'St_StockLogAttach', 'column', 'StockLogAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', 'dbo', 'table', 'St_StockLogAttach', 'column', 'StockLogId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件主表序号',
   'user', 'dbo', 'table', 'St_StockLogAttach', 'column', 'AttachId'
go

alter table dbo.St_StockLogAttach
   add constraint PK_ST_STOCKLOGATTACH primary key (StockLogAttachId)
go

/****** Object:  Stored Procedure [dbo].St_StockLogAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockLogAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockLogAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockLogAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockLogAttachUpdateStatus
// 存储过程功能描述：更新St_StockLogAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockLogAttach'

set @str = 'update [dbo].[St_StockLogAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockLogAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockLogAttachGoBack
// 存储过程功能描述：撤返St_StockLogAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockLogAttach'

set @str = 'update [dbo].[St_StockLogAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockLogAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockLogAttachGet
// 存储过程功能描述：查询指定St_StockLogAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogAttachGet
    /*
	@StockLogAttachId int
    */
    @id int
AS

SELECT
	[StockLogAttachId],
	[StockLogId],
	[AttachId]
FROM
	[dbo].[St_StockLogAttach]
WHERE
	[StockLogAttachId] = @id

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
// 存储过程名：[dbo].St_StockLogAttachLoad
// 存储过程功能描述：查询所有St_StockLogAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogAttachLoad
AS

SELECT
	[StockLogAttachId],
	[StockLogId],
	[AttachId]
FROM
	[dbo].[St_StockLogAttach]

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
// 存储过程名：[dbo].St_StockLogAttachInsert
// 存储过程功能描述：新增一条St_StockLogAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogAttachInsert
	@StockLogId int =NULL ,
	@AttachId int =NULL ,
	@StockLogAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_StockLogAttach] (
	[StockLogId],
	[AttachId]
) VALUES (
	@StockLogId,
	@AttachId
)


SET @StockLogAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockLogAttachUpdate
// 存储过程功能描述：更新St_StockLogAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockLogAttachUpdate
    @StockLogAttachId int,
@StockLogId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_StockLogAttach] SET
	[StockLogId] = @StockLogId,
	[AttachId] = @AttachId
WHERE
	[StockLogAttachId] = @StockLogAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



