alter table Pri_StopLossDetail
   drop constraint PK_PRI_STOPLOSSDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_StopLossDetail')
            and   type = 'U')
   drop table Pri_StopLossDetail
go

/*==============================================================*/
/* Table: Pri_StopLossDetail                                    */
/*==============================================================*/
create table Pri_StopLossDetail (
   DetailId             int                  identity,
   StopLossId           int                  null,
   StopLossApplyId      int                  null,
   StopLossApplyDetailId int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   StopLossWeight       decimal(18,4)        null,
   AvgPrice             decimal(18,4)        null,
   StopLossTime         datetime             null,
   StopLosser           int                  null,
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
   '止损明细表',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损明细序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StopLossId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损申请序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StopLossApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损申请明细序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StopLossApplyDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损重量',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StopLossWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损均价',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'AvgPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损时间',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StopLossTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '止损人',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'StopLosser'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_StopLossDetail', 'column', 'LastModifyTime'
go

alter table Pri_StopLossDetail
   add constraint PK_PRI_STOPLOSSDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Pri_StopLossDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_StopLossDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_StopLossDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_StopLossDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_StopLossDetailUpdateStatus
// 存储过程功能描述：更新Pri_StopLossDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_StopLossDetail'

set @str = 'update [dbo].[Pri_StopLossDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_StopLossDetailGoBack
// 存储过程功能描述：撤返Pri_StopLossDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_StopLossDetail'

set @str = 'update [dbo].[Pri_StopLossDetail] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where DetailId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Pri_StopLossDetailGet
// 存储过程功能描述：查询指定Pri_StopLossDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[StopLossId],
	[StopLossApplyId],
	[StopLossApplyDetailId],
	[StockId],
	[StockLogId],
	[StopLossWeight],
	[AvgPrice],
	[StopLossTime],
	[StopLosser],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_StopLossDetail]
WHERE
	[DetailId] = @id

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
// 存储过程名：[dbo].Pri_StopLossDetailLoad
// 存储过程功能描述：查询所有Pri_StopLossDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossDetailLoad
AS

SELECT
	[DetailId],
	[StopLossId],
	[StopLossApplyId],
	[StopLossApplyDetailId],
	[StockId],
	[StockLogId],
	[StopLossWeight],
	[AvgPrice],
	[StopLossTime],
	[StopLosser],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_StopLossDetail]

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
// 存储过程名：[dbo].Pri_StopLossDetailInsert
// 存储过程功能描述：新增一条Pri_StopLossDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossDetailInsert
	@StopLossId int =NULL ,
	@StopLossApplyId int =NULL ,
	@StopLossApplyDetailId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@StopLossWeight decimal(18, 4) =NULL ,
	@AvgPrice decimal(18, 4) =NULL ,
	@StopLossTime datetime =NULL ,
	@StopLosser int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Pri_StopLossDetail] (
	[StopLossId],
	[StopLossApplyId],
	[StopLossApplyDetailId],
	[StockId],
	[StockLogId],
	[StopLossWeight],
	[AvgPrice],
	[StopLossTime],
	[StopLosser],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@StopLossId,
	@StopLossApplyId,
	@StopLossApplyDetailId,
	@StockId,
	@StockLogId,
	@StopLossWeight,
	@AvgPrice,
	@StopLossTime,
	@StopLosser,
	@DetailStatus,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @DetailId = @@IDENTITY

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
// 存储过程名：[dbo].Pri_StopLossDetailUpdate
// 存储过程功能描述：更新Pri_StopLossDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_StopLossDetailUpdate
    @DetailId int,
@StopLossId int = NULL,
@StopLossApplyId int = NULL,
@StopLossApplyDetailId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@StopLossWeight decimal(18, 4) = NULL,
@AvgPrice decimal(18, 4) = NULL,
@StopLossTime datetime = NULL,
@StopLosser int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_StopLossDetail] SET
	[StopLossId] = @StopLossId,
	[StopLossApplyId] = @StopLossApplyId,
	[StopLossApplyDetailId] = @StopLossApplyDetailId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[StopLossWeight] = @StopLossWeight,
	[AvgPrice] = @AvgPrice,
	[StopLossTime] = @StopLossTime,
	[StopLosser] = @StopLosser,
	[DetailStatus] = @DetailStatus,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[DetailId] = @DetailId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



