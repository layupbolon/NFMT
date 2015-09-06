alter table dbo.Con_ContractPrice
   drop constraint PK_CON_CONTRACTPRICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Con_ContractPrice')
            and   type = 'U')
   drop table dbo.Con_ContractPrice
go

/*==============================================================*/
/* Table: Con_ContractPrice                                     */
/*==============================================================*/
create table dbo.Con_ContractPrice (
   ContractPriceId      int                  identity,
   ContractId           int                  null,
   FixedPrice           numeric(18,6)        null,
   FixedPriceMemo       varchar(4000)        null,
   WhoDoPrice           int                  null,
   AlmostPrice          decimal(18,6)        null,
   DoPriceBeginDate     datetime             null,
   DoPriceEndDate       datetime             null,
   IsQP                 bit                  null,
   PriceFrom            int                  null,
   PriceStyle1          int                  null,
   PriceStyle2          int                  null,
   MarginMode           int                  null,
   MarginAmount         numeric(18,4)        null,
   MarginMemo           varchar(4000)        null,
   CreatorId            int                  null,
   CreateTime           datetime             null,
   LastModifyId         int                  null,
   LastModifyTime       datetime             null
)
go

execute sys.sp_addextendedproperty 'MS_Description', 
   '合约价格明细',
   'user', 'dbo', 'table', 'Con_ContractPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约价格序号',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'ContractPriceId'
go

execute sp_addextendedproperty 'MS_Description', 
   '合约序号',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'ContractId'
go

execute sp_addextendedproperty 'MS_Description', 
   '定价价格',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'FixedPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '定价价格备注',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'FixedPriceMemo'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价方',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'WhoDoPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价估价',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'AlmostPrice'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价起始日',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'DoPriceBeginDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '点价最终日',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'DoPriceEndDate'
go

execute sp_addextendedproperty 'MS_Description', 
   '可否QP延期',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'IsQP'
go

execute sp_addextendedproperty 'MS_Description', 
   '裸价来源(交易所列表)',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'PriceFrom'
go

execute sp_addextendedproperty 'MS_Description', 
   '作价方式2',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'PriceStyle2'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格保证金方式',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'MarginMode'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格保证金数量',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'MarginAmount'
go

execute sp_addextendedproperty 'MS_Description', 
   '价格保证金备注',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'MarginMemo'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建人',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'CreatorId'
go

execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'CreateTime'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改人序号',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'LastModifyId'
go

execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', 'dbo', 'table', 'Con_ContractPrice', 'column', 'LastModifyTime'
go

alter table dbo.Con_ContractPrice
   add constraint PK_CON_CONTRACTPRICE primary key (ContractPriceId)
go

/****** Object:  Stored Procedure [dbo].Con_ContractPriceGet    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractPriceGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractPriceGet]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractPriceLoad    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractPriceLoad]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractPriceLoad]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractPriceInsert    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractPriceInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractPriceInsert]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractPriceUpdate    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractPriceUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractPriceUpdate]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractPriceUpdateStatus    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractPriceUpdateStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractPriceUpdateStatus]
GO

/****** Object:  Stored Procedure [dbo].Con_ContractPriceUpdateStatus    Script Date: 2015年2月4日 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Con_ContractPriceGoBack]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Con_ContractPriceGoBack]
GO

    

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
/*----------------------------------------------------------------
// Copyright (C) 2014-2015 上海迪亮信息科技有限公司 版权所有。 
// 存储过程名：[dbo].Con_ContractPriceUpdateStatus
// 存储过程功能描述：更新Con_ContractPrice中的状态
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractPriceUpdateStatus
	@id int = null,
    @status int = null,
    @lastModifyId int = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractPrice'

set @str = 'update [dbo].[Con_ContractPrice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContractPriceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractPriceGoBack
// 存储过程功能描述：撤返Con_ContractPrice，并同步工作流中的状态
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractPriceGoBack
	@id int = null,
    @status int = null,
    @lastModifyId int = null,
    @dbName varchar(200) = null,
    @tableName varchar(50) = null
AS
declare @StatusNameCode varchar(50),@str varchar(1000)

select @StatusNameCode = StatusNameCode from NFMT_Basic.dbo.BDStatus where TableName = 'dbo.Con_ContractPrice'

set @str = 'update [dbo].[Con_ContractPrice] '+
           ' set ' + @StatusNameCode + ' = ' + Convert(varchar,@status)   +', LastModifyId = ' + Convert(varchar,@lastModifyId)  + ' , LastModifyTime = getdate()'
           +' where ContractPriceId = '+ Convert(varchar,@id) 
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
// 存储过程名：[dbo].Con_ContractPriceGet
// 存储过程功能描述：查询指定Con_ContractPrice的一条记录
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractPriceGet
    /*
	@ContractPriceId int
    */
    @id int
AS

