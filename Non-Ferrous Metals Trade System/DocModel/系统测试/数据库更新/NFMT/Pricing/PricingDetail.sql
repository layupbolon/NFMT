alter table Pri_PricingDetail
   drop constraint PK_PRI_PRICINGDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_PricingDetail')
            and   type = 'U')
   drop table Pri_PricingDetail
go

/*==============================================================*/
/* Table: Pri_PricingDetail                                     */
/*==============================================================*/
create table Pri_PricingDetail (
   DetailId             int                  identity,
   PricingId            int                  null,
   PricingApplyId       int                  null,
   PricingApplyDetailId int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   PricingWeight        decimal(18,4)        null,
   AvgPrice             decimal(18,4)        null,
   PricingTime          datetime             null,
   Pricinger            int                  null,
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
   '点价明细表',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价明细序号',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'PricingId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价申请序号',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'PricingApplyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请明细序号',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'PricingApplyDetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价重量',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'PricingWeight'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价均价',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'AvgPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价时间',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'PricingTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点价人',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'Pricinger'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_PricingDetail', 'column', 'LastModifyTime'
go

alter table Pri_PricingDetail
   add constraint PK_PRI_PRICINGDETAIL primary key (DetailId)
go


/****** Object:  Stored Procedure [dbo].Pri_PricingDetailGet    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingDetailLoad    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingDetailInsert    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingDetailUpdate    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_PricingDetailUpdateStatus    Script Date: 2014年12月25日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_PricingDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_PricingDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_PricingDetailUpdateStatus
// 存储过程功能描述：更新Pri_PricingDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingDetail'

set @str = 'update [dbo].[Pri_PricingDetail] '+
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
// 存储过程名：[dbo].Pri_PricingDetailGoBack
// 存储过程功能描述：撤返Pri_PricingDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_PricingDetail'

set @str = 'update [dbo].[Pri_PricingDetail] '+
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
// 存储过程名：[dbo].Pri_PricingDetailGet
// 存储过程功能描述：查询指定Pri_PricingDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[PricingId],
	[PricingApplyId],
	[PricingApplyDetailId],
	[StockId],
	[StockLogId],
	[PricingWeight],
	[AvgPrice],
	[PricingTime],
	[Pricinger],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingDetail]
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
// 存储过程名：[dbo].Pri_PricingDetailLoad
// 存储过程功能描述：查询所有Pri_PricingDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingDetailLoad
AS

SELECT
	[DetailId],
	[PricingId],
	[PricingApplyId],
	[PricingApplyDetailId],
	[StockId],
	[StockLogId],
	[PricingWeight],
	[AvgPrice],
	[PricingTime],
	[Pricinger],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_PricingDetail]

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
// 存储过程名：[dbo].Pri_PricingDetailInsert
// 存储过程功能描述：新增一条Pri_PricingDetail记录
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingDetailInsert
	@PricingId int =NULL ,
	@PricingApplyId int =NULL ,
	@PricingApplyDetailId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@PricingWeight decimal(18, 4) =NULL ,
	@AvgPrice decimal(18, 4) =NULL ,
	@PricingTime datetime =NULL ,
	@Pricinger int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Pri_PricingDetail] (
	[PricingId],
	[PricingApplyId],
	[PricingApplyDetailId],
	[StockId],
	[StockLogId],
	[PricingWeight],
	[AvgPrice],
	[PricingTime],
	[Pricinger],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@PricingId,
	@PricingApplyId,
	@PricingApplyDetailId,
	@StockId,
	@StockLogId,
	@PricingWeight,
	@AvgPrice,
	@PricingTime,
	@Pricinger,
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
// 存储过程名：[dbo].Pri_PricingDetailUpdate
// 存储过程功能描述：更新Pri_PricingDetail
// 创建人：CodeSmith
// 创建时间： 2014年12月25日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_PricingDetailUpdate
    @DetailId int,
@PricingId int = NULL,
@PricingApplyId int = NULL,
@PricingApplyDetailId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@PricingWeight decimal(18, 4) = NULL,
@AvgPrice decimal(18, 4) = NULL,
@PricingTime datetime = NULL,
@Pricinger int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_PricingDetail] SET
	[PricingId] = @PricingId,
	[PricingApplyId] = @PricingApplyId,
	[PricingApplyDetailId] = @PricingApplyDetailId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[PricingWeight] = @PricingWeight,
	[AvgPrice] = @AvgPrice,
	[PricingTime] = @PricingTime,
	[Pricinger] = @Pricinger,
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



