alter table dbo.St_StockMove
   drop constraint PK_ST_STOCKMOVE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockMove')
            and   type = 'U')
   drop table dbo.St_StockMove
go

/*==============================================================*/
/* Table: St_StockMove                                          */
/*==============================================================*/
create table dbo.St_StockMove (
   StockMoveId          int                  identity,
   StockMoveApplyId     int                  not null,
   Mover                int                  null,
   MoveTime             datetime             null,
   MoveStatus           int                  null,
   MoveMemo             varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '移库',
   'user', 'dbo', 'table', 'St_StockMove'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库序号',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'StockMoveId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库申请序号',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'StockMoveApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库人',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'Mover'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库时间',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'MoveTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '移库状态',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'MoveStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_StockMove', 'column', 'LastModifyTime'
go

alter table dbo.St_StockMove
   add constraint PK_ST_STOCKMOVE primary key (StockMoveId)
go

/****** Object:  Stored Procedure [dbo].St_StockMoveGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockMoveUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockMoveGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockMoveGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockMoveUpdateStatus
// 存储过程功能描述：更新St_StockMove中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMove'

set @str = 'update [dbo].[St_StockMove] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockMoveId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockMoveGoBack
// 存储过程功能描述：撤返St_StockMove，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockMove'

set @str = 'update [dbo].[St_StockMove] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockMoveId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockMoveGet
// 存储过程功能描述：查询指定St_StockMove的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveGet
    /*
	@StockMoveId int
    */
    @id int
AS

SELECT
	[StockMoveId],
	[StockMoveApplyId],
	[Mover],
	[MoveTime],
	[MoveStatus],
	[MoveMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockMove]
WHERE
	[StockMoveId] = @id

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
// 存储过程名：[dbo].St_StockMoveLoad
// 存储过程功能描述：查询所有St_StockMove记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveLoad
AS

SELECT
	[StockMoveId],
	[StockMoveApplyId],
	[Mover],
	[MoveTime],
	[MoveStatus],
	[MoveMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockMove]

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
// 存储过程名：[dbo].St_StockMoveInsert
// 存储过程功能描述：新增一条St_StockMove记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveInsert
	@StockMoveApplyId int ,
	@Mover int =NULL ,
	@MoveTime datetime =NULL ,
	@MoveStatus int =NULL ,
	@MoveMemo varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@StockMoveId int OUTPUT
AS

INSERT INTO [dbo].[St_StockMove] (
	[StockMoveApplyId],
	[Mover],
	[MoveTime],
	[MoveStatus],
	[MoveMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StockMoveApplyId,
	@Mover,
	@MoveTime,
	@MoveStatus,
	@MoveMemo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StockMoveId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockMoveUpdate
// 存储过程功能描述：更新St_StockMove
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockMoveUpdate
    @StockMoveId int,
@StockMoveApplyId int,
@Mover int = NULL,
@MoveTime datetime = NULL,
@MoveStatus int = NULL,
@MoveMemo varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockMove] SET
	[StockMoveApplyId] = @StockMoveApplyId,
	[Mover] = @Mover,
	[MoveTime] = @MoveTime,
	[MoveStatus] = @MoveStatus,
	[MoveMemo] = @MoveMemo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StockMoveId] = @StockMoveId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