SELECT
	[ContractPriceId],
	[ContractId],
	[FixedPrice],
	[FixedPriceMemo],
	[WhoDoPrice],
	[AlmostPrice],
	[DoPriceBeginDate],
	[DoPriceEndDate],
	[IsQP],
	[PriceFrom],
	[PriceStyle1],
	[PriceStyle2],
	[MarginMode],
	[MarginAmount],
	[MarginMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractPrice]
WHERE
	[ContractPriceId] = @id

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
// 存储过程名：[dbo].Con_ContractPriceLoad
// 存储过程功能描述：查询所有Con_ContractPrice记录
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractPriceLoad
AS

SELECT
	[ContractPriceId],
	[ContractId],
	[FixedPrice],
	[FixedPriceMemo],
	[WhoDoPrice],
	[AlmostPrice],
	[DoPriceBeginDate],
	[DoPriceEndDate],
	[IsQP],
	[PriceFrom],
	[PriceStyle1],
	[PriceStyle2],
	[MarginMode],
	[MarginAmount],
	[MarginMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
FROM
	[dbo].[Con_ContractPrice]

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
// 存储过程名：[dbo].Con_ContractPriceInsert
// 存储过程功能描述：新增一条Con_ContractPrice记录
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractPriceInsert
	@ContractId int =NULL ,
	@FixedPrice numeric(18, 6) =NULL ,
	@FixedPriceMemo varchar(4000) =NULL ,
	@WhoDoPrice int =NULL ,
	@AlmostPrice decimal(18, 6) =NULL ,
	@DoPriceBeginDate datetime =NULL ,
	@DoPriceEndDate datetime =NULL ,
	@IsQP bit =NULL ,
	@PriceFrom int =NULL ,
	@PriceStyle1 int =NULL ,
	@PriceStyle2 int =NULL ,
	@MarginMode int =NULL ,
	@MarginAmount numeric(18, 4) =NULL ,
	@MarginMemo varchar(4000) =NULL ,
	@CreatorId int =NULL ,
	@ContractPriceId int OUTPUT
AS

INSERT INTO [dbo].[Con_ContractPrice] (
	[ContractId],
	[FixedPrice],
	[FixedPriceMemo],
	[WhoDoPrice],
	[AlmostPrice],
	[DoPriceBeginDate],
	[DoPriceEndDate],
	[IsQP],
	[PriceFrom],
	[PriceStyle1],
	[PriceStyle2],
	[MarginMode],
	[MarginAmount],
	[MarginMemo],
	[CreatorId],
	[CreateTime],
	[LastModifyId],
	[LastModifyTime]
) VALUES (
	@ContractId,
	@FixedPrice,
	@FixedPriceMemo,
	@WhoDoPrice,
	@AlmostPrice,
	@DoPriceBeginDate,
	@DoPriceEndDate,
	@IsQP,
	@PriceFrom,
	@PriceStyle1,
	@PriceStyle2,
	@MarginMode,
	@MarginAmount,
	@MarginMemo,
	@CreatorId,
        getdate()
,
       @CreatorId
,
       getdate()

)


SET @ContractPriceId = @@IDENTITY

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
// 存储过程名：[dbo].Con_ContractPriceUpdate
// 存储过程功能描述：更新Con_ContractPrice
// 创建人：CodeSmith
// 创建时间： 2015年2月4日
----------------------------------------------------------------*/

CREATE PROCEDURE [dbo].Con_ContractPriceUpdate
    @ContractPriceId int,
@ContractId int = NULL,
@FixedPrice numeric(18, 6) = NULL,
@FixedPriceMemo varchar(4000) = NULL,
@WhoDoPrice int = NULL,
@AlmostPrice decimal(18, 6) = NULL,
@DoPriceBeginDate datetime = NULL,
@DoPriceEndDate datetime = NULL,
@IsQP bit = NULL,
@PriceFrom int = NULL,
@PriceStyle1 int = NULL,
@PriceStyle2 int = NULL,
@MarginMode int = NULL,
@MarginAmount numeric(18, 4) = NULL,
@MarginMemo varchar(4000) = NULL,
@LastModifyId int = NULL
AS

UPDATE [dbo].[Con_ContractPrice] SET
	[ContractId] = @ContractId,
	[FixedPrice] = @FixedPrice,
	[FixedPriceMemo] = @FixedPriceMemo,
	[WhoDoPrice] = @WhoDoPrice,
	[AlmostPrice] = @AlmostPrice,
	[DoPriceBeginDate] = @DoPriceBeginDate,
	[DoPriceEndDate] = @DoPriceEndDate,
	[IsQP] = @IsQP,
	[PriceFrom] = @PriceFrom,
	[PriceStyle1] = @PriceStyle1,
	[PriceStyle2] = @PriceStyle2,
	[MarginMode] = @MarginMode,
	[MarginAmount] = @MarginAmount,
	[MarginMemo] = @MarginMemo,
	[LastModifyId] = @LastModifyId,
    [LastModifyTime] = getdate()

WHERE
	[ContractPriceId] = @ContractPriceId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



