alter table Pri_PricingApplyDetail
   drop constraint PK_PRI_PRICINGAPPLYDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PricingApplyDetail')
            and   type = 'U')
   drop table Pri_PricingApplyDetail
go

/*==============================================================*/
/* Table: Pri_PricingApplyDetail                                */
/*==============================================================*/
create table Pri_PricingApplyDetail (
   DetailId             int                  identity,
   PricingApplyId       int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   PricingWeight        decimal(18,4)        null,
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
   '点价申请明细',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'PricingApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价重量',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'PricingWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请创建人',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PricingApplyDetail', 'column', 'LastModifyTime'
go

alter table Pri_PricingApplyDetail
   add constraint PK_PRI_PRICINGAPPLYDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingApplyDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingApplyDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingApplyDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingApplyDetailUpdateStatus
// 存储过程功能描述：更新Pri_PricingApplyDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingApplyDetail'

set @str = 'update [dbo].[Pri_PricingApplyDetail] '+
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
// 存储过程名：[dbo].Pri_PricingApplyDetailGoBack
// 存储过程功能描述：撤返Pri_PricingApplyDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingApplyDetail'

set @str = 'update [dbo].[Pri_PricingApplyDetail] '+
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
// 存储过程名：[dbo].Pri_PricingApplyDetailGet
// 存储过程功能描述：查询指定Pri_PricingApplyDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PricingApplyId],
	[StockId],
	[StockLogId],
	[PricingWeight],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingApplyDetail]
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
// 存储过程名：[dbo].Pri_PricingApplyDetailLoad
// 存储过程功能描述：查询所有Pri_PricingApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDetailLoad
AS

SELECT
	[DetailId],
	[PricingApplyId],
	[StockId],
	[StockLogId],
	[PricingWeight],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingApplyDetail]

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
// 存储过程名：[dbo].Pri_PricingApplyDetailInsert
// 存储过程功能描述：新增一条Pri_PricingApplyDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDetailInsert
	@PricingApplyId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@PricingWeight decimal(18, 4) =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PricingApplyDetail] (
	[PricingApplyId],
	[StockId],
	[StockLogId],
	[PricingWeight],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PricingApplyId,
	@StockId,
	@StockLogId,
	@PricingWeight,
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
// 存储过程名：[dbo].Pri_PricingApplyDetailUpdate
// 存储过程功能描述：更新Pri_PricingApplyDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingApplyDetailUpdate
    @DetailId int,
@PricingApplyId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@PricingWeight decimal(18, 4) = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PricingApplyDetail] SET
	[PricingApplyId] = @PricingApplyId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[PricingWeight] = @PricingWeight,
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



