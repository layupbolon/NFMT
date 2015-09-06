alter table Pri_InterestDetail
   drop constraint PK_PRI_INTERESTDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Pri_InterestDetail')
            and   type = 'U')
   drop table Pri_InterestDetail
go

/*==============================================================*/
/* Table: Pri_InterestDetail                                    */
/*==============================================================*/
create table Pri_InterestDetail (
   DetailId             int                  identity,
   InterestId           int                  null,
   SubContractId        int                  null,
   ContractId           int                  null,
   StockId              int                  null,
   StockLogId           int                  null,
   InterestAmount       decimal(18,4)        null,
   PricingUnit          decimal(18,4)        null,
   Premium              decimal(18,4)        null,
   OtherPrice           decimal(18,4)        null,
   InterestPrice        decimal(18,4)        null,
   StockBala            decimal(18,4)        null,
   InterestStartDate    datetime             null,
   InterestEndDate      datetime             null,
   InterestDay          int                  null,
   InterestUnit         decimal(18,4)        null,
   InterestBala         decimal(18,4)        null,
   InterestType         int                  null,
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
   '利息明细',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细序号',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'DetailId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '利息序号',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '子合约序号',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'SubContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'ContractId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存序号',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'StockId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '库存流水序号',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'StockLogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息重量',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '期货点价价格',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'PricingUnit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '升贴水',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'Premium'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '其他费用',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'OtherPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息价格',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息货值',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'StockBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '起息日',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestStartDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '到期日',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestEndDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息天数',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestDay'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日利息额',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestUnit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '当前息额',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestBala'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '计息类型',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'InterestType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '明细状态',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'DetailStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '申请创建人',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'CreatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'LastModifyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'Pri_InterestDetail', 'column', 'LastModifyTime'
go

alter table Pri_InterestDetail
   add constraint PK_PRI_INTERESTDETAIL primary key (DetailId)
go

/****** Object:  Stored Procedure [dbo].Pri_InterestDetailGet    Script Date: 2015年3月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestDetailGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestDetailGet]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestDetailLoad    Script Date: 2015年3月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestDetailLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestDetailLoad]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestDetailInsert    Script Date: 2015年3月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestDetailInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestDetailInsert]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestDetailUpdate    Script Date: 2015年3月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestDetailUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestDetailUpdate]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestDetailUpdateStatus    Script Date: 2015年3月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestDetailUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestDetailUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Pri_InterestDetailUpdateStatus    Script Date: 2015年3月20日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Pri_InterestDetailGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Pri_InterestDetailGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Pri_InterestDetailUpdateStatus
// 存储过程功能描述：更新Pri_InterestDetail中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestDetailUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_InterestDetail'

set @str = 'update [dbo].[Pri_InterestDetail] '+
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
// 存储过程名：[dbo].Pri_InterestDetailGoBack
// 存储过程功能描述：撤返Pri_InterestDetail，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestDetailGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Pri_InterestDetail'

set @str = 'update [dbo].[Pri_InterestDetail] '+
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
// 存储过程名：[dbo].Pri_InterestDetailGet
// 存储过程功能描述：查询指定Pri_InterestDetail的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestDetailGet
    /*
	@DetailId int
    */
    @id int
AS

SELECT
	[DetailId],
	[InterestId],
	[SubContractId],
	[ContractId],
	[StockId],
	[StockLogId],
	[InterestAmount],
	[PricingUnit],
	[Premium],
	[OtherPrice],
	[InterestPrice],
	[StockBala],
	[InterestStartDate],
	[InterestEndDate],
	[InterestDay],
	[InterestUnit],
	[InterestBala],
	[InterestType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_InterestDetail]
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
// 存储过程名：[dbo].Pri_InterestDetailLoad
// 存储过程功能描述：查询所有Pri_InterestDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestDetailLoad
AS

SELECT
	[DetailId],
	[InterestId],
	[SubContractId],
	[ContractId],
	[StockId],
	[StockLogId],
	[InterestAmount],
	[PricingUnit],
	[Premium],
	[OtherPrice],
	[InterestPrice],
	[StockBala],
	[InterestStartDate],
	[InterestEndDate],
	[InterestDay],
	[InterestUnit],
	[InterestBala],
	[InterestType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Pri_InterestDetail]

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
// 存储过程名：[dbo].Pri_InterestDetailInsert
// 存储过程功能描述：新增一条Pri_InterestDetail记录
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestDetailInsert
	@InterestId int =NULL ,
	@SubContractId int =NULL ,
	@ContractId int =NULL ,
	@StockId int =NULL ,
	@StockLogId int =NULL ,
	@InterestAmount decimal(18, 4) =NULL ,
	@PricingUnit decimal(18, 4) =NULL ,
	@Premium decimal(18, 4) =NULL ,
	@OtherPrice decimal(18, 4) =NULL ,
	@InterestPrice decimal(18, 4) =NULL ,
	@StockBala decimal(18, 4) =NULL ,
	@InterestStartDate datetime =NULL ,
	@InterestEndDate datetime =NULL ,
	@InterestDay int =NULL ,
	@InterestUnit decimal(18, 4) =NULL ,
	@InterestBala decimal(18, 4) =NULL ,
	@InterestType int =NULL ,
	@DetailStatus int =NULL ,
	@CreatorId int =NULL ,
	@DetailId int OUTPUT
AS

INSERT INTO [dbo].[Pri_InterestDetail] (
	[InterestId],
	[SubContractId],
	[ContractId],
	[StockId],
	[StockLogId],
	[InterestAmount],
	[PricingUnit],
	[Premium],
	[OtherPrice],
	[InterestPrice],
	[StockBala],
	[InterestStartDate],
	[InterestEndDate],
	[InterestDay],
	[InterestUnit],
	[InterestBala],
	[InterestType],
	[DetailStatus],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@InterestId,
	@SubContractId,
	@ContractId,
	@StockId,
	@StockLogId,
	@InterestAmount,
	@PricingUnit,
	@Premium,
	@OtherPrice,
	@InterestPrice,
	@StockBala,
	@InterestStartDate,
	@InterestEndDate,
	@InterestDay,
	@InterestUnit,
	@InterestBala,
	@InterestType,
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
// 存储过程名：[dbo].Pri_InterestDetailUpdate
// 存储过程功能描述：更新Pri_InterestDetail
// 创建人：CodeSmith
// 创建时间： 2015年3月20日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Pri_InterestDetailUpdate
    @DetailId int,
@InterestId int = NULL,
@SubContractId int = NULL,
@ContractId int = NULL,
@StockId int = NULL,
@StockLogId int = NULL,
@InterestAmount decimal(18, 4) = NULL,
@PricingUnit decimal(18, 4) = NULL,
@Premium decimal(18, 4) = NULL,
@OtherPrice decimal(18, 4) = NULL,
@InterestPrice decimal(18, 4) = NULL,
@StockBala decimal(18, 4) = NULL,
@InterestStartDate datetime = NULL,
@InterestEndDate datetime = NULL,
@InterestDay int = NULL,
@InterestUnit decimal(18, 4) = NULL,
@InterestBala decimal(18, 4) = NULL,
@InterestType int = NULL,
@DetailStatus int = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Pri_InterestDetail] SET
	[InterestId] = @InterestId,
	[SubContractId] = @SubContractId,
	[ContractId] = @ContractId,
	[StockId] = @StockId,
	[StockLogId] = @StockLogId,
	[InterestAmount] = @InterestAmount,
	[PricingUnit] = @PricingUnit,
	[Premium] = @Premium,
	[OtherPrice] = @OtherPrice,
	[InterestPrice] = @InterestPrice,
	[StockBala] = @StockBala,
	[InterestStartDate] = @InterestStartDate,
	[InterestEndDate] = @InterestEndDate,
	[InterestDay] = @InterestDay,
	[InterestUnit] = @InterestUnit,
	[InterestBala] = @InterestBala,
	[InterestType] = @InterestType,
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



