alter table dbo.St_StockMoveAttach
   drop constraint PK_ST_STOCKMOVEATTACH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockMoveAttach')
            and   type = 'U')
   drop table dbo.St_StockMoveAttach
go

/*==============================================================*/
/* Table: St_StockMoveAttach                                    */
/*==============================================================*/
create table dbo.St_StockMoveAttach (
   StockMoveAttachId    int                  identity,
   StockMoveId          int                  null,
   AttachId             int                  null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '移库附件',
   'user', 'dbo', 'table', 'St_StockMoveAttach'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库附件序号',
   'user', 'dbo', 'table', 'St_StockMoveAttach', 'column', 'StockMoveAttachId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库序号',
   'user', 'dbo', 'table', 'St_StockMoveAttach', 'column', 'StockMoveId'
go

execute sp_addextendedproperty 'MS_Description', 
   '附件序号',
   'user', 'dbo', 'table', 'St_StockMoveAttach', 'column', 'AttachId'
go

alter table dbo.St_StockMoveAttach
   add constraint PK_ST_STOCKMOVEATTACH primary key (StockMoveAttachId)
go

/****** Object:  Stored Procedure [dbo].St_StockMoveAttachGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveAttachGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveAttachGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveAttachLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveAttachLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveAttachLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveAttachInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveAttachInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveAttachInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveAttachUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveAttachUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveAttachUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveAttachUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveAttachUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveAttachUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveAttachGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveAttachGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockMoveAttachUpdateStatus
// 存储过程功能描述：更新St_StockMoveAttach中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveAttachUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMoveAttach'

set @str = 'update [dbo].[St_StockMoveAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockMoveAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockMoveAttachGoBack
// 存储过程功能描述：撤返St_StockMoveAttach，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveAttachGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMoveAttach'

set @str = 'update [dbo].[St_StockMoveAttach] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   
           +' where StockMoveAttachId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockMoveAttachGet
// 存储过程功能描述：查询指定St_StockMoveAttach的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveAttachGet
    /*
	@StockMoveAttachId int
    */
    @id int
AS

SELECT
	[StockMoveAttachId],
	[StockMoveId],
	[AttachId]
FROM
	[dbo].[St_StockMoveAttach]
WHERE
	[StockMoveAttachId] = @id

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
// 存储过程名：[dbo].St_StockMoveAttachLoad
// 存储过程功能描述：查询所有St_StockMoveAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveAttachLoad
AS

SELECT
	[StockMoveAttachId],
	[StockMoveId],
	[AttachId]
FROM
	[dbo].[St_StockMoveAttach]

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
// 存储过程名：[dbo].St_StockMoveAttachInsert
// 存储过程功能描述：新增一条St_StockMoveAttach记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveAttachInsert
	@StockMoveId int =NULL ,
	@AttachId int =NULL ,
	@StockMoveAttachId int OUTPUT
AS

INSERT INTO [dbo].[St_StockMoveAttach] (
	[StockMoveId],
	[AttachId]
) VALUES (
	@StockMoveId,
	@AttachId
)


SET @StockMoveAttachId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockMoveAttachUpdate
// 存储过程功能描述：更新St_StockMoveAttach
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveAttachUpdate
    @StockMoveAttachId int,
@StockMoveId int = NULL,
@AttachId int = NULL
AS

UPDATE [dbo].[St_StockMoveAttach] SET
	[StockMoveId] = @StockMoveId,
	[AttachId] = @AttachId
WHERE
	[StockMoveAttachId] = @StockMoveAttachId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



