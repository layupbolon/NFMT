alter table St_Pledge
   drop constraint PK_ST_PLEDGE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('St_Pledge')
            and   type = 'U')
   drop table St_Pledge
go

/*==============================================================*/
/* Table: St_Pledge                                             */
/*==============================================================*/
create table St_Pledge (
   PledgeId             int                  identity,
   PledgeApplyId        int                  null,
   Pledger              int                  null,
   PledgeTime           datetime             null,
   PledgeBank           int                  null,
   Memo                 varchar(4000)        null,
   PledgeStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '质押',
   'user', @CurrentUser, 'table', 'St_Pledge'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押序号',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'PledgeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请序号',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押执行人',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'Pledger'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押确认时间',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'PledgeTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押银行',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'PledgeBank'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附言',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'Memo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押状态',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'PledgeStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'St_Pledge', 'column', 'LastModifyTime'
go

alter table St_Pledge
   add constraint PK_ST_PLEDGE primary key (PledgeId)
go

/****** Object:  Stored Procedure [dbo].St_PledgeGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeGet]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeLoad]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeInsert]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeUpdate]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].St_PledgeUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[St_PledgeGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[St_PledgeGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].St_PledgeUpdateStatus
// 存储过程功能描述：更新St_Pledge中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_Pledge'

set @str = 'update [dbo].[St_Pledge] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PledgeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_PledgeGoBack
// 存储过程功能描述：撤返St_Pledge，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.St_Pledge'

set @str = 'update [dbo].[St_Pledge] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where PledgeId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].St_PledgeGet
// 存储过程功能描述：查询指定St_Pledge的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeGet
    /*
	@PledgeId int
    */
    @id int
AS

SELECT
	[PledgeId],
	[PledgeApplyId],
	[Pledger],
	[PledgeTime],
	[PledgeBank],
	[Memo],
	[PledgeStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_Pledge]
WHERE
	[PledgeId] = @id

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
// 存储过程名：[dbo].St_PledgeLoad
// 存储过程功能描述：查询所有St_Pledge记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeLoad
AS

SELECT
	[PledgeId],
	[PledgeApplyId],
	[Pledger],
	[PledgeTime],
	[PledgeBank],
	[Memo],
	[PledgeStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[St_Pledge]

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
// 存储过程名：[dbo].St_PledgeInsert
// 存储过程功能描述：新增一条St_Pledge记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeInsert
	@PledgeApplyId int =NULL ,
	@Pledger int =NULL ,
	@PledgeTime datetime =NULL ,
	@PledgeBank int =NULL ,
	@Memo varchar(4000) =NULL ,
	@PledgeStatus int =NULL ,
	@CreatorId int =NULL ,
	@PledgeId int OUTPUT
AS

INSERT INTO [dbo].[St_Pledge] (
	[PledgeApplyId],
	[Pledger],
	[PledgeTime],
	[PledgeBank],
	[Memo],
	[PledgeStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PledgeApplyId,
	@Pledger,
	@PledgeTime,
	@PledgeBank,
	@Memo,
	@PledgeStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @PledgeId = @@IDENTITY

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
// 存储过程名：[dbo].St_PledgeUpdate
// 存储过程功能描述：更新St_Pledge
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].St_PledgeUpdate
    @PledgeId int,
@PledgeApplyId int = NULL,
@Pledger int = NULL,
@PledgeTime datetime = NULL,
@PledgeBank int = NULL,
@Memo varchar(4000) = NULL,
@PledgeStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[St_Pledge] SET
	[PledgeApplyId] = @PledgeApplyId,
	[Pledger] = @Pledger,
	[PledgeTime] = @PledgeTime,
	[PledgeBank] = @PledgeBank,
	[Memo] = @Memo,
	[PledgeStatus] = @PledgeStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[PledgeId] = @PledgeId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



