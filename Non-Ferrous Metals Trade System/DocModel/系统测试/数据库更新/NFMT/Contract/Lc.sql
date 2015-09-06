alter table dbo.Con_Lc
   drop constraint PK_CON_LC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_Lc')
            and   type = 'U')
   drop table dbo.Con_Lc
go

/*==============================================================*/
/* Table: Con_Lc                                                */
/*==============================================================*/
create table dbo.Con_Lc (
   LcId                 int                  identity,
   IssueBank            int                  null,
   AdviseBank           int                  null,
   IssueDate            datetime             null,
   FutureDay            int                  null,
   LcBala               numeric(18,4)        null,
   Currency             int                  null,
   LCStatus             int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '信用证',
   'user', 'dbo', 'table', 'Con_Lc'
go

execute sp_addextendedproperty 'MS_Description', 
   '信用证序号',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'LcId'
go

execute sp_addextendedproperty 'MS_Description', 
   '开证行',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'IssueBank'
go

execute sp_addextendedproperty 'MS_Description', 
   '通知行',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'AdviseBank'
go

execute sp_addextendedproperty 'MS_Description', 
   '开证日期',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'IssueDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '远期天数',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'FutureDay'
go

execute sp_addextendedproperty 'MS_Description', 
   '信用证金额',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'LcBala'
go

execute sp_addextendedproperty 'MS_Description', 
   '信用证币种',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'Currency'
go

execute sp_addextendedproperty 'MS_Description', 
   '信用证状态',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'LCStatus'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_Lc', 'column', 'LastModifyTime'
go

alter table dbo.Con_Lc
   add constraint PK_CON_LC primary key (LcId)
go


/****** Object:  Stored Procedure [dbo].Con_LcGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcGet]
GO

/****** Object:  Stored Procedure [dbo].Con_LcLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_LcInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_LcUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_LcUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_LcUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_LcGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_LcGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_LcUpdateStatus
// 存储过程功能描述：更新Con_Lc中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_Lc'

set @str = 'update [dbo].[Con_Lc] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where LcId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_LcGoBack
// 存储过程功能描述：撤返Con_Lc，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_Lc'

set @str = 'update [dbo].[Con_Lc] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where LcId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_LcGet
// 存储过程功能描述：查询指定Con_Lc的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcGet
    /*
	@LcId int
    */
    @id int
AS

SELECT
	[LcId],
	[IssueBank],
	[AdviseBank],
	[IssueDate],
	[FutureDay],
	[LcBala],
	[Currency],
	[LCStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_Lc]
WHERE
	[LcId] = @id

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
// 存储过程名：[dbo].Con_LcLoad
// 存储过程功能描述：查询所有Con_Lc记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcLoad
AS

SELECT
	[LcId],
	[IssueBank],
	[AdviseBank],
	[IssueDate],
	[FutureDay],
	[LcBala],
	[Currency],
	[LCStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_Lc]

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
// 存储过程名：[dbo].Con_LcInsert
// 存储过程功能描述：新增一条Con_Lc记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcInsert
	@IssueBank int =NULL ,
	@AdviseBank int =NULL ,
	@IssueDate datetime =NULL ,
	@FutureDay int =NULL ,
	@LcBala numeric(18, 4) =NULL ,
	@Currency int =NULL ,
	@LCStatus int =NULL ,
	@CreatorId int =NULL ,
	@LcId int OUTPUT
AS

INSERT INTO [dbo].[Con_Lc] (
	[IssueBank],
	[AdviseBank],
	[IssueDate],
	[FutureDay],
	[LcBala],
	[Currency],
	[LCStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@IssueBank,
	@AdviseBank,
	@IssueDate,
	@FutureDay,
	@LcBala,
	@Currency,
	@LCStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @LcId = @@IDENTITY

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
// 存储过程名：[dbo].Con_LcUpdate
// 存储过程功能描述：更新Con_Lc
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_LcUpdate
    @LcId int,
@IssueBank int = NULL,
@AdviseBank int = NULL,
@IssueDate datetime = NULL,
@FutureDay int = NULL,
@LcBala numeric(18, 4) = NULL,
@Currency int = NULL,
@LCStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_Lc] SET
	[IssueBank] = @IssueBank,
	[AdviseBank] = @AdviseBank,
	[IssueDate] = @IssueDate,
	[FutureDay] = @FutureDay,
	[LcBala] = @LcBala,
	[Currency] = @Currency,
	[LCStatus] = @LCStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[LcId] = @LcId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



