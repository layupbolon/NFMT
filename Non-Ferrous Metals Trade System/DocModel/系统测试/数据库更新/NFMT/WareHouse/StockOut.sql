alter table dbo.St_StockOut
   drop constraint PK_ST_STOCKOUT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.St_StockOut')
            and   type = 'U')
   drop table dbo.St_StockOut
go

/*==============================================================*/
/* Table: St_StockOut                                           */
/*==============================================================*/
create table dbo.St_StockOut (
   StockOutId           int                  identity,
   StockOutApplyId      int                  null,
   Executor             int                  null,
   Confirmor            int                  null,
   StockOutTime         datetime             null,
   GrosstAmount         decimal(18,4)        null,
   NetAmount            decimal(18,4)        null,
   Bundles              int                  null,
   Unit                 int                  null,
   StockOperateType     int                  null,
   Memo                 varchar(4000)        null,
   StockOutStatus       int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '出库',
   'user', 'dbo', 'table', 'St_StockOut'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库序号',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'StockOutId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库申请序号',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'StockOutApplyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库执行人',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'Executor'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库确认人',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'Confirmor'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库确认时间',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'StockOutTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库总毛重',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'GrosstAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库总净重',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'NetAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库总捆数',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'Bundles'
go

execute sp_addextendedproperty 'MS_Description', 
   '单位',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'Unit'
go

execute sp_addextendedproperty 'MS_Description', 
   '出入库类型',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'StockOperateType'
go

execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'Memo'
go

execute sp_addextendedproperty 'MS_Description', 
   '出库状态',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'StockOutStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'St_StockOut', 'column', 'LastModifyTime'
go

alter table dbo.St_StockOut
   add constraint PK_ST_STOCKOUT primary key (StockOutId)
go


/****** Object:  Stored Procedure [dbo].St_StockOutGet    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutGet]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutLoad    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutLoad]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutInsert    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutInsert]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutUpdate    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutUpdateStatus    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_StockOutUpdateStatus    Script Date: 2015年3月9日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_StockOutGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_StockOutGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_StockOutUpdateStatus
// 存储过程功能描述：更新St_StockOut中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOut'

set @str = 'update [dbo].[St_StockOut] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockOutId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutGoBack
// 存储过程功能描述：撤返St_StockOut，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_StockOut'

set @str = 'update [dbo].[St_StockOut] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where StockOutId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_StockOutGet
// 存储过程功能描述：查询指定St_StockOut的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutGet
    /*
	@StockOutId int
    */
    @id int
AS

SELECT
	[StockOutId],
	[StockOutApplyId],
	[Executor],
	[Confirmor],
	[StockOutTime],
	[GrosstAmount],
	[NetAmount],
	[Bundles],
	[Unit],
	[StockOperateType],
	[Memo],
	[StockOutStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockOut]
WHERE
	[StockOutId] = @id

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
// 存储过程名：[dbo].St_StockOutLoad
// 存储过程功能描述：查询所有St_StockOut记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutLoad
AS

SELECT
	[StockOutId],
	[StockOutApplyId],
	[Executor],
	[Confirmor],
	[StockOutTime],
	[GrosstAmount],
	[NetAmount],
	[Bundles],
	[Unit],
	[StockOperateType],
	[Memo],
	[StockOutStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_StockOut]

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
// 存储过程名：[dbo].St_StockOutInsert
// 存储过程功能描述：新增一条St_StockOut记录
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutInsert
	@StockOutApplyId int =NULL ,
	@Executor int =NULL ,
	@Confirmor int =NULL ,
	@StockOutTime datetime =NULL ,
	@GrosstAmount decimal(18, 4) =NULL ,
	@NetAmount decimal(18, 4) =NULL ,
	@Bundles int =NULL ,
	@Unit int =NULL ,
	@StockOperateType int =NULL ,
	@Memo varchar(4000) =NULL ,
	@StockOutStatus int =NULL ,
	@CreatorId int =NULL ,
	@StockOutId int OUTPUT
AS

INSERT INTO [dbo].[St_StockOut] (
	[StockOutApplyId],
	[Executor],
	[Confirmor],
	[StockOutTime],
	[GrosstAmount],
	[NetAmount],
	[Bundles],
	[Unit],
	[StockOperateType],
	[Memo],
	[StockOutStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StockOutApplyId,
	@Executor,
	@Confirmor,
	@StockOutTime,
	@GrosstAmount,
	@NetAmount,
	@Bundles,
	@Unit,
	@StockOperateType,
	@Memo,
	@StockOutStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @StockOutId = @@IDENTITY

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
// 存储过程名：[dbo].St_StockOutUpdate
// 存储过程功能描述：更新St_StockOut
// 创建人：CodeSmith
// 创建时间： 2015年3月9日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_StockOutUpdate
    @StockOutId int,
@StockOutApplyId int = NULL,
@Executor int = NULL,
@Confirmor int = NULL,
@StockOutTime datetime = NULL,
@GrosstAmount decimal(18, 4) = NULL,
@NetAmount decimal(18, 4) = NULL,
@Bundles int = NULL,
@Unit int = NULL,
@StockOperateType int = NULL,
@Memo varchar(4000) = NULL,
@StockOutStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_StockOut] SET
	[StockOutApplyId] = @StockOutApplyId,
	[Executor] = @Executor,
	[Confirmor] = @Confirmor,
	[StockOutTime] = @StockOutTime,
	[GrosstAmount] = @GrosstAmount,
	[NetAmount] = @NetAmount,
	[Bundles] = @Bundles,
	[Unit] = @Unit,
	[StockOperateType] = @StockOperateType,
	[Memo] = @Memo,
	[StockOutStatus] = @StockOutStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[StockOutId] = @StockOutId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



