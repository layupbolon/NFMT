alter table Fin_RepoApply
   drop constraint PK_FIN_REPOAPPLY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Fin_RepoApply')
            and   type = 'U')
   drop table Fin_RepoApply
go

/*==============================================================*/
/* Table: Fin_RepoApply                                         */
/*==============================================================*/
create table Fin_RepoApply (
   RepoApplyId          int                  identity,
   RepoApplyIdNo        varchar(20)          null,
   PledgeApplyId        int                  null,
   SumNetAmount         numeric(18,4)        null,
   SumHands             int                  null,
   RepoApplyStatus      int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '融资头寸赎回申请单',
   'user', @CurrentUser, 'table', 'Fin_RepoApply'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'RepoApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '赎回申请单号',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'RepoApplyIdNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '质押申请序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'PledgeApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '净重合计',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'SumNetAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '手数合计',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'SumHands'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'RepoApplyStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Fin_RepoApply', 'column', 'LastModifyTime'
go

alter table Fin_RepoApply
   add constraint PK_FIN_REPOAPPLY primary key (RepoApplyId)
go

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyGet    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyGet]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyLoad    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyLoad]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyInsert    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyInsert]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyUpdate    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyUpdate]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyUpdateStatus    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Fin_RepoApplyUpdateStatus    Script Date: 2015年4月24日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Fin_RepoApplyGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Fin_RepoApplyGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Fin_RepoApplyUpdateStatus
// 存储过程功能描述：更新Fin_RepoApply中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_RepoApply'

set @str = 'update [dbo].[Fin_RepoApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RepoApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_RepoApplyGoBack
// 存储过程功能描述：撤返Fin_RepoApply，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Fin_RepoApply'

set @str = 'update [dbo].[Fin_RepoApply] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where RepoApplyId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Fin_RepoApplyGet
// 存储过程功能描述：查询指定Fin_RepoApply的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyGet
    /*
	@RepoApplyId int
    */
    @id int
AS

SELECT
	[RepoApplyId],
	[RepoApplyIdNo],
	[PledgeApplyId],
	[SumNetAmount],
	[SumHands],
	[RepoApplyStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fin_RepoApply]
WHERE
	[RepoApplyId] = @id

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
// 存储过程名：[dbo].Fin_RepoApplyLoad
// 存储过程功能描述：查询所有Fin_RepoApply记录
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyLoad
AS

SELECT
	[RepoApplyId],
	[RepoApplyIdNo],
	[PledgeApplyId],
	[SumNetAmount],
	[SumHands],
	[RepoApplyStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Fin_RepoApply]

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
// 存储过程名：[dbo].Fin_RepoApplyInsert
// 存储过程功能描述：新增一条Fin_RepoApply记录
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyInsert
	@RepoApplyIdNo varchar(20) =NULL ,
	@PledgeApplyId int =NULL ,
	@SumNetAmount numeric(18, 4) =NULL ,
	@SumHands int =NULL ,
	@RepoApplyStatus int =NULL ,
	@CreatorId int =NULL ,
	@RepoApplyId int OUTPUT
AS

INSERT INTO [dbo].[Fin_RepoApply] (
	[RepoApplyIdNo],
	[PledgeApplyId],
	[SumNetAmount],
	[SumHands],
	[RepoApplyStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@RepoApplyIdNo,
	@PledgeApplyId,
	@SumNetAmount,
	@SumHands,
	@RepoApplyStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @RepoApplyId = @@IDENTITY

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
// 存储过程名：[dbo].Fin_RepoApplyUpdate
// 存储过程功能描述：更新Fin_RepoApply
// 创建人：CodeSmith
// 创建时间： 2015年4月24日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Fin_RepoApplyUpdate
    @RepoApplyId int,
@RepoApplyIdNo varchar(20) = NULL,
@PledgeApplyId int = NULL,
@SumNetAmount numeric(18, 4) = NULL,
@SumHands int = NULL,
@RepoApplyStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Fin_RepoApply] SET
	[RepoApplyIdNo] = @RepoApplyIdNo,
	[PledgeApplyId] = @PledgeApplyId,
	[SumNetAmount] = @SumNetAmount,
	[SumHands] = @SumHands,
	[RepoApplyStatus] = @RepoApplyStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[RepoApplyId] = @RepoApplyId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



