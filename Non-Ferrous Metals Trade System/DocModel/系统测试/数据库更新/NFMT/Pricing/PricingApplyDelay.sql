alter table Pri_PricingApplyDelay
   drop constraint PK_PRI_PRICINGAPPLYDELAY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PricingApplyDelay')
            and   type = 'U')
   drop table Pri_PricingApplyDelay
go

/*==============================================================*/
/* Table: Pri_PricingApplyDelay                                 */
/*==============================================================*/
create table Pri_PricingApplyDelay (
   DelayId              int                  identity,
   PricingApplyId       int                  null,
   DelayAmount          decimal(18,4)        null,
   DelayFee             decimal(18,4)        null,
   DelayQP              datetime             null,
   DetailStatus         int                  null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sys.sp_addextendedproperty 'MS_Description', 
   '点价申请延期',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'DelayId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'PricingApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期重量',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'DelayAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期费',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'DelayFee'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '延期Qp',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'DelayQP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请创建人',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDelay', 'column', 'LastModifyTime'
go

alter table Pri_PricingApplyDelay
   add constraint PK_PRI_PRICINGAPPLYDELAY primary key (DelayId)
go


/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDelayGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDelayGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDelayGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDelayLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDelayLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDelayLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDelayInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDelayInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDelayInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDelayUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDelayUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDelayUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDelayUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDelayUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDelayUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDelayUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDelayGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDelayGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingApplyDelayUpdateStatus
// 存储过程功能描述：更新Pri_PricingApplyDelay中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDelayUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingApplyDelay'

set @str = 'update [dbo].[Pri_PricingApplyDelay] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DelayId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PricingApplyDelayGoBack
// 存储过程功能描述：撤返Pri_PricingApplyDelay，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDelayGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingApplyDelay'

set @str = 'update [dbo].[Pri_PricingApplyDelay] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DelayId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_PricingApplyDelayGet
// 存储过程功能描述：查询指定Pri_PricingApplyDelay的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDelayGet
    /*
	@DelayId int
    */
    @id int
AS

SELECT
	[DelayId],
	[PricingApplyId],
	[DelayAmount],
	[DelayFee],
	[DelayQP],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingApplyDelay]
WHERE
	[DelayId] = @id

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
// 存储过程名：[dbo].Pri_PricingApplyDelayLoad
// 存储过程功能描述：查询所有Pri_PricingApplyDelay记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDelayLoad
AS

SELECT
	[DelayId],
	[PricingApplyId],
	[DelayAmount],
	[DelayFee],
	[DelayQP],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingApplyDelay]

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
// 存储过程名：[dbo].Pri_PricingApplyDelayInsert
// 存储过程功能描述：新增一条Pri_PricingApplyDelay记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDelayInsert
	@PricingApplyId int =NULL ,
	@DelayAmount decimal(18, 4) =NULL ,
	@DelayFee decimal(18, 4) =NULL ,
	@DelayQP datetime =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DelayId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PricingApplyDelay] (
	[PricingApplyId],
	[DelayAmount],
	[DelayFee],
	[DelayQP],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PricingApplyId,
	@DelayAmount,
	@DelayFee,
	@DelayQP,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DelayId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_PricingApplyDelayUpdate
// 存储过程功能描述：更新Pri_PricingApplyDelay
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDelayUpdate
    @DelayId int,
@PricingApplyId int = NULL,
@DelayAmount decimal(18, 4) = NULL,
@DelayFee decimal(18, 4) = NULL,
@DelayQP datetime = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PricingApplyDelay] SET
	[PricingApplyId] = @PricingApplyId,
	[DelayAmount] = @DelayAmount,
	[DelayFee] = @DelayFee,
	[DelayQP] = @DelayQP,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DelayId] = @DelayId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



